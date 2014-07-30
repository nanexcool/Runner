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
        public Background()
        {
            Scale = 3;
        }

        public override void Update()
        {
            Position -= Vector2.UnitX * 0.5f;
            if (Position.X <= -Texture.Width * Scale)
            {
                Position = Vector2.Zero;
            }
            base.Update();
        }

        public override void Draw()
        {
            Util.SpriteBatch.Draw(Texture, Position, SourceRect, Color, Rotation, Origin, Scale, Effects, 0);
            Util.SpriteBatch.Draw(Texture, Position + new Vector2(Texture.Width * Scale, 0), SourceRect, Color, Rotation, Origin, Scale, Effects, 0);
        }
    }
}
