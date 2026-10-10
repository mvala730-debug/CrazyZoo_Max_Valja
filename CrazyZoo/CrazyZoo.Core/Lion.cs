namespace CrazyZoo.Core;

public sealed class Lion : Animal
{
    public Lion(string name, int age) : base(name, age) { }

    public override string TypeName => "Lion";
    public override string FavoriteFood => "liha";

    public override string Act()
    {
        EnsureEnoughEnergy(7);
        Energy -= 7;
        Happiness = Math.Min(100, Happiness + 5);
        return $"{Name} möirgas nii valjult, et piletikassa printer hakkas tööle.";
    }

    public override string CrazyAction()
    {
        EnsureEnoughEnergy(22);
        Energy -= 22;
        Happiness = Math.Min(100, Happiness + 12);
        return $"{Name} pani pähe krooni ja nõudis loomaaialt kuninglikku Wi-Fi parooli.";
    }
}
