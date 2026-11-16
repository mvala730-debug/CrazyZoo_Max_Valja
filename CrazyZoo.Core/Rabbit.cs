namespace CrazyZoo.Core;

public sealed class Rabbit : Animal
{
    public Rabbit(string name, int age) : base(name, age) { }
    public override string TypeName => "Rabbit";
    public override string FavoriteFood => "porgand";

    public override string Act()
    {
        UseEnergy(5);
        return $"{Name} hüppas kolm korda.";
    }

    public override string CrazyAction()
    {
        UseEnergy(10);
        return $"{Name} varastas kõik loomaaia porgandid.";
    }
}
