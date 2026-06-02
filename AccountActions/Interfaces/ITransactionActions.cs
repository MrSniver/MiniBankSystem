using MiniBankSystem.Entites;

namespace MiniBankSystem.AccountActions.Interfaces;

public interface ITransactionActions
{
    public bool Withdraw(string accountNumber, decimal withdrawalAmount);
    
    public bool Deposit(string accountNumber, decimal depositAmount);

    public bool Transfer(string transferAccountNumber, string receiverAccountNumber, decimal transferAmount);
}