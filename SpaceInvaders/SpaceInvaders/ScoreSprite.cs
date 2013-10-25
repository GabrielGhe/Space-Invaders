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
    /// this is the Sprite to display score and player lifes
    /// </summary>
    class ScoreSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        int currentScore = 0;
        int currentHighScore;
        private SpriteFont font;
        private SpriteBatch spriteBatch;
        private Texture2D banner;
        private LaserFactory laser;
        private PlayerSprite play;
        private AlienSquad alienSquad;
        private Game game;
        private HighScore highScore;
        private bool over = false;
        private Texture2D player;
        float screenWidth;
        float screenHeight;

        public ScoreSprite(Game1 game)
            : base(game)
        {
            this.game = game;
        }

        private void updateScore(AlienSprite alien)
        {
            currentScore += alien.getPts();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            player = game.Content.Load<Texture2D>("playerLife");
            laser.Collision1 += updateScore;
            highScore = new HighScore();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = game.Content.Load<SpriteFont>("scoreFont");
            banner = game.Content.Load<Texture2D>("banner");
            screenWidth = GraphicsDevice.Viewport.Width / 2;
            screenHeight = GraphicsDevice.Viewport.Height;
            // playerLife
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(banner, new Vector2( 0 , (screenHeight - banner.Height)), Color.White);


            /*
             * this checks which message to display at end game 
             */
            if (!over)
            {
                spriteBatch.DrawString(font, "Score: " + currentScore, new Vector2(0, (screenHeight - 23)), Color.White);
                for (int i = 0; i < play.getHp(); i++)
			    {
                    spriteBatch.Draw(player, new Vector2((screenWidth + (i * player.Width)), (screenHeight - player.Height - 5)), Color.White);
			    }
            }
            else
            {
                if (currentHighScore <= currentScore )
                {
                    spriteBatch.DrawString(font, ("Game Over!!!" + "\n" + "You have a new HighScore!!!!" + "\n" + "Your score was: " + currentScore), new Vector2(0, 0), Color.White);
                    highScore.writeFile(currentScore.ToString());
                }
                else
                {
                    spriteBatch.DrawString(font, "Game Over!!! Your score was: " + currentScore, new Vector2(0, 0), Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


        /// <summary>
        /// adds a laserFactory
        /// </summary>
        /// <param name="laser"></param>
        public void addFactory(LaserFactory laser)
        {
            this.laser = laser;
        }

        /// <summary>
        /// adds a player
        /// </summary>
        /// <param name="play"></param>
        public void addPlayer(PlayerSprite play)
        {
            this.play = play;
        }

        /// <summary>
        /// adds a alienSquad
        /// </summary>
        /// <param name="alienSquad"></param>
        public void addSquad(AlienSquad alienSquad)
        {
            this.alienSquad = alienSquad;
        }


        /// <summary>
        /// checks if the player is alive
        /// </summary>
        public bool alive()
        {
            if (play.getHp() > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// this checks rather game is over if it is 
        /// stops moving aliens.
        /// </summary>
        /// <param name="gameTime"></param>
        public void gameOver(GameTime gameTime)
        {
            if (!alienSquad.GG(play.Boundary()) && alive())
            {
                    alienSquad.Move(gameTime);
            }
            else
            {
                currentHighScore = highScore.readFile();
                over = true;
            }
        }

        public bool Over
        {
            get { return over; }
        }
    }
}
