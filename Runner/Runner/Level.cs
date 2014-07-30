using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Runner
{
    class Level
    {
        public Background Background { get; set; }
        public Texture2D[] ObstacleTextures { get; set; }

        public Player Player { get; set; }

        public List<Obstacle> Obstacles { get; set; }

        public Input Input;

        public SoundEffect JumpSound { get; set; }
        public SoundEffect Coin { get; set; }

        public bool Paused { get; set; }

        bool lost = false;

        int counter = 0;

        int score = 0;
        int maxScore = 0;
        bool highScore = false;

        public Level(Game game)
        {
            Paused = true;

            Player = new Player();
            Player.Texture = game.Content.Load<Texture2D>("guy");

            Background = new Background();
            Background.Texture = game.Content.Load<Texture2D>("back2");

            ObstacleTextures = new Texture2D[2];
            ObstacleTextures[0] = game.Content.Load<Texture2D>("tevez1");
            ObstacleTextures[1] = game.Content.Load<Texture2D>("tevez2");

            JumpSound = game.Content.Load<SoundEffect>("Sounds/jump");
            Coin = game.Content.Load<SoundEffect>("Sounds/coin");

            Obstacles = new List<Obstacle>();

            Input = new Input();
        }

        public void Update()
        {
            Input.Update();

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

            Background.Update();

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
            Background.Draw();

            Obstacles.ForEach(o => o.Draw());
            Player.Draw();

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
            if (Util.Random.NextDouble() < 0.1)
            {
                return;
            }
            float y = Util.Random.NextDouble() > 0.5 ? 0 : Util.Height - ObstacleTextures[0].Height - 128;
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
                if (Player.Hitbox.Intersects(o.Hitbox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
