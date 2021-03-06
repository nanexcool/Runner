﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    class Obstacle : Thing
    {
        float Speed = 10f;

        public bool Collidable { get; set; }

        public Obstacle()
        {
            Collidable = true;
        }

        public override void Update()
        {
            Position -= Vector2.UnitX * Speed;
            if (Position.X + Width < 0)
            {
                Active = false;
            }
        }
    }
}
