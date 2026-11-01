namespace CrazyZoo.Core;
public abstract class Animal
{
    protected Animal(string name,int age){if(string.IsNullOrWhiteSpace(name)||name.Trim().Length<2||name.Trim().Length>30)throw new ArgumentException("Nimi peab olema 2-30 märki.");if(age<0||age>150)throw new ArgumentOutOfRangeException(nameof(age));Name=name.Trim();Age=age;Energy=100;Happiness=50;}
    public string Name{get;} public int Age{get;} public int Energy{get;protected set;} public int Happiness{get;protected set;} public abstract string TypeName{get;} public abstract string FavoriteFood{get;}
    public virtual string Act(){UseEnergy(5);return $"{Name} tegi tavalise loomategevuse.";} public abstract string CrazyAction();
    public string Eat(){Energy=Math.Min(100,Energy+20);Happiness=Math.Min(100,Happiness+5);return $"{Name} sõi: {FavoriteFood}.";}
    protected void UseEnergy(int amount){if(Energy<amount)throw new InvalidOperationException($"{Name} vajab vähemalt {amount} energiapunkti.");Energy-=amount;Happiness=Math.Min(100,Happiness+amount/2);}
}