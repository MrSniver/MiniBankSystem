using System.Text.RegularExpressions;

namespace MiniBankSystem.InputControl;

public class StringInputControl
{
    /// <summary>
    /// Funkcja sprawdzająca, czy w input wprowadzane są tylko litery z polskimi znakami
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool OnlyLettersInput(string input)
    {
        if(Regex.IsMatch(input ?? "", @"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$"))
            return true;

        return false;
    }
}