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
    /// this is the GUI wrapper class for the aliens 
    /// </summary>
     class AlienSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Alien al;
        private SpriteBatch spriteBatch;
        private Texture2D imageAlien;
        private Game game;
        private String img;
        float x;
        float y;
        private BombFactory bombFactory;
        bool remove;
        private bool remains = true;
        private int pts;

        /// <summary>
        /// constuctor for Alien Sprite object
        /// </summary>
        /// <param name="game"></param>
        /// <param name="img"></param>
        /// <param name="Xposition"></param>
        /// <param name="Yposition"></param>
        public AlienSprite(Game game, String img, float Xposition, float Yposition, int pts)
            : base(game)
        {
            this.game = game;
            this.img = img;
            this.x = Xposition;
            this.y = Yposition;
            this.remove = true;
            this.pts = pts;
        }



        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
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
        /// Loads the content of the class
        /// </summary>
        protected override void LoadContent()
        { 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageAlien = game.Content.Load<Texture2D>(img);
            al = new Alien(imageAlien.Width, imageAlien.Height, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, x, y, pts);
            base.LoadContent();
        }



        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        public override void Draw(GameTime gameTime)
        {
            if (remove)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(imageAlien, al.Position, Color.White);
                spriteBatch.End();
            } 
            base.Draw(gameTime);
        }

        /// <summary>
        /// property of remains variable. public get and private set.
        /// </summary>
        public bool Remains
        {
            get { return remains; }
        }


        /// <summary>
        /// Returns the alien's current direction
        /// </summary>
        public Direction CurrentDir()
        {
           return al.CurrentDir();
        }



        /// <summary>
        /// Changes the alien's direction
        /// </summary>
        /// <param name="dir">Direction enum</param>
        public void changeDir(Direction dir)
        {
            al.changeDir(dir);
        }



        /// <summary>
        /// Returns the alien's current direction
        /// </summary>
        /// <param name="dir">Direction enum</param>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Move(Direction dir, GameTime gameTime)
        {
            al.Move(dir, gameTime);
        }


        /// <summary>
        /// Returns a Rectangle for alien collision
        /// </summary>
        public Rectangle Boundary()
        {
            return al.Boundary;
        }



        /// <summary>
        /// Tries to move the alien in a direction given
        /// </summary>
        /// <param name="dir">Direction enum</param>
        public bool tryMove(Direction dir)
        {
            return al.tryMove(dir);
        }



        /// <summary>
        /// Checks if the game is over
        /// </summary>
        /// <param name="player">Collision recangle of player</param>
        public bool GG (Rectangle player)
        {
            if (remains)
                return al.GG(player);
            else
                return false;
        }


        /// <summary>
        /// Adds a bombFactory
        /// </summary>
        /// <param name="bombFactory">Class that holds all the bombs</param>
        public void addBombFactory(BombFactory bombFactory)
        {
            this.bombFactory = bombFactory;
        }

        /// <summary>
        /// sets the sprite to dead
        /// </summary>
        public void kill()
        {
            this.remove = false;
            remains = false;
        }

        public void ressurect()
        {
            remove = true;
            remains = true;
            al.resetPos();
        }

        public int getPts()
        {
            return al.Pts;
        }

    }
}
