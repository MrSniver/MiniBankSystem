using MiniBankSystem.Entites;

namespace MiniBankSystem.AccountActions.Interfaces;

public interface IAccountManagementActions
{
    bool CreateNewAccount(string firstName, string lastName, string streetName, string homeNumber, string cityName, string postalCode, string provinceName, string countryName,
        string username, string password, string aptNumber = null);

    Account LogIntoAccount(string username, string password);

    void DisplayAccountBalance(Account account);

    void DisplayAllAccountInfo(Account account);
}