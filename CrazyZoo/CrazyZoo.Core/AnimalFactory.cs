namespace CrazyZoo.Core;

public static class AnimalFactory
{
    public static Animal Create(string type, string name, int age)
    {
        return type switch
        {
            "Monkey" => new Monkey(name, age),
            "Penguin" => new Penguin(name, age),
            "Lion" => new Lion(name, age),
            "Rabbit" => new Rabbit(name, age),
            "Parrot" => new Parrot(name, age),
            "Bear" => new Bear(name, age),
            "Fox" => new Fox(name, age),
            "Turtle" => new Turtle(name, age),
            _ => throw new ArgumentException("Vali looma tüüp.")
        };
    }
}