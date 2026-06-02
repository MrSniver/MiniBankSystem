using MiniBankSystem.Entites;

namespace MiniBankSystem.AccountActions.Services.Interfaces;

public interface IAccountService
{
    IEnumerable<Account> GetAll();
    Account GetAccountByUsernameAndPassword(string username, string password);
    Account? GetByAccountNumber(string accountNumber);
    Account? GetByFullName(string accountFullName);
    Account AddAccount(Account account);
    Decimal GetBalance(string accountNumber);
    void DepositIntoAccount(Account account, decimal depositAmount);
    void WithdrawFromAccount(Account account, decimal withdrawalAmount);
    void TransferBetweenAccounts(Account transferAccount, Account receiverAccount, decimal transferAmount);
}