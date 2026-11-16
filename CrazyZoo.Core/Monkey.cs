namespace CrazyZoo.Core;

public sealed class Monkey : Animal
{
    public Monkey(string name, int age) : base(name, age) { }
    public override string TypeName => "Monkey";
    public override string FavoriteFood => "banaan";

    public override string Act()
    {
        UseEnergy(8);
        return $"{Name} kiikus liaanil ja peitis direktori võtmed.";
    }

    public override string CrazyAction()
    {
        UseEnergy(20);
        return $"{Name} avas banaanipanga ja kuulutas end direktoriks.";
    }
}
