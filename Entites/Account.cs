namespace MiniBankSystem.Entites;

public class Account
{
    public string AccountNumber { get; } // Numer konta zapisany w string, ze względu na to, że nie jest on modyfikowany w kodzie, raz wygenerowany zostaje taki sam, dlatego ma tylko get;
    public AccountOwner Owner { get; set; }
    public decimal Balance { get; set; }
    public string AccountUsername { get; set; }
    public string AccountPassword { get; set; }

    public Account(string accountNumber)
    {
        AccountNumber = accountNumber;
    }
}