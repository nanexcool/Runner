using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Runner
{
    class Level
    {
        public Background BG { get; set; }
        public Background Road { get; set; }
        public Background Foreground { get; set; }
        public Texture2D[] ObstacleTextures { get; set; }

        public Player Player { get; set; }

        public List<Obstacle> Obstacles { get; set; }

        public Input Input;

        public Camera Camera { get; set; }

        public SoundEffect JumpSound { get; set; }
        public SoundEffect Coin { get; set; }

        public Song Song { get; set; }

        public bool Paused { get; set; }

        bool lost = false;

        int counter = 0;

        int score = 0;
        int maxScore = 0;
        bool highScore = false;

        public Level(Game game)
        {
            Paused = true;

            Player = new Player(game.Content.Load<Texture2D>("scott"));

            BG = new Background();
            BG.Textures.Add(game.Content.Load<Texture2D>("Backgrounds/Background 1a"));
            BG.Scale = 1.4f;
            BG.Speed = 0.2f;

            Road = new Background();
            Road.Textures.Add(game.Content.Load<Texture2D>("Backgrounds/road"));
            Road.Speed = 10f;
            Road.Position = new Vector2(0, 270);

            Foreground = new Background();
            Foreground.Textures.Add(game.Content.Load<Texture2D>("Backgrounds/grass"));
            Foreground.Position = new Vector2(0, 315);
            Foreground.Speed = 12f;
            Foreground.Scale = 2;

            ObstacleTextures = new Texture2D[2];
            ObstacleTextures[0] = game.Content.Load<Texture2D>("barrel");
            ObstacleTextures[1] = game.Content.Load<Texture2D>("tevez2");

            JumpSound = game.Content.Load<SoundEffect>("Sounds/jump");
            Coin = game.Content.Load<SoundEffect>("Sounds/coin");
            Song = game.Content.Load<Song>("Music/Plasticidio");

            Obstacles = new List<Obstacle>();

            Input = new Input();

            Camera = new Camera(Util.Width, Util.Height, Player);
        }

        public void Update()
        {
            Input.Update();

            Camera.Update();

            if (Input.Jump())
            {                
                if (Paused)
                {
                    Reset();
                }
                else
                {
                    if (Player.Jump())
                    {
                        JumpSound.Play();
                    }
                }
            }

            if (Paused)
            {
                return;
            }

            BG.Update();
            Road.Update();
            Foreground.Update();

            for (int i = 0; i < Obstacles.Count; i++)
            {
                Obstacles[i].Update();
                if (!Obstacles[i].Active)
                {
                    Obstacles.RemoveAt(i--);
                    score++;
                    Coin.Play();
                }
            }
            
            Player.Update();
            
            if (counter++ >= 60)
            {
                AddObstacle();
                counter = 0;
            }

            if (CheckCollisions())
            {
                Die();
            }
        }

        private void Reset()
        {
            Paused = false;
            lost = false;
            highScore = false;
            score = 0;
            Obstacles.Clear();
            Player.Reset();
        }

        private void Die()
        {
            Paused = true;
            lost = true;
            if (score > maxScore)
            {
                maxScore = score;
                highScore = true;
            }
        }

        public void Draw()
        {
            BG.Draw();
            Road.Draw();
            Obstacles.ForEach(o => o.Draw());
            Player.Draw();

            Foreground.Draw();

            if (Paused && !lost)
            {
                Util.DrawText("Press SPACE to jump!", new Vector2(150, 20));
            }

            if (lost)
            {
                if (highScore)
                {
                    Util.DrawText("NEW HIGH SCORE: " + score.ToString(), new Vector2(150, 20));
                }
                else
	            {
                    Util.DrawText("Final score: " + score.ToString(), new Vector2(150, 20));
	            }
                
            }

            DrawScore();
        }

        private void DrawScore()
        {
            Util.DrawText(score.ToString(), new Vector2(Util.Width / 2, Util.Height / 4));
        }

        private void AddObstacle()
        {
            float y = Util.Random.NextDouble() > 0.5 ? 0 : Util.Height - ObstacleTextures[0].Height - 64;
            Vector2 p = new Vector2(Util.Width - 32, y);
            Obstacles.Add(new Obstacle()
            {
                Texture = ObstacleTextures[Util.Random.Next(0, 2)],
                Position = p
            });
        }

        public bool CheckCollisions()
        {
            foreach (Obstacle o in Obstacles)
            {
                if (!o.Collidable)
                {
                    continue;
                }
                if (Player.Hitbox.Intersects(o.Hitbox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
