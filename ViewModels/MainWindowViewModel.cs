using AnimalsApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace AnimalsApp.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    [ObservableProperty] private string pantherVoiceText = "";
    [ObservableProperty] private bool isPVoiceVisible = false;
    [ObservableProperty] private bool isOnTree = false;

    [ObservableProperty] private string dogVoiceText = "";
    [ObservableProperty] private bool isDVoiceVisible = false;

    public Panther Panther { get; } = new() { PositionX = 90, PositionY = 50 };
    public Dog Dog { get; } = new() { PositionX = 90, PositionY = 250 };
    public Turtle Turtle { get; } = new() { PositionX = 90, PositionY = 400 };

    public double PantherX => Panther.PositionX;
    public double PantherY => Panther.PositionY;
    public Bitmap PantherImage => Panther.Image;

    public double DogX => Dog.PositionX;
    public double DogY => Dog.PositionY;
    public Bitmap DogImage => Dog.Image;

    public double TurtleX => Turtle.PositionX;
    public double TurtleY => Turtle.PositionY;
    public Bitmap TurtleImage => Turtle.Image;

    public IRelayCommand PantherMoveCommand { get; }
    public IRelayCommand PantherStopCommand { get; }
    public IRelayCommand PantherRoarCommand { get; }
    public IRelayCommand PantherClimbCommand { get; }

    public IRelayCommand DogMoveCommand { get; }
    public IRelayCommand DogStopCommand { get; }
    public IRelayCommand DogBarkCommand { get; }

    public IRelayCommand TurtleMoveCommand { get; }
    public IRelayCommand TurtleStopCommand { get; }

    public MainWindowViewModel()
    {
        Panther.VoiceEvent += OnPantherRoar;
        Dog.BarkEvent += OnDogBark;

        PantherMoveCommand = new RelayCommand(() =>
        {
            if (isOnTree) DescendFromTree();
            Panther.Move();
        });

        PantherStopCommand = new RelayCommand(() => Panther.Stand());
        PantherRoarCommand = new RelayCommand(() => Panther.Roar());
        PantherClimbCommand = new RelayCommand(() =>
        {
            if (!isOnTree) ClimbTree();
        });

        DogMoveCommand = new RelayCommand(() => Dog.Move());
        DogStopCommand = new RelayCommand(() => Dog.Stand());
        DogBarkCommand = new RelayCommand(() => Dog.Bark());

        TurtleMoveCommand = new RelayCommand(() => Turtle.Move());
        TurtleStopCommand = new RelayCommand(() => Turtle.Stand());

        _ = StartMovementLoop();
    }

    private async Task StartMovementLoop()
    {
        while (true)
        {
            await Task.Delay(100);

            if (Panther.Speed > 0)
            {
                Move(Panther);
                OnPropertyChanged(nameof(PantherX));
            }
            if (Dog.Speed > 0)
            {
                Move(Dog);
                OnPropertyChanged(nameof(DogX));
            }
            if (Turtle.Speed > 0)
            {
                Move(Turtle);
                OnPropertyChanged(nameof(TurtleX));
            }
        }
    }

    private void ClimbTree()
    {
        if (Panther is Panther p)
        {
            p.ClimbTree();
            p.PositionX = 700;
            p.PositionY = 50;
            isOnTree = true;

            OnPropertyChanged(nameof(PantherX));
            OnPropertyChanged(nameof(PantherY));
        }
    }

    private void DescendFromTree()
    {
        Panther.PositionX = 10;
        Panther.PositionY = 50;
        isOnTree = false;

        OnPropertyChanged(nameof(PantherX));
        OnPropertyChanged(nameof(PantherY));
    }

    private void Move(Animal animal)
    {
        double maxWidth = 800;
        double animalWidth = 100;

        if (animal.PositionX <= 0 || animal.PositionX >= maxWidth - animalWidth)
        {
            animal.DirectionX *= -1; // разворот именно этого животного
        }

        animal.PositionX += animal.DirectionX * animal.Speed;
    }

    private async void OnPantherRoar()
    {
        PantherVoiceText = "R-R-R-R-R!";
        isPVoiceVisible = true;
        OnPropertyChanged(nameof(PantherVoiceText)); // Это важный момент
        OnPropertyChanged(nameof(IsPVoiceVisible));

        await Task.Delay(3000);
        PantherVoiceText = "";
        isPVoiceVisible = false;
        OnPropertyChanged(nameof(PantherVoiceText)); // Для обновления UI
        OnPropertyChanged(nameof(IsPVoiceVisible));
    }

    private async void OnDogBark()
    {
        DogVoiceText = "GAV-GAV-GAV-GAV-GAV!";
        isDVoiceVisible = true;
        OnPropertyChanged(nameof(DogVoiceText));
        OnPropertyChanged(nameof(IsDVoiceVisible));

        await Task.Delay(3000);
        DogVoiceText = "";
        isDVoiceVisible = false;
        OnPropertyChanged(nameof(DogVoiceText));
        OnPropertyChanged(nameof(IsDVoiceVisible));
    }

}
