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
                return currentAnimation.SourceRect;
            }
        }

        int offset = 40;

        public override Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Math.Floor(Position.X) + offset, (int)Math.Floor(Position.Y) + offset, currentAnimation.Width - offset * 2, currentAnimation.Height - offset * 2);
            }
        }

        Animation currentAnimation;
        List<Animation> animations;

        Vector2 originalPosition = new Vector2(50, 240);

        public Player(Texture2D texture)
        {
            Texture = texture;
            Position = originalPosition;
            jumpPosition = Position.Y;
            animations = new List<Animation>();
            currentAnimation = new Animation("running_right", 8, 3, 108, 140);
            animations.Add(currentAnimation);
        }

        public override void Update()
        {
            base.Update();
            currentAnimation.Update();
            velocity.Y += 1f;
            Position.Y += velocity.Y;

            if (Position.Y > jumpPosition)
            {
                Position.Y = jumpPosition;
                Jumping = false;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void Reset()
        {
            Position = originalPosition;
            jumpPosition = Position.Y;
            velocity.Y = 0;
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
