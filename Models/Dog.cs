using Avalonia.Media.Imaging;
using System;

namespace AnimalsApp.Models
{
    public class Dog : Animal
    {
        public event Action? BarkEvent;
        public Dog()
        {
            Image = new Bitmap("Assets/dog.png");
            Speed = 0;
            DefaultSpeed = 5;
        }

        public override void Move()
        {
            Speed += DefaultSpeed;
        }

        public override void Stand()
        {
            if (Speed != 0) Speed -= DefaultSpeed;  // замедление, если животное в движении
        }

        public void Bark()
        {
            BarkEvent?.Invoke();
        }
    }
}
