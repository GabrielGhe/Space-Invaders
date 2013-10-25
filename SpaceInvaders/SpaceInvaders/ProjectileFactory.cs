using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{

    /// <summary>
    /// this is an abstract class for different types of bullet objects
    /// </summary>
    abstract class ProjectileFactory : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected List<ProjectileSprite> bullets;
        private Game1 game;
        //private List<Buncker> bunker;


        public ProjectileFactory(Game1 game)
            : base(game)
        {
            this.game = game;

        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        public virtual void Launch(Rectangle x, GameTime game1)
        {

        }


        /// <summary>
        /// Initializes the class
        /// </summary>
        public override void Initialize()
        {
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

            base.Update(gameTime);
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
            base.LoadContent();
        }
    }
}
