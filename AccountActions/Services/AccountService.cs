using System.Security.Cryptography.X509Certificates;
using MiniBankSystem.AccountActions.Services.Interfaces;
using MiniBankSystem.Entites;

namespace MiniBankSystem.AccountActions.Services;

public class AccountService: IAccountService
{
    private readonly List<Account> _account = new() //Seedowanie kont przy rozpoczynaniu aplikacji w serwisie
    {
        new Account("PL09109010140000000123456789")
        {
            Owner = new AccountOwner
            {
                FirstName = "John",
                LastName = "Doe",
                StreetName = "Ryszardowa",
                HomeNumber = "15",
                CityName = "Rysa",
                PostalCode = "11-110",
                ProvinceName = "Małopolskie",
                CountryName = "Polska"
            },
            Balance = 2500.45m,
            AccountUsername = "JohnDoe15",
            AccountPassword = "JDoe25"
        },
        new Account("PL45916841290740172176509724")
        {
            Owner = new AccountOwner
            {
                FirstName = "Jane",
                LastName = "Doe",
                StreetName = "Koszykowa",
                HomeNumber = "45",
                CityName = "Daleszyce",
                PostalCode = "12-115",
                ProvinceName = "Świętokrzyskie",
                CountryName = "Polska"
            },
            Balance = 5400.84m,
            AccountUsername = "JaneD90",
            AccountPassword = "D9Jane"
        }
    };

    private const string CountryCode = "PL";
    private const string BankCode = "10901014";

    public IEnumerable<Account> GetAll()
    {
        return _account;
    }

    public Account GetAccountByUsernameAndPassword(string username, string password) => _account.FirstOrDefault(x => x.AccountUsername == username && x.AccountPassword == password);

    public Account? GetByAccountNumber(string accountNumber) => _account.FirstOrDefault(x => x.AccountNumber == accountNumber);

    public Account? GetByFullName(string accountFullName) => _account.FirstOrDefault(x => x.Owner.FirstName + " " + x.Owner.LastName == accountFullName);

    public Account AddAccount(Account account)
    {
        _account.Add(account);
        return account;
    }

    public Decimal GetBalance(string accountNumber) => _account.FirstOrDefault(x => x.AccountNumber == accountNumber).Balance;

    public void DepositIntoAccount(Account account, decimal depositAmount)
    {
        account.Balance += depositAmount;
    }

    public void WithdrawFromAccount(Account account, decimal withdrawalAmount)
    {
        account.Balance -= withdrawalAmount;
    }

    public void TransferBetweenAccounts(Account transferAccount, Account receiverAccount, decimal transferAmount)
    {
        transferAccount.Balance -= transferAmount;
        receiverAccount.Balance += transferAmount;
    }

    /// <summary>
    /// Funkcja generująca numer polskiego konta bankowego z kodem państwa oraz cyframi kontrolnymi
    /// </summary>
    /// <returns></returns>
    public static string GenerateAccountNumber()
    {
        string accountNumber = new Random() // Ten random pozawla nam na wygenerowanie numeru konta, które będzie miało 16 znaków, ponieważ będzie uzupełnione do 16 znaków lub obcięte do 16 znaków
        .Next(1000000000, int.MaxValue)
        .ToString()
        .PadLeft(16, '0')
        .Substring(0, 16);

        int checkDigits = CalculateCheckDigits(accountNumber);

        return CountryCode + checkDigits.ToString("D2") + BankCode + accountNumber;
    }

    /// <summary>
    /// Funcja odpowiedzialna za obliczenie cyfr kontrolnych
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <returns></returns>
    public static int CalculateCheckDigits(string accountNumber)
    {
        string accountNumberWithoutChecks = BankCode + accountNumber + CountryCode + "00";
        string numeric = accountNumberWithoutChecks.Replace("P", "25").Replace("L", "21");

        int mod = 0;
        foreach(char c in numeric)
        {
            mod = (mod * 10 + int.Parse(c.ToString())) % 97;
        }

        return 98 - mod;
    }
}