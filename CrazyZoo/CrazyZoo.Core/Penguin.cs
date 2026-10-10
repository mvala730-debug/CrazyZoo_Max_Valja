namespace CrazyZoo.Core;

public sealed class Penguin : Animal, ISwimmable
{
    public Penguin(string name, int age) : base(name, age) { }

    public override string TypeName => "Penguin";
    public override string FavoriteFood => "kala";

    public override string Act()
    {
        EnsureEnoughEnergy(6);
        Energy -= 6;
        Happiness = Math.Min(100, Happiness + 7);
        return $"{Name} libises kõhul üle jää ja tegi täiusliku pöörde.";
    }

    public override string CrazyAction()
    {
        EnsureEnoughEnergy(18);
        Energy -= 18;
        Happiness = Math.Min(100, Happiness + 18);
        return $"{Name} korraldas külmkapis diskopidu ja kutsus kõik kalad.";
    }

    public string Swim()
    {
        EnsureEnoughEnergy(9);
        Energy -= 9;
        Happiness = Math.Min(100, Happiness + 10);
        return $"{Name} ujus basseinis kolm kiiret ringi.";
    }
}
