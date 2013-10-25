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
    /// this is the GUI wrapper class for the player object (displays the image for the player ).
    /// </summary>
    class PlayerSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private KeyboardState state;
        private KeyboardState oldState;
        private Player player;
        private SpriteBatch spriteBatch;
        private Texture2D imagePlayer;
        private Game game;
        TimeSpan previousKeyTime;
        TimeSpan shootKeyTime;
        TimeSpan tolerance = TimeSpan.FromMilliseconds(100);
        TimeSpan shootTolerance = TimeSpan.FromMilliseconds(200);
        private LaserFactory laserFactory;
        

        /// <summary>
        /// constructor for the playerSprite object
        /// </summary>
        /// <param name="game"></param>
        public PlayerSprite(Game game)
            : base(game)
        {
            this.game = game;
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            state = Keyboard.GetState();
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
            if (player.HP > 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(imagePlayer, player.Position, Color.White);
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
            imagePlayer = game.Content.Load<Texture2D>("player");
            player = new Player(imagePlayer.Height, imagePlayer.Width, GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height, 20F, 5);
            base.LoadContent();
        }



        /// <summary>
        /// checks the movement of the player and determines if movement is allowed or if it should be 
        /// repeated 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void checkInput(GameTime gameTime)
        {
            // previousKeyTime = gameTime.TotalGameTime;
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Right))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    player.MoveRight();
                    previousKeyTime = gameTime.TotalGameTime;
                }
                else
                {
                    if (gameTime.TotalGameTime - previousKeyTime > tolerance)
                    {
                        player.MoveRight();
                    }
                }
            }
            else if (newState.IsKeyDown(Keys.Left))
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    player.MoveLeft();
                    previousKeyTime = gameTime.TotalGameTime;
                }
                else
                {
                    if (gameTime.TotalGameTime - previousKeyTime > tolerance)
                    {
                        player.MoveLeft();
                    }
                }

            // Update saved state.
            oldState = newState;
            
                
                state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Space))
                {
                    if (gameTime.TotalGameTime - shootKeyTime > shootTolerance)
                    {
                        laserFactory.Launch(player.Boundary, gameTime);
                        shootKeyTime = gameTime.TotalGameTime;
                    } 
                }
        }


        /// <summary>
        /// Says the player is hit
        /// </summary>
        public void Hit()
        {
                player.Hit();
        }

        public int getHp()
        {
            return player.HP;
        }

        /// <summary>
        /// Checks if the player is alive
        /// </summary>


        /// <summary>
        /// Returns the collision rectangle of the player
        /// </summary>
        public Rectangle Boundary()
        {
            return player.Boundary;
        }


        /// <summary>
        /// Adds a laser factory
        /// </summary>
        /// <param name="factory">Class that holds all the lasers</param>
        public void addLaserFactory(LaserFactory factory)
        {
            this.laserFactory = factory;
        }
    }
}

