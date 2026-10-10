namespace CrazyZoo.Core;

public abstract class Animal
{
    protected Animal(string name, int age)
    {
        AnimalValidator.ValidateName(name);
        AnimalValidator.ValidateAge(age);

        Name = name.Trim();
        Age = age;
        Energy = 100;
        Happiness = 50;
    }

    public string Name { get; }
    public int Age { get; }
    public int Energy { get; protected set; }
    public int Happiness { get; protected set; }
    public abstract string TypeName { get; }
    public abstract string FavoriteFood { get; }

    public virtual string Act()
    {
        EnsureEnoughEnergy(5);
        Energy -= 5;
        Happiness = Math.Min(100, Happiness + 3);
        return $"{Name} tegi tavalise loomategevuse.";
    }

    public abstract string CrazyAction();

    public string Eat()
    {
        Energy = Math.Min(100, Energy + 20);
        Happiness = Math.Min(100, Happiness + 5);
        return $"{Name} sõi toitu: {FavoriteFood}. Energia on nüüd {Energy}.";
    }

    protected void EnsureEnoughEnergy(int requiredEnergy)
    {
        if (Energy < requiredEnergy)
            throw new InvalidOperationException($"{Name} vajab vähemalt {requiredEnergy} energiapunkti.");
    }
}
