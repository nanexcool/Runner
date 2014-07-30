using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Runner
{
    static class Util
    {
        public static Texture2D Texture { get; set; }
        public static SpriteFont Font { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static Random Random = new Random();

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void LoadContent(Game game)
        {
            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[1] { Color.White });

            Font = game.Content.Load<SpriteFont>("Font");

            Width = game.GraphicsDevice.PresentationParameters.BackBufferWidth;
            Height = game.GraphicsDevice.PresentationParameters.BackBufferHeight;
        }

        public static void DrawText(string text, Vector2 position)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 && y != 0)
                    {
                        Util.SpriteBatch.DrawString(Util.Font, text, new Vector2(position.X + x, position.Y + y), Color.White);
                    }
                }
            }
            Util.SpriteBatch.DrawString(Util.Font, text.ToString(), position, Color.Red);
        }
    }
}
