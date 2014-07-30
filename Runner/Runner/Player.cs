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

        public Player()
        {
            Position = new Vector2(100, Util.Height / 3);
            jumpPosition = Position.Y;
        }

        public override void Update()
        {
            base.Update();
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
            Position = new Vector2(100, Util.Height / 3);
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
