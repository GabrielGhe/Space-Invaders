using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is a game component that implements IUpdateable.
    /// it is also the GUI wrapper class for projectiles
    /// </summary>
    class ProjectileSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game1 game;
        private Vector2 start;
        private String img;
        private Texture2D imageBullet;
        private SpriteBatch spriteBatch;
        private Projectile proj;
        private float x;
        private float y;
        private Vector2 velocity;
        private ScoreSprite over;

        public ProjectileSprite(Game1 game, Vector2 start, Texture2D imageBullet, Vector2 velocity, ScoreSprite score)
            : base(game)
        {
            this.game = game;
            this.start = start;
            this.imageBullet = imageBullet;
            this.velocity = velocity;
            this.over = score;
            // TODO: Construct any child components here
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            x = start.X;
            y = start.Y;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            proj.move();
            base.Update(gameTime);
        }


        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            /*
             *checks if game is over if it is no longer display bullets
             */
            if (over.alive())
            {
                spriteBatch.Begin();
                spriteBatch.Draw(imageBullet, proj.Position, Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }


        /// <summary>
        /// Loads the content of the class
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            proj = new Projectile(imageBullet.Width, imageBullet.Height,x,y,velocity);
            base.LoadContent();
        }


        /// <summary>
        /// Returns the collision rectangle of the projectile
        /// </summary>
        public Rectangle Boundary()
        {
            return proj.Boundary;
        }
    }
}
