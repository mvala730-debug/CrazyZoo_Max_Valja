namespace CrazyZoo.Core;

public static class AnimalFactory
{
    public static Animal Create(string animalType, string name, int age)
    {
        return animalType switch
        {
            "Monkey" => new Monkey(name, age),
            "Penguin" => new Penguin(name, age),
            "Lion" => new Lion(name, age),
            "Parrot" => new Parrot(name, age),
            _ => throw new ArgumentException("Vali looma tüüp.", nameof(animalType))
        };
    }
}
