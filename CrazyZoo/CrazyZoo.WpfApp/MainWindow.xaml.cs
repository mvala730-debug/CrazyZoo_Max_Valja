using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CrazyZoo.Core;

namespace CrazyZoo.WpfApp;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<Animal> animals = new();

    public MainWindow()
    {
        InitializeComponent();
        AnimalTypeComboBox.ItemsSource = new[] { "Monkey", "Penguin", "Lion", "Parrot" };
        AnimalsListBox.ItemsSource = animals;
        UpdateDetails();
    }

    private Animal? SelectedAnimal => AnimalsListBox.SelectedItem as Animal;

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        ValidationTextBlock.Text = string.Empty;

        if (AnimalTypeComboBox.SelectedItem is not string animalType)
        {
            ValidationTextBlock.Text = "Vali looma tüüp.";
            return;
        }

        if (!AnimalValidator.TryParseAge(AgeTextBox.Text, out int age, out string ageError))
        {
            ValidationTextBlock.Text = ageError;
            return;
        }

        try
        {
            Animal animal = AnimalFactory.Create(animalType, NameTextBox.Text, age);
            animals.Add(animal);
            AnimalsListBox.SelectedItem = animal;
            AddLog($"Lisatud {animal.TypeName}: {animal.Name}.");
            ClearInputs();
        }
        catch (ArgumentException exception)
        {
            ValidationTextBlock.Text = exception.Message;
        }
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        Animal? animal = SelectedAnimal;
        if (animal is null)
        {
            ValidationTextBlock.Text = "Vali eemaldatav loom.";
            return;
        }

        animals.Remove(animal);
        AddLog($"Eemaldatud {animal.TypeName}: {animal.Name}.");
        ValidationTextBlock.Text = string.Empty;
        UpdateDetails();
    }

    private void AnimalsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ValidationTextBlock.Text = string.Empty;
        UpdateDetails();
    }

    private void ActButton_Click(object sender, RoutedEventArgs e) => RunAction(animal => animal.Act());
    private void CrazyButton_Click(object sender, RoutedEventArgs e) => RunAction(animal => animal.CrazyAction());
    private void EatButton_Click(object sender, RoutedEventArgs e) => RunAction(animal => animal.Eat());

    private void SwimButton_Click(object sender, RoutedEventArgs e)
    {
        RunAbility<ISwimmable>(swimmer => swimmer.Swim());
    }

    private void ClimbButton_Click(object sender, RoutedEventArgs e)
    {
        RunAbility<IClimb>(climber => climber.Climb());
    }

    private void FlyButton_Click(object sender, RoutedEventArgs e)
    {
        RunAbility<IFly>(flyer => flyer.Fly());
    }

    private void RunAction(Func<Animal, string> action)
    {
        Animal? animal = SelectedAnimal;
        if (animal is null)
        {
            ValidationTextBlock.Text = "Vali loom.";
            return;
        }

        try
        {
            string message = action(animal);
            AddLog(message);
            ValidationTextBlock.Text = string.Empty;
            UpdateDetails();
        }
        catch (InvalidOperationException exception)
        {
            ValidationTextBlock.Text = exception.Message;
            AddLog($"Tegevus ebaõnnestus: {exception.Message}");
        }
    }

    private void RunAbility<TAbility>(Func<TAbility, string> action)
    {
        Animal? animal = SelectedAnimal;
        if (animal is null)
        {
            ValidationTextBlock.Text = "Vali loom.";
            return;
        }

        if (animal is not TAbility ability)
        {
            ValidationTextBlock.Text = $"Loomal {animal.Name} puudub see võime.";
            return;
        }

        try
        {
            AddLog(action(ability));
            ValidationTextBlock.Text = string.Empty;
            UpdateDetails();
        }
        catch (InvalidOperationException exception)
        {
            ValidationTextBlock.Text = exception.Message;
            AddLog($"Võime kasutamine ebaõnnestus: {exception.Message}");
        }
    }

    private void UpdateDetails()
    {
        Animal? animal = SelectedAnimal;
        bool selected = animal is not null;

        TypeValueTextBlock.Text = animal?.TypeName ?? "-";
        NameValueTextBlock.Text = animal?.Name ?? "-";
        AgeValueTextBlock.Text = animal is null ? "-" : $"{animal.Age} aastat";
        FoodValueTextBlock.Text = animal?.FavoriteFood ?? "-";
        HappinessValueTextBlock.Text = animal is null ? "-" : animal.Happiness.ToString();
        EnergyProgressBar.Value = animal?.Energy ?? 0;
        EnergyValueTextBlock.Text = animal is null ? "0 / 100" : $"{animal.Energy} / 100";

        List<string> abilities = new();
        if (animal is ISwimmable) abilities.Add("ISwimmable");
        if (animal is IClimb) abilities.Add("IClimb");
        if (animal is IFly) abilities.Add("IFly");
        AbilitiesValueTextBlock.Text = abilities.Count == 0 ? "puuduvad" : string.Join(", ", abilities);

        SwimButton.IsEnabled = selected && animal is ISwimmable;
        ClimbButton.IsEnabled = selected && animal is IClimb;
        FlyButton.IsEnabled = selected && animal is IFly;
    }

    private void AddLog(string message)
    {
        LogListBox.Items.Insert(0, $"{DateTime.Now:HH:mm:ss}  {message}");
    }

    private void ClearInputs()
    {
        AnimalTypeComboBox.SelectedIndex = -1;
        NameTextBox.Clear();
        AgeTextBox.Clear();
        NameTextBox.Focus();
    }
}
