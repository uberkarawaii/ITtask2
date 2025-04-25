using Avalonia.Media.Imaging;
using ReactiveUI;
using System;

namespace AnimalsApp.Models {
    public abstract class Animal : ReactiveObject
    {
        private double _speed;
        private double _positionX;
        private double _positionY;
        private double _defaultSpeed;

        public double Speed
        {
            get => _speed;
            set => this.RaiseAndSetIfChanged(ref _speed, value);
        }
        public double DefaultSpeed
        {
            get => _defaultSpeed;
            set => this.RaiseAndSetIfChanged(ref _defaultSpeed, value);
        }

        public double PositionX
        {
            get => _positionX;
            set => this.RaiseAndSetIfChanged(ref _positionX, value);
        }

        public double PositionY
        {
            get => _positionY;
            set => this.RaiseAndSetIfChanged(ref _positionY, value);
        }

        // Это свойство будет использоваться для привязки в XAML
        public Bitmap Image { get; set; }

        public abstract void Move();
        public abstract void Stand();
        public double DirectionX { get; set; } = 1;
    }
}

