namespace CrazyZoo.Core;

public class Bear : Animal
{
    public Bear(string name, int age) : base(name, age)
    {
    }

    public override string TypeName => "Bear";
    public override string FavoriteFood => "mesi";

    public override string Act()
    {
        UseEnergy(8);
        return Name + " otsis mett.";
    }

    public override string CrazyAction()
    {
        UseEnergy(15);
        return Name + " avas meepoe ja sõi ise kõik kaubad ära.";
    }

    protected override string GetCrazyReaction(Animal other)
    {
        Happiness = Math.Min(100, Happiness + 3);
        return Name + " naerab looma " + other.Name + " tegevuse üle.";
    }
}