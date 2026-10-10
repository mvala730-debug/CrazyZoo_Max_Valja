
namespace CrazyZoo.Core;

public sealed class Rabbit : Animal
{
    public Rabbit(string name, int age) : base(name, age) { }

    public override string TypeName => "Rabbit";
    public override string FavoriteFood => "porgand";

    public override string Act()
    {
        EnsureEnoughEnergy(5);
        Energy -= 5;
        Happiness = Math.Min(100, Happiness + 6);
        return $"{Name} hüppas ringi ja sõi porgandit";
    }

    public override string CrazyAction()
    {
        EnsureEnoughEnergy(15);
        Energy -= 15;
        Happiness = Math.Min(100, Happiness + 10);
        return $"{Name} varastas kõik loomaaia porgandid ja põgenes";
    }
}
