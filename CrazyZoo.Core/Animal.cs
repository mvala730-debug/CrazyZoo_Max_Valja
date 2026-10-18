using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CrazyZoo.Core;

public abstract class Animal : INotifyPropertyChanged
{
    private int energy;
    private int happiness;

    protected Animal(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2 || name.Trim().Length > 30)
            throw new ArgumentException("Nimi peab olema 2-30 märki.", nameof(name));

        if (age < 0 || age > 150)
            throw new ArgumentOutOfRangeException(nameof(age), "Vanus peab olema 0-150 aastat.");

        Name = name.Trim();
        Age = age;
        energy = 100;
        happiness = 50;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<string>? ReactionCreated;

    public string Name { get; }
    public int Age { get; }
    public abstract string TypeName { get; }
    public abstract string FavoriteFood { get; }

    public int Energy
    {
        get => energy;
        protected set
        {
            energy = value;
            OnPropertyChanged();
        }
    }

    public int Happiness
    {
        get => happiness;
        protected set
        {
            happiness = value;
            OnPropertyChanged();
        }
    }

    public virtual string Act()
    {
        UseEnergy(5);
        return $"{Name} tegi tavalise loomategevuse.";
    }

    public abstract string CrazyAction();

    public string Eat()
    {
        Energy = Math.Min(100, Energy + 20);
        Happiness = Math.Min(100, Happiness + 5);
        return $"{Name} sõi toitu: {FavoriteFood}.";
    }

    public void ReactToAdded(object? sender, AnimalEventArgs eventArgs)
    {
        if (!ReferenceEquals(this, eventArgs.Animal))
            ReactionCreated?.Invoke(this, $"{Name} tervitab uut looma {eventArgs.Animal.Name}.");
    }

    public void ReactToRemoving(object? sender, AnimalEventArgs eventArgs)
    {
        if (!ReferenceEquals(this, eventArgs.Animal))
            ReactionCreated?.Invoke(this, $"{Name} jätab loomaga {eventArgs.Animal.Name} hüvasti.");
    }

    public void ReactToCrazyAction(object? sender, AnimalEventArgs eventArgs)
    {
        if (!ReferenceEquals(this, eventArgs.Animal))
            ReactionCreated?.Invoke(this, $"{Name} reageerib looma {eventArgs.Animal.Name} hullule tegevusele.");
    }

    protected void UseEnergy(int amount)
    {
        if (Energy < amount)
            throw new InvalidOperationException($"{Name} vajab vähemalt {amount} energiapunkti.");

        Energy -= amount;
        Happiness = Math.Min(100, Happiness + amount / 2);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
