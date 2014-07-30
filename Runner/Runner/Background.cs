using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    class Background : Thing
    {
        public List<Texture2D> Textures;

        int index = 0;

        public float Speed = 1f;

        public Background()
        {
            Textures = new List<Texture2D>();
            Color = Color.White;
        }

        public override void Update()
        {
            Position -= Vector2.UnitX * Speed;
            if (Position.X <= -Textures[index].Width * Scale)
            {
                Position.X = 0;
            }
            base.Update();
        }

        public override void Draw()
        {
            Util.SpriteBatch.Draw(Textures[index], Position, SourceRect, Color, Rotation, Origin, Scale, Effects, 0);
            Util.SpriteBatch.Draw(Textures[index], Position + new Vector2(Textures[index].Width * Scale, 0), SourceRect, Color, Rotation, Origin, Scale, Effects, 0);
            //Util.SpriteBatch.Draw(Textures[index], Position + new Vector2(Textures[index].Width * Scale * 2, 0), SourceRect, Color, Rotation, Origin, Scale, Effects, 0);
        }
    }
}
