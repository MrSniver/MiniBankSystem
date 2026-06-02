using MiniBankSystem.InputControl;

namespace MiniBankSystem.AccountActions.Validators;

public interface IValidator
{
    string GetValidatedInput(string fieldName, ValidationType validationType);
    bool IsValid(string input, ValidationType validationType);
}

public enum ValidationType
{
    LettersOnly,
    PositiveNumbers,
    NotEmpty,
    Decimal
}

public class InputValidator : IValidator //Validator pattern użyty dla walidacji inputu użytkownika
{
    private readonly StringInputControl _stringInputControl = new();
    private readonly NumericInputControl _numericInputControl = new();
    
    /// <summary>
    /// Funkcja używająca return switch w celu zwrócenia nam typu walidacji, którego potrzebujemy
    /// </summary>
    /// <param name="input"></param>
    /// <param name="validationType"></param>
    /// <returns></returns>
    public bool IsValid(string input, ValidationType validationType)
    {
        return validationType switch
        {
            ValidationType.LettersOnly => _stringInputControl.OnlyLettersInput(input),
            ValidationType.PositiveNumbers => _numericInputControl.OnlyPositiveNumbersInput(input),
            ValidationType.Decimal => _numericInputControl.NumberWithPrecisionTwoInput(input),
            ValidationType.NotEmpty => !string.IsNullOrEmpty(input),
            _ => false
        };
    }

    public string GetValidatedInput(string fieldName, ValidationType validationType)
    {
        string input;
        bool inputValid = false;

        do
        {
            Console.Write($"{fieldName}: ");
            input = Console.ReadLine();

            inputValid = IsValid(input, validationType);

            if (!inputValid)  // ← Add this check
            {
                string errorMessage = GetErrorMessage(fieldName, validationType);
                Console.WriteLine($"{errorMessage}");
            }

        }while(!inputValid);

        return input;
    }

    /// <summary>
    /// Funkcja pozwalająca na zwracanie wiadomości błędu, pozwala to na niepowtarzanie kodu
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="validationType"></param>
    /// <returns></returns>
    private string GetErrorMessage(string fieldName, ValidationType validationType)
    {
        return validationType switch
        {
            ValidationType.LettersOnly => $"Pole {fieldName} może zawierać tylko litery.",
            ValidationType.PositiveNumbers => $"Pole {fieldName} może zawierać tylko cyfry.",
            ValidationType.Decimal => $"Pole {fieldName} musi być liczbą z maksymalnie 2 miejscami po przecinku.",
            ValidationType.NotEmpty => $"Pole {fieldName} nie może być puste.",
            _ => "Niepoprawne dane."
        };
    }
}