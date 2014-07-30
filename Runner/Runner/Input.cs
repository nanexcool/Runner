using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Runner
{
    class Input
    {
        KeyboardState state, oldState;

        public Input()
        {
            oldState = state = Keyboard.GetState();
        }

        public void Update()
        {
            oldState = state;
            state = Keyboard.GetState();
        }

        public bool Jump()
        {
            return IsKeyPressed(Keys.Space);
        }

        public bool IsKeyPressed(Keys k)
        {
            return state.IsKeyDown(k) && !oldState.IsKeyDown(k);
        }
    }
}
