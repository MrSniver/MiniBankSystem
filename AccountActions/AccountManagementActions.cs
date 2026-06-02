using MiniBankSystem.AccountActions.Interfaces;
using MiniBankSystem.AccountActions.Services;
using MiniBankSystem.Entites;

namespace MiniBankSystem.AccountActions;

public class AccountManagementActions : IAccountManagementActions
{
    private readonly AccountService _accountService;

    public AccountManagementActions(AccountService accountService)
    {
        _accountService = accountService;
    }

    public bool CreateNewAccount(string firstName, string lastName, string streetName, string homeNumber, string cityName, string postalCode, string provinceName, 
        string countryName, string username, string password, string aptNumber = null)
    {
        var existingAccount = _accountService.GetAll().FirstOrDefault(a => a.AccountUsername == username);
    
        if (existingAccount != null)
        {
            Console.WriteLine("Użytkownik o tej nazwie już istnieje.");
            return false;
        }


        Account newAccount = new Account(AccountService.GenerateAccountNumber())
        {
            Owner = new AccountOwner
            {
                FirstName = CapitalizeFirstChar(firstName),
                LastName = CapitalizeFirstChar(lastName),
                StreetName = CapitalizeFirstChar(streetName),
                HomeNumber = homeNumber,
                AptNumber = aptNumber,
                CityName = CapitalizeFirstChar(cityName),
                PostalCode = FormatPostalCode(postalCode),
                ProvinceName = CapitalizeFirstChar(provinceName),
                CountryName = CapitalizeFirstChar(countryName),
            },
            Balance = 0m,
            AccountUsername = username,
            AccountPassword = password
        };

        var createdAccount = _accountService.AddAccount(newAccount);

        if(createdAccount == null)
        {
            Console.WriteLine("Błąd podczas tworzenia konta");
            return false;
        }

        return true;
    }

    public Account LogIntoAccount(string username, string password)
    {
        Account loggedAccount = _accountService.GetAccountByUsernameAndPassword(username, password);

        if(loggedAccount == null)
        {
            Console.WriteLine("Błędna nazwa użytkownika lub hasło");
            return null;
        }

        return loggedAccount;
    }

    public void DisplayAccountBalance(Account account)
    {
        Console.WriteLine($"Aktualny balans konta: {account.Balance:F2} PLN");
    }

    public void DisplayAllAccountInfo(Account account)
    {
        Console.WriteLine($"====== Informacje o koncie ======");
        Console.WriteLine($"Numer konta: {account.AccountNumber}");
        Console.WriteLine($"Imię: {account.Owner.FirstName}");
        Console.WriteLine($"Nazwisko: {account.Owner.LastName}");
        Console.WriteLine($"Ulica: {account.Owner.StreetName}");
        Console.WriteLine($"Numer domu: {account.Owner.HomeNumber}");
        Console.WriteLine($"Numer mieszkania: {account.Owner.AptNumber ?? "Brak"}");
        Console.WriteLine($"Miasto: {account.Owner.CityName}");
        Console.WriteLine($"Kod pocztowy: {account.Owner.PostalCode}");
        Console.WriteLine($"Województwo: {account.Owner.ProvinceName}");
        Console.WriteLine($"Kraj: {account.Owner.CountryName}");
        Console.WriteLine($"Saldo: {account.Balance:F2} PLN");
        Console.WriteLine($"================================");
    }

    public string CapitalizeFirstChar(string input)
    {
        return string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1));
    }

    public string FormatPostalCode(string input)
    {
        if(!input.Contains(" ") || !input.Contains("-"))
        {
            input = input.Insert(2, "-");
        }
        else
        {
            input = input.Replace(" ", "-");
        }

        return input;
    }
}