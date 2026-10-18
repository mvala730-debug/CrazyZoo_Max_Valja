namespace CrazyZoo.Core;

public sealed class Parrot : Animal
{
    public Parrot(string name, int age) : base(name, age) { }
    public override string TypeName => "Parrot";
    public override string FavoriteFood => "seemned";

    public override string Act()
    {
        UseEnergy(5);
        return $"{Name} kordas külastajate lauseid täiesti vales järjekorras.";
    }

    public override string CrazyAction()
    {
        UseEnergy(16);
        return $"{Name} kuulutas valjuhääldist banaanipausi.";
    }
}
