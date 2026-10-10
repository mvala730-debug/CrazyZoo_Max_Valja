namespace CrazyZoo.Core;

public static class AnimalValidator
{
    public static void ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nimi ei tohi olla tühi.", nameof(name));

        string trimmedName = name.Trim();
        if (trimmedName.Length < 2 || trimmedName.Length > 30)
            throw new ArgumentException("Nimi peab olema 2-30 märki.", nameof(name));
    }

    public static void ValidateAge(int age)
    {
        if (age < 0 || age > 150)
            throw new ArgumentOutOfRangeException(nameof(age), "Vanus peab olema 0-150 aastat.");
    }

    public static bool TryParseAge(string ageText, out int age, out string errorMessage)
    {
        age = 0;
        errorMessage = string.Empty;

        if (!int.TryParse(ageText, out age))
        {
            errorMessage = "Vanus peab olema täisarv.";
            return false;
        }

        if (age < 0 || age > 150)
        {
            errorMessage = "Vanus peab olema 0-150 aastat.";
            return false;
        }

        return true;
    }
}
