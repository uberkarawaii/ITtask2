using AnimalsApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace AnimalsApp.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private Animal _currentAnimal;
    private double _directionX = 1;

    [ObservableProperty]
    private string pantherVoiceText = "";

    [ObservableProperty]
    private bool isVoiceVisible = false;

    [ObservableProperty]
    private bool isOnTree = false;

    public Animal CurrentAnimal
    {
        get => _currentAnimal;
        set => SetProperty(ref _currentAnimal, value);
    }

    public double AnimalPositionX => CurrentAnimal.PositionX;
    public double AnimalPositionY => CurrentAnimal.PositionY;
    public Bitmap AnimalImage => CurrentAnimal.Image;

    public IRelayCommand ClimbTreeCommand { get; }
    public IRelayCommand MoveAnimalCommand { get; }
    public IRelayCommand StopAnimalCommand { get; }
    public IRelayCommand RoarCommand { get; }

    public MainWindowViewModel()
    {
        CurrentAnimal = new Panther
        {
            PositionX = 90,
            PositionY = 50
        };

        if (CurrentAnimal is Panther panther)
        {
            panther.VoiceEvent += OnPantherRoar;
        }

        MoveAnimalCommand = new RelayCommand(() =>
        {
            // Если на дереве, спускаем
            if (IsOnTree)
            {
                DescendFromTree();
            }

            CurrentAnimal.Move();
        });

        StopAnimalCommand = new RelayCommand(() =>
        {
            CurrentAnimal.Stand();
        });

        RoarCommand = new RelayCommand(() =>
        {
            if (CurrentAnimal is Panther p)
                p.Roar();
        });

        ClimbTreeCommand = new RelayCommand(() =>
        {
            if (!IsOnTree)
            {
                ClimbTree();
            }
        });

        _ = StartMovementLoop();
    }

    private async Task StartMovementLoop()
    {
        while (true)
        {
            await Task.Delay(100);

            if (CurrentAnimal.Speed > 0)
            {
                MoveAnimal();

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    OnPropertyChanged(nameof(AnimalPositionX));
                });
            }
        }
    }

    private void ClimbTree()
    {
        // CurrentAnimal.Stand(); // остановить перед подъёмом
        if (CurrentAnimal is Panther p)
        {
            p.ClimbTree();
            p.PositionX = 700;
            p.PositionY = 50;
            IsOnTree = true;

            OnPropertyChanged(nameof(AnimalPositionX));
            OnPropertyChanged(nameof(AnimalPositionY));
        }
    }

    private void DescendFromTree()
    {
        CurrentAnimal.PositionX = 10;
        CurrentAnimal.PositionY = 50;
        IsOnTree = false;

        OnPropertyChanged(nameof(AnimalPositionX));
        OnPropertyChanged(nameof(AnimalPositionY));
    }

    private void MoveAnimal()
    {
        double maxWidth = 800;
        double animalWidth = 100;

        if (CurrentAnimal.PositionX <= 0 || CurrentAnimal.PositionX >= maxWidth - animalWidth)
        {
            _directionX = -_directionX;
        }

        CurrentAnimal.PositionX += _directionX * CurrentAnimal.Speed;
    }

    private async void OnPantherRoar()
    {
        PantherVoiceText = "R-R-R-R-R!";
        IsVoiceVisible = true;

        await Task.Delay(3000);

        PantherVoiceText = "";
        IsVoiceVisible = false;
    }
}
