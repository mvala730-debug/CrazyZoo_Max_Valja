namespace CrazyZoo.Core;

public sealed class Lion : Animal
{
    public Lion(string name, int age) : base(name, age) { }
    public override string TypeName => "Lion";
    public override string FavoriteFood => "liha";

    public override string Act()
    {
        UseEnergy(7);
        return $"{Name} möirgas nii valjult, et aknad värisesid.";
    }

    public override string CrazyAction()
    {
        UseEnergy(22);
        return $"{Name} pani pähe krooni ja nõudis kuninglikku Wi-Fi parooli.";
    }
}
