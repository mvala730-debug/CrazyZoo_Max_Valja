using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CrazyZoo.Core;
using CrazyZoo.WpfApp.Commands;

namespace CrazyZoo.WpfApp.ViewModels;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly Repository<Monkey> monkeyRepository = new();
    private readonly Repository<Penguin> penguinRepository = new();
    private readonly Repository<Lion> lionRepository = new();
    private readonly Repository<Rabbit> rabbitRepository = new();
    private readonly Repository<Parrot> parrotRepository = new();
    private Animal? selectedAnimal;
    private string selectedType = "Monkey";
    private string nameInput = string.Empty;
    private string ageInput = string.Empty;
    private string searchText = string.Empty;
    private string selectedSort = "Name A-Z";
    private string statusText = string.Empty;

    public MainViewModel()
    {
        AnimalTypes = new ObservableCollection<string> { "Monkey", "Penguin", "Lion", "Rabbit", "Parrot" };
        SortOptions = new ObservableCollection<string> { "Name A-Z", "Name Z-A", "Age ascending", "Age descending" };
        Animals = new ObservableCollection<Animal>();
        VisibleAnimals = new ObservableCollection<Animal>();
        GroupSummary = new ObservableCollection<string>();
        Log = new ObservableCollection<string>();

        monkeyRepository.CrazyActionStarted += Repository_CrazyActionStarted;
        penguinRepository.CrazyActionStarted += Repository_CrazyActionStarted;
        lionRepository.CrazyActionStarted += Repository_CrazyActionStarted;
        rabbitRepository.CrazyActionStarted += Repository_CrazyActionStarted;
        parrotRepository.CrazyActionStarted += Repository_CrazyActionStarted;

        AddCommand = new RelayCommand(_ => AddAnimal());
        RemoveCommand = new RelayCommand(_ => RemoveAnimal(), _ => SelectedAnimal is not null);
        ActCommand = new RelayCommand(_ => RunAct(), _ => SelectedAnimal is not null);
        CrazyActionCommand = new RelayCommand(_ => RunCrazyAction(), _ => SelectedAnimal is not null);
        EatCommand = new RelayCommand(_ => Eat(), _ => SelectedAnimal is not null);
        RefreshCommand = new RelayCommand(_ => RefreshViews());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<string> AnimalTypes { get; }
    public ObservableCollection<string> SortOptions { get; }
    public ObservableCollection<Animal> Animals { get; }
    public ObservableCollection<Animal> VisibleAnimals { get; }
    public ObservableCollection<string> GroupSummary { get; }
    public ObservableCollection<string> Log { get; }

    public RelayCommand AddCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand ActCommand { get; }
    public RelayCommand CrazyActionCommand { get; }
    public RelayCommand EatCommand { get; }
    public RelayCommand RefreshCommand { get; }

    public string SelectedType
    {
        get => selectedType;
        set
        {
            selectedType = value;
            OnPropertyChanged();
        }
    }

    public string NameInput
    {
        get => nameInput;
        set
        {
            nameInput = value;
            OnPropertyChanged();
        }
    }

    public string AgeInput
    {
        get => ageInput;
        set
        {
            ageInput = value;
            OnPropertyChanged();
        }
    }

    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged();
            RefreshViews();
        }
    }

    public string SelectedSort
    {
        get => selectedSort;
        set
        {
            selectedSort = value;
            OnPropertyChanged();
            RefreshViews();
        }
    }

    public Animal? SelectedAnimal
    {
        get => selectedAnimal;
        set
        {
            selectedAnimal = value;
            OnPropertyChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            ActCommand.RaiseCanExecuteChanged();
            CrazyActionCommand.RaiseCanExecuteChanged();
            EatCommand.RaiseCanExecuteChanged();
        }
    }

    public string StatusText
    {
        get => statusText;
        private set
        {
            statusText = value;
            OnPropertyChanged();
        }
    }

    private void AddAnimal()
    {
        StatusText = string.Empty;

        if (!int.TryParse(AgeInput, out int age))
        {
            StatusText = "Vanus peab olema täisarv.";
            return;
        }

        try
        {
            Animal animal = AnimalFactory.Create(SelectedType, NameInput, age);
            animal.ReactionCreated += Animal_ReactionCreated;

            switch (animal)
            {
                case Monkey monkey:
                    monkeyRepository.Add(monkey);
                    break;
                case Penguin penguin:
                    penguinRepository.Add(penguin);
                    break;
                case Lion lion:
                    lionRepository.Add(lion);
                    break;
                case Rabbit rabbit:
                    rabbitRepository.Add(rabbit);
                    break;
                case Parrot parrot:
                    parrotRepository.Add(parrot);
                    break;
            }

            Animals.Add(animal);
            AddLog($"{animal.TypeName} {animal.Name} lisati.");
            NameInput = string.Empty;
            AgeInput = string.Empty;
            RefreshViews();
        }
        catch (ArgumentException exception)
        {
            StatusText = exception.Message;
        }
    }

    private void RemoveAnimal()
    {
        if (SelectedAnimal is null)
            return;

        Animal animal = SelectedAnimal;
        bool removed = animal switch
        {
            Monkey monkey => monkeyRepository.Remove(monkey),
            Penguin penguin => penguinRepository.Remove(penguin),
            Lion lion => lionRepository.Remove(lion),
            Rabbit rabbit => rabbitRepository.Remove(rabbit),
            Parrot parrot => parrotRepository.Remove(parrot),
            _ => false
        };

        if (!removed)
            return;

        animal.ReactionCreated -= Animal_ReactionCreated;
        Animals.Remove(animal);
        AddLog($"{animal.TypeName} {animal.Name} eemaldati.");
        SelectedAnimal = null;
        RefreshViews();
    }

    private void RunAct()
    {
        if (SelectedAnimal is null)
            return;

        try
        {
            AddLog(SelectedAnimal.Act());
            StatusText = string.Empty;
        }
        catch (InvalidOperationException exception)
        {
            StatusText = exception.Message;
        }
    }

    private void RunCrazyAction()
    {
        if (SelectedAnimal is null)
            return;

        try
        {
            switch (SelectedAnimal)
            {
                case Monkey monkey:
                    monkeyRepository.RunCrazyAction(monkey);
                    break;
                case Penguin penguin:
                    penguinRepository.RunCrazyAction(penguin);
                    break;
                case Lion lion:
                    lionRepository.RunCrazyAction(lion);
                    break;
                case Rabbit rabbit:
                    rabbitRepository.RunCrazyAction(rabbit);
                    break;
                case Parrot parrot:
                    parrotRepository.RunCrazyAction(parrot);
                    break;
            }
        }
        catch (InvalidOperationException exception)
        {
            StatusText = exception.Message;
        }
    }

    private void Repository_CrazyActionStarted(object? sender, AnimalEventArgs eventArgs)
    {
        AddLog(eventArgs.Message);
    }

    private void Eat()
    {
        if (SelectedAnimal is null)
            return;

        AddLog(SelectedAnimal.Eat());
    }

    private void RefreshViews()
    {
        IEnumerable<Animal> query = Animals;

        if (!string.IsNullOrWhiteSpace(SearchText))
            query = query.Where(animal => animal.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        query = SelectedSort switch
        {
            "Name Z-A" => query.OrderByDescending(animal => animal.Name),
            "Age ascending" => query.OrderBy(animal => animal.Age),
            "Age descending" => query.OrderByDescending(animal => animal.Age),
            _ => query.OrderBy(animal => animal.Name)
        };

        ReplaceCollection(VisibleAnimals, query);

        IEnumerable<string> summary = Animals
            .GroupBy(animal => animal.TypeName)
            .OrderBy(group => group.Key)
            .Select(group => $"{group.Key}: {group.Count()}");

        ReplaceCollection(GroupSummary, summary);
    }

    private void Animal_ReactionCreated(object? sender, string message)
    {
        AddLog(message);
    }

    private void AddLog(string message)
    {
        Log.Insert(0, $"{DateTime.Now:HH:mm:ss}  {message}");
    }

    private static void ReplaceCollection<T>(ObservableCollection<T> collection, IEnumerable<T> items)
    {
        collection.Clear();
        foreach (T item in items)
            collection.Add(item);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
