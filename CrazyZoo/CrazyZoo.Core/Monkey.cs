namespace CrazyZoo.Core;

public sealed class Monkey : Animal, IClimb
{
    public Monkey(string name, int age) : base(name, age) { }

    public override string TypeName => "Monkey";
    public override string FavoriteFood => "banaan";

    public override string Act()
    {
        EnsureEnoughEnergy(8);
        Energy -= 8;
        Happiness = Math.Min(100, Happiness + 8);
        return $"{Name} kiikus liaanil ja peitis loomaaia direktori võtmed.";
    }

    public override string CrazyAction()
    {
        EnsureEnoughEnergy(20);
        Energy -= 20;
        Happiness = Math.Min(100, Happiness + 15);
        return $"{Name} avas banaanipanga ja kuulutas end direktoriks.";
    }

    public string Climb()
    {
        EnsureEnoughEnergy(10);
        Energy -= 10;
        Happiness = Math.Min(100, Happiness + 6);
        return $"{Name} ronis kõige kõrgema puu otsa.";
    }
}
