using MiniBankSystem.AccountActions.Interfaces;
using MiniBankSystem.AccountActions.Services.Interfaces;
using MiniBankSystem.Entites;

namespace MiniBankSystem.AccountActions;

public class TransactionActions : ITransactionActions
{
    private readonly IAccountService _accountService;

    public TransactionActions(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public bool Withdraw(string accountNumber, decimal withdrawalAmount)
    {
        var account = ValidateAndGetAccount(accountNumber, "Numer konta do wypłaty");
        if(account == null) return false;

        if(account.Balance < withdrawalAmount)
        {
            Console.WriteLine($"Brak dostępnych środków na wypłacenie {withdrawalAmount} PLN");
            return false;
        }

        _accountService.WithdrawFromAccount(account, withdrawalAmount);
        return true;
    }
    public bool Deposit(string accountNumber, decimal depositAmount)
    {
        var account = ValidateAndGetAccount(accountNumber, "Numer konta do wpłaty");
        if(account == null) return false;

        _accountService.DepositIntoAccount(account, depositAmount);
        return true;
    }

    public bool Transfer(string transferAccountNumber, string receiverAccountNumber, decimal transferAmount)
    {
        var transferAccount = ValidateAndGetAccount(transferAccountNumber, "Numer konta nadawcy");
        if(transferAccount == null) return false;

        if(transferAccount.Balance < transferAmount)
        {
            Console.WriteLine($"Brak dostępnych funduszy na przelanie {transferAmount} PLN");
            return false;
        }

        var receiverAccount = ValidateAndGetAccount(receiverAccountNumber, "Numer konta odbiorcy");
        if(receiverAccount == null) return false;

        _accountService.TransferBetweenAccounts(transferAccount, receiverAccount, transferAmount);
        return true;
    }


    /// <summary>
    /// Funkcja pozwalająca zwalidować, czy podany numer konta jest pusty lub czy konto zostało odnalezione, żeby nie powtarzać tych samych instrukcji warunkowych
    /// </summary>
    /// <returns></returns>
    private Account? ValidateAndGetAccount(string accountNumber, string accountType = "Numer konta")
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            Console.WriteLine($"{accountType} jest pusty.");
            return null;
        }

        Account account = _accountService.GetByAccountNumber(accountNumber);
        
        if(account == null)
        {
            Console.WriteLine($"Nie znaleziono konta dla numeru: {accountNumber}.");
        }

        return account;
    }

}