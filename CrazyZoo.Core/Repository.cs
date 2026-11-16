namespace CrazyZoo.Core;

public sealed class Repository<T> : IRepository<T> where T : Animal
{
    private readonly List<T> items = new();

    public event EventHandler<AnimalEventArgs>? Added;
    public event EventHandler<AnimalEventArgs>? Removing;
    public event EventHandler<AnimalEventArgs>? CrazyActionStarted;

    public void Add(T item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        foreach (T existing in items)
            Added += existing.ReactToAdded;

        Added?.Invoke(this, new AnimalEventArgs(item, $"{item.Name} lisati hoidlasse."));

        foreach (T existing in items)
            Added -= existing.ReactToAdded;

        items.Add(item);
    }

    public bool Remove(T item)
    {
        if (!items.Contains(item))
            return false;

        foreach (T existing in items.Where(existing => !ReferenceEquals(existing, item)))
            Removing += existing.ReactToRemoving;

        Removing?.Invoke(this, new AnimalEventArgs(item, $"{item.Name} eemaldatakse hoidlast."));

        foreach (T existing in items.Where(existing => !ReferenceEquals(existing, item)))
            Removing -= existing.ReactToRemoving;

        return items.Remove(item);
    }

    public IEnumerable<T> GetAll()
    {
        return items.ToList();
    }

    public T? Find(Func<T, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }

    public void RunCrazyAction(T item)
    {
        if (!items.Contains(item))
            throw new InvalidOperationException("Looma ei leitud hoidlast.");

        string actionMessage = item.CrazyAction();

        foreach (T existing in items.Where(existing => !ReferenceEquals(existing, item)))
            CrazyActionStarted += existing.ReactToCrazyAction;

        CrazyActionStarted?.Invoke(this, new AnimalEventArgs(item, actionMessage));

        foreach (T existing in items.Where(existing => !ReferenceEquals(existing, item)))
            CrazyActionStarted -= existing.ReactToCrazyAction;
    }
}
