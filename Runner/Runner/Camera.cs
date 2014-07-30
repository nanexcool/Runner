using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Runner
{
    class Camera
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int X { get { return (int)Math.Floor(Position.X); } }
        public int Y { get { return (int)Math.Floor(Position.Y); } }

        public Vector2 Position { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Origin { get; set; }

        public Matrix Transform { get; set; }

        public float Scale { get; set; }
        public float Rotation { get; set; }

        public Thing Focus { get; set; }

        public Camera(int width, int height, Thing focus = null)
        {
            Width = width;
            Height = height;

            Center = new Vector2(Width / 2, Height / 2);

            Scale = 1f;
            Rotation = 0;
            Origin = Center / Scale;

            Focus = focus;
        }

        public void Update()
        {
            Origin = Center / Scale;

            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-X, -Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(Scale);

            if (Focus != null)
            {
                Position = Focus.Position;
            }
        }

        public void Toggle(Thing e)
        {
            if (Focus == null)
            {
                Focus = e;
            }
            else
            {
                Focus = null;
            }
        }
    }
}
