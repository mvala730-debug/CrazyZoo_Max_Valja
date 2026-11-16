namespace CrazyZoo.Core;

public class Fox : Animal
{
    public Fox(string name, int age) : base(name, age)
    {
    }

    public override string TypeName => "Fox";
    public override string FavoriteFood => "liha";

    public override string Act()
    {
        UseEnergy(5);
        return Name + " peitis oma toidu.";
    }

    public override string CrazyAction()
    {
        UseEnergy(14);
        return Name + " müüs loomaaia direktorile nähtamatu kana.";
    }

    protected override string GetCrazyReaction(Animal other)
    {
        Happiness = Math.Min(100, Happiness + 2);
        return Name + " tahab looma " + other.Name + " trikist raha teenida.";
    }
}