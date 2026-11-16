namespace CrazyZoo.Core;

public sealed class AnimalEventArgs : EventArgs
{
    public AnimalEventArgs(Animal animal, string message)
    {
        Animal = animal;
        Message = message;
    }

    public Animal Animal { get; }
    public string Message { get; }
}
