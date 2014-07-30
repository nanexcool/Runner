using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    class Player : Thing
    {
        bool Jumping = false;
        float jumpPosition;

        Vector2 velocity = Vector2.Zero;

        public override Rectangle? SourceRect
        {
            get
            {
                return a.SourceRect;
            }
        }

        public override Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Math.Floor(Position.X), (int)Math.Floor(Position.Y), a.Width, a.Height);
            }
        }

        Animation a;

        Vector2 originalPosition = new Vector2(0, 240);

        public Player()
        {
            Position = originalPosition;
            jumpPosition = Position.Y;
            //SourceRect = new Rectangle(0, 0, 108, 140);

            a = new Animation("running_right", 8, 4, 108, 140);
        }

        public override void Update()
        {
            base.Update();
            a.Update();
            velocity.Y += 1f;
            Position.Y += velocity.Y;

            if (Position.Y > jumpPosition)
            {
                Position.Y = jumpPosition;
                Jumping = false;
            }
        }

        public void Reset()
        {
            Position = originalPosition;
            jumpPosition = Position.Y;
            velocity.Y = 0;
        }

        public override void Draw()
        {
            base.Draw();
            
        }

        public bool Jump()
        {
            if (!Jumping)
            {
                Jumping = true;
                jumpPosition = Position.Y;
                velocity.Y = -24;
                return true;
            }
            return false;
        }
    }
}
