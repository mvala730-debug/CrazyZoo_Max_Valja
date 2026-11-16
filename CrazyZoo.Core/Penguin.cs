namespace CrazyZoo.Core;

public sealed class Penguin : Animal
{
    public Penguin(string name, int age) : base(name, age) { }
    public override string TypeName => "Penguin";
    public override string FavoriteFood => "kala";

    public override string Act()
    {
        UseEnergy(6);
        return $"{Name} libises kõhul üle jää.";
    }

    public override string CrazyAction()
    {
        UseEnergy(18);
        return $"{Name} korraldas külmkapis diskopidu.";
    }
}
