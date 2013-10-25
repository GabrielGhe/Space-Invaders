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
    /// this is the class that creates bullet objects for the aliens
    /// </summary>
     class BombFactory : ProjectileFactory
    {
        private List<ProjectileSprite> badBullets = new List<ProjectileSprite>();
        private Game1 game;
        private PlayerSprite player;
        private Texture2D imageBullet;
        private String img = "bomb";
        private Vector2 velocity = new Vector2(0, 2);
        private ScoreSprite score;

        public BombFactory(Game1 game) : base(game)
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
                if (item.Boundary().Bottom > (GraphicsDevice.Viewport.Height -26 ))
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
        /// <param name="player">collision rectangle of the alien</param>
        /// <param name="game1">The game</param>
        public override void Launch(Rectangle alien, GameTime game1)
        {
            Vector2 start = new Vector2((alien.X), (alien.Y));
            ProjectileSprite bullet = new ProjectileSprite(game, start, imageBullet, velocity, score);
            bullet.Initialize();
            bullets.Add(bullet);
            game.Components.Add(bullet);
        }


        /// <summary>
        /// adds a player
        /// </summary>
        /// <param name="player">the player</param>
        public void addPlayer(PlayerSprite player)
        {
            this.player = player;
        }


        /// <summary>
        /// Checks collision of lasers
        /// </summary>
        public void Collision()
        {
            foreach (ProjectileSprite item in bullets)
            { 
                   if (item.Boundary().Intersects(player.Boundary()))
                    {
                        player.Hit();
                        badBullets.Add(item);
                        game.Components.Remove(item);
                   } 
            }
            removeBullet();
        }

        public void removeBullet()
        {
            foreach (ProjectileSprite item in badBullets)
            {
                bullets.Remove(item);
                game.Components.Remove(item);
            }
            badBullets.Clear();
        }

        public void addScoreScrprite(ScoreSprite score)
        {
            this.score = score;
        }

    }
}
