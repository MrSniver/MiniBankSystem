using MiniBankSystem.AccountActions;
using MiniBankSystem.AccountActions.Services;
using MiniBankSystem.AccountActions.Validators;

namespace MiniBankSystem;

public class Program
{
    public static void Main(string[] args)
    {
        //Prosty manualny DI, żeby nie było potrzeby wykorzystywania Extensions.DependencyInjection
        var accountService = new AccountService();
        var accountManagement = new AccountManagementActions(accountService);
        var transactionActions = new TransactionActions(accountService);
        var validator = new InputValidator();
        var bank = new Bank(accountService, accountManagement, transactionActions, validator);

        bank.Run();
    }
}