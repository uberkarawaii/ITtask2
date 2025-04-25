using Avalonia.Media.Imaging;
using System;

namespace AnimalsApp.Models
{
    public class Panther : Animal
    {
        public event Action? VoiceEvent;
        public double DirectionX { get; set; } = 1;
        public Panther()
        {
            Image = new Bitmap("Assets/panther.png");
            Speed = 0;  
            DefaultSpeed = 10;
        }

        public override void Move()
        {
            Speed += DefaultSpeed;
        }

        public override void Stand()
        {
            if (Speed != 0) Speed -= DefaultSpeed;  // замедление, если животное в движении
        }

        public void Roar()
        {
            VoiceEvent?.Invoke();
        }

        public void ClimbTree() {
            Speed = 0;
        }
    }
}