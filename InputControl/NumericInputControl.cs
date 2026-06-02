using System.Text.RegularExpressions;

namespace MiniBankSystem.InputControl;

public class NumericInputControl
{
    /// <summary>
    /// Funkcja sprawdzająca czy w input są tylko pozytywne cyfry
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool OnlyPositiveNumbersInput(string input)
    {
        if(Regex.IsMatch(input ?? "", @"^\d+$"))
            return true;

        return false;
    }

    /// <summary>
    /// Funkcja sprawdzająca czy w input jest liczba do dwóch miejsc po przecinku
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool NumberWithPrecisionTwoInput(string input)
    {
        if(Regex.IsMatch(input ?? "", @"^\d+(\.\d{1,2})?$"))
            return true;
        
        return false;
    }
}