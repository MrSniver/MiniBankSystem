using MiniBankSystem.AccountActions.Interfaces;
using MiniBankSystem.AccountActions.Services.Interfaces;
using MiniBankSystem.AccountActions.Validators;
using MiniBankSystem.Entites;
using MiniBankSystem.PrintControl;

namespace MiniBankSystem;

public class Bank
{
    private readonly IAccountService _accountService;
    private readonly IAccountManagementActions _accountManagementActions;
    private readonly ITransactionActions _transactionActions;
    private readonly InputValidator _validator;
    private Account _loggedInAccount;

    public Bank(IAccountService accountService, IAccountManagementActions accountManagementActions, ITransactionActions transactionActions, InputValidator validator)
    {
        _accountService = accountService;
        _accountManagementActions = accountManagementActions;
        _transactionActions = transactionActions;
        _validator = validator;
    }

    public void Run()
    {
        bool isRunning = true;

        do
        {
            if(_loggedInAccount == null)
            {
                ConsolePrintControl.PrintHeader("Mini Bank System");
                ConsolePrintControl.PrintMenu("Zaloguj się na konto", "Zarejestruj nowe konto", "Wyjdź");
            
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        _loggedInAccount = LogIntoAccount();
                        break;
                    case "2":
                        RegisterNewAccount();
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
                        break;
                }
            }
            else
            {
                ConsolePrintControl.PrintHeader($"Konto: {_loggedInAccount.AccountNumber} {_loggedInAccount.Owner.FirstName} {_loggedInAccount.Owner.LastName}");
                ConsolePrintControl.PrintMenu("Wypłać pieniądze", "Wpłać pieniądze", "Wykonaj przelew", "Sprawdź saldo", "Wyświetl informacje o koncie", "Wyloguj się");

                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        WithdrawMoney();
                        break;
                    case "2":
                        DepositMoney();
                        break;
                    case "3":
                        TransferMoney();
                        break;
                    case "4":
                        _accountManagementActions.DisplayAccountBalance(_loggedInAccount);
                        break;
                    case "5":
                        _accountManagementActions.DisplayAllAccountInfo(_loggedInAccount);
                        break;
                    case "6":
                        _loggedInAccount = null;
                        break;
                }
            }
        } while(isRunning);
    }

    private void RegisterNewAccount()
    {
        string firstName = _validator.GetValidatedInput("Imię", ValidationType.LettersOnly);
        string lastName = _validator.GetValidatedInput("Nazwisko", ValidationType.LettersOnly);

        string streetName = _validator.GetValidatedInput("Nazwa ulicy", ValidationType.NotEmpty); //ulica nie ma dodatkowej walidacji ze względu na to, że może mieć w nazwie zarówno litery i cyfry
        string homeNumber = _validator.GetValidatedInput("Numer domu", ValidationType.PositiveNumbers);
        string aptNumber = _validator.GetValidatedInput("Numer mieszkania (jeżeli brak napisać 0)", ValidationType.PositiveNumbers);
        string cityName = _validator.GetValidatedInput("Nazwa miasta", ValidationType.LettersOnly);
        string postalCode;
        do //specjalny walidator dla kodu pocztowego w celu zapewnienia tylko 6 znaków
        {
            postalCode = _validator.GetValidatedInput("Kod pocztowy (Format 11000)", ValidationType.PositiveNumbers);//kod pocztowy może być wpisany jako 6 cyfr, bo jest on później zmieniany na poprawny przy tworzeniu konta
            if (postalCode.Length != 5)
            {
                Console.WriteLine("Kod pocztowy musi mieć dokładnie 5 cyfr.");
            }
        } while (postalCode.Length != 5);

        string provinceName = _validator.GetValidatedInput("Województwo", ValidationType.LettersOnly);
        string countryName = _validator.GetValidatedInput("Państwo", ValidationType.LettersOnly);

        string username = _validator.GetValidatedInput("Nazwa użytkownika", ValidationType.NotEmpty);
        string password = _validator.GetValidatedInput("Hasło", ValidationType.NotEmpty); //brak dodatkowej walidacji dla hasła, bo system logowania jest uproszczony w projekcie

        bool success = _accountManagementActions.CreateNewAccount(firstName, lastName, streetName, homeNumber, cityName, postalCode, provinceName, countryName, username, password, aptNumber);

        if(success)
        {
            Console.WriteLine("Konto zostało pomyślnie utworzone");
        }
    }

    private Account LogIntoAccount()
    {
        string username = _validator.GetValidatedInput("Nazwa użytkownika", ValidationType.NotEmpty);
        string password = _validator.GetValidatedInput("Hasło", ValidationType.NotEmpty);

        Account loggedAccount = _accountManagementActions.LogIntoAccount(username, password);

        if(loggedAccount != null)
        {
            Console.WriteLine("Zalogowano");
            return loggedAccount;
        }

        return null;
    }

    private void WithdrawMoney()
    {
        string withdrawalAmount = _validator.GetValidatedInput("Wypłacane pieniądze", ValidationType.Decimal);

        bool success = _transactionActions.Withdraw(_loggedInAccount.AccountNumber, Decimal.Parse(withdrawalAmount));

        if(success)
        {
            _loggedInAccount = _accountService.GetByAccountNumber(_loggedInAccount.AccountNumber); //prosty update dla zalogowanego konta w celu aktualizacji saldo
            Console.WriteLine($"Wypłacono {withdrawalAmount} PLN. Nowe saldo: {_loggedInAccount.Balance} PLN");
        }
    }

    private void DepositMoney()
    {
        string depositAmount = _validator.GetValidatedInput("Wpłacane pieniądze", ValidationType.Decimal);

        bool success = _transactionActions.Deposit(_loggedInAccount.AccountNumber, Decimal.Parse(depositAmount));

        if(success)
        {
            _loggedInAccount = _accountService.GetByAccountNumber(_loggedInAccount.AccountNumber); //prosty update dla zalogowanego konta w celu aktualizacji saldo
            Console.WriteLine($"Wpłacono {depositAmount} PLN. Nowe saldo: {_loggedInAccount.Balance} PLN");
        }
    }

    private void TransferMoney()
    {
        string receiverAccountNumber = _validator.GetValidatedInput("Numer odbiorcy", ValidationType.NotEmpty);

        string transferAmount = _validator.GetValidatedInput("Przelewane pieniądze", ValidationType.Decimal);

        bool success = _transactionActions.Transfer(_loggedInAccount.AccountNumber, receiverAccountNumber, Decimal.Parse(transferAmount));

        if(success)
        {
            _loggedInAccount = _accountService.GetByAccountNumber(_loggedInAccount.AccountNumber); //prosty update dla zalogowanego konta w celu aktualizacji saldo
            Console.WriteLine($"Przelano {transferAmount} PLN. Nowe saldo: {_loggedInAccount.Balance} PLN");
        }
    }
}