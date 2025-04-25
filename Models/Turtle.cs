using Avalonia.Media.Imaging;
using System;

namespace AnimalsApp.Models
{
    public class Turtle : Animal
    {
        public double DirectionX { get; set; } = 1;
        public Turtle()
        {
            Image = new Bitmap("Assets/turtle.png");
            Speed = 0;
            DefaultSpeed = 2;
        }

        public override void Move()
        {
            Speed += DefaultSpeed;
        }

        public override void Stand()
        {
            if (Speed != 0) Speed -= DefaultSpeed;  // замедление, если животное в движении
        }
    }
}
