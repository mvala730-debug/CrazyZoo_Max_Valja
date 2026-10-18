namespace CrazyZoo.Core;

public interface IRepository<T> where T : Animal
{
    event EventHandler<AnimalEventArgs>? Added;
    event EventHandler<AnimalEventArgs>? Removing;
    event EventHandler<AnimalEventArgs>? CrazyActionStarted;

    void Add(T item);
    bool Remove(T item);
    IEnumerable<T> GetAll();
    T? Find(Func<T, bool> predicate);
    void RunCrazyAction(T item);
}
