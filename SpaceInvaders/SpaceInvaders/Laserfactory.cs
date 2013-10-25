using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceInvaders
{
    /// <summary>
    /// this is a class that is used to generate bullets for the player
    /// </summary>
      class LaserFactory : ProjectileFactory
    {
        private List<ProjectileSprite> badBullets = new List<ProjectileSprite>();
        private Game1 game;
        private AlienSquad alien;
        private String img = "laser1";
        private Texture2D imageBullet;
        private Vector2 velocity = new Vector2(0, -2);
        public event Handler Collision1;
        int speedCount = 0;
        private ScoreSprite score;



        public LaserFactory(Game1 game)
            : base(game)
        {
            this.game = game;
        }


        /// <summary>
        /// Initializes the class
        /// </summary>
        public override void Initialize()
        {
            bullets = new List<ProjectileSprite>();
            // TODO: Add your initialization code here
            base.Initialize();
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            removeBullets();
            Collision();
          
            base.Update(gameTime);
        }

        private void removeBullets()
        {
            foreach (var item in bullets)
            {
                if (item.Boundary().Bottom < 0)
                    badBullets.Add(item);
            }
        }


        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        /// <summary>
        /// Loads the content of the class
        /// </summary>
        protected override void LoadContent()
        {
            imageBullet = game.Content.Load<Texture2D>(img);
            base.LoadContent();
        }


        /// <summary>
        /// launches a laser
        /// </summary>
        /// <param name="player">collision rectangle of the player</param>
        /// <param name="game1">The game</param>
        public override void Launch(Rectangle player, GameTime game1)
         {
            Vector2 start = new Vector2((player.X) +(player.Width /2), (player.Y) - 2);
            ProjectileSprite bullet = new ProjectileSprite(game, start, imageBullet, velocity, score);
            bullet.Initialize();
            bullets.Add(bullet);
            game.Components.Add(bullet);
         }


        /// <summary>
        /// adds a alien squad
        /// </summary>
        /// <param name="alien">Group of enemies</param>
        public void addSquad(AlienSquad alien)
        {
            this.alien = alien;
        }

        /// <summary>
        /// Checks collision of lasers
        /// </summary>
        public void Collision()
        {
            foreach (ProjectileSprite item in bullets)
            {
                for (int i = 0; i < alien.Length; i++)
                {
                    if (alien[i].Remains)
                    {
                        if (item.Boundary().Intersects(alien[i].Boundary()))
                        {                  
                            onCollision1(alien[i]);
                            badBullets.Add(item);
                            speedCount++;
                             if (speedCount == 2)
                            {
                                alien.increaseSpeed();
                                speedCount = 0;
                            }                            
                        }
                    }
                }
            }

            foreach (ProjectileSprite item in badBullets)
            {
                bullets.Remove(item);
                game.Components.Remove(item);
            }
            badBullets.Clear();
        }

        protected void onCollision1(AlienSprite alien)
        {
            if (Collision1 != null)
                Collision1(alien);
        }

        public void addScoreScrprite(ScoreSprite score)
        {
            this.score = score;
        }

    }
}
