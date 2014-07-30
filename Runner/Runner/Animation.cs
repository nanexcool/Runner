using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Runner
{
    class Animation
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Frames { get; set; }
        public int FPS { get; set; }

        public Rectangle SourceRect
        {
            get
            {
                return new Rectangle(index * Width, 0, Width, Height);
            }
        }

        int index = 0;
        int counter = 0;

        public Animation(string name, int frames, int fps, int width, int height)
        {
            Name = name;
            Frames = frames;
            FPS = fps;
            Width = width;
            Height = height;
        }

        public void Update()
        {
            if (counter++ >= FPS)
            {
                index = ++index >= Frames ? 0 : index;
                counter = 0;
            }
        }
    }
}
