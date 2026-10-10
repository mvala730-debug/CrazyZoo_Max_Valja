namespace CrazyZoo.Core;

public sealed class Parrot : Animal, IFly
{
    public Parrot(string name, int age) : base(name, age) { }

    public override string TypeName => "Parrot";
    public override string FavoriteFood => "seemned";

    public override string Act()
    {
        EnsureEnoughEnergy(5);
        Energy -= 5;
        Happiness = Math.Min(100, Happiness + 6);
        return $"{Name} kordas külastajate lauseid täiesti vales järjekorras.";
    }

    public override string CrazyAction()
    {
        EnsureEnoughEnergy(16);
        Energy -= 16;
        Happiness = Math.Min(100, Happiness + 14);
        return $"{Name} kuulutas valjuhääldist, et loomaaed suletakse banaanipausiks.";
    }

    public string Fly()
    {
        EnsureEnoughEnergy(8);
        Energy -= 8;
        Happiness = Math.Min(100, Happiness + 8);
        return $"{Name} lendas üle loomaaia ja maandus piletikassal.";
    }
}
