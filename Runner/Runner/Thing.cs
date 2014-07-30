using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    class Thing
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position = Vector2.Zero;
        public virtual Rectangle? SourceRect
        {
            get { return null; }
        }
        public Color Color = Color.White;
        public Vector2 Origin = Vector2.Zero;
        public float Rotation = 0;
        public float Scale = 1;
        public SpriteEffects Effects = SpriteEffects.None;

        public virtual Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Math.Floor(Position.X), (int)Math.Floor(Position.Y), Width, Height);
            }
        }

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public bool Active = true;

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            Util.SpriteBatch.Draw(Texture, Position, SourceRect, Color, Rotation, Origin, Scale, Effects, 0);
            //Util.SpriteBatch.Draw(Texture, Position, Hitbox, Color.Black * 0.5f, Rotation, Origin, Scale, Effects, 0);
        }
    }
}
