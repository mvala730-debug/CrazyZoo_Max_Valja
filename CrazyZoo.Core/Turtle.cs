namespace CrazyZoo.Core;

public class Turtle : Animal
{
    public Turtle(string name, int age) : base(name, age)
    {
    }

    public override string TypeName => "Turtle";
    public override string FavoriteFood => "salat";

    public override string Act()
    {
        UseEnergy(3);
        return Name + " jalutas aeglaselt.";
    }

    public override string CrazyAction()
    {
        UseEnergy(10);
        return Name + " kutsus kõik loomad jooksuvõistlusele.";
    }

    protected override string GetCrazyReaction(Animal other)
    {
        Happiness = Math.Min(100, Happiness + 1);
        return Name + " plaksutab loomale " + other.Name + " väga aeglaselt.";
    }
}