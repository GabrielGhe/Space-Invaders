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
    public delegate void killEventHandler (int point);
    /// <summary>
    /// keeps track of all aliens on screen.
    /// </summary>
    class AlienSquad : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int numAlien;
        private AlienSprite[,] squads;
        private String[] aliens = { "bug1", "flyingsaucer1", "spaceship1", "satellite1" };
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D imageAlien;
        private float x;
        private float y;
        private Direction current = Direction.LEFT;
        private TimeSpan previousKeyTime;
        private TimeSpan shootKeyTime;
        private TimeSpan tolerance;
        private TimeSpan shootTolerance;
        private BombFactory bombFactory;
        private AlienSprite movingRight;
        private AlienSprite movingLeft;
        private LaserFactory pewpew;
        private int speed = 350;
        private int shootingSpeed = 1800;
        public event killEventHandler kill2;
        private ScoreSprite scoreSprite;
        private int length;

        /// <summary>
        /// constructor for the alienSquad which are rows of aliens
        /// </summary>
        /// <param name="game"></param>
        public AlienSquad(Game1 game, int numAliens, BombFactory bombFactory)
            : base(game)
        {
            this.game = game;
            this.numAlien = numAliens;
            squads = new AlienSprite[numAlien, 12];
            this.bombFactory = bombFactory;
        }



        /// <summary>
        /// initializes all variables
        /// </summary>
        public override void Initialize()
        {
            imageAlien = game.Content.Load<Texture2D>(aliens[0]);
            int picNum = 0;
            float picHeight = imageAlien.Height;
            float picWidth = imageAlien.Width * 2;
            this.x = picWidth;
            this.y = picHeight + (picHeight /2);

            tolerance = TimeSpan.FromMilliseconds(speed);
            shootTolerance = TimeSpan.FromMilliseconds(shootingSpeed);

            for (int i = 0; i < numAlien; i++)
            {
                if (picNum == 4)
                    picNum = 0;

                for (int x = 0; x < 12; x++)
                {
                    squads[i, x] = new AlienSprite(game, aliens[picNum], picWidth, picHeight, (((numAlien + 1) - i)*10));
                    squads[i, x].addBombFactory(bombFactory);
                    squads[i, x].Initialize();
                    picWidth += this.x;
                }
                picWidth = this.x;
                picHeight += this.y;
                picNum++;
            }
            movingLeft = squads[3, 0];
            movingRight = squads[3, 5];
            length = squads.Length;
            pewpew.Collision1 += kill;

            base.Initialize();
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (!scoreSprite.Over)
            {
                RandomFire(gameTime);
            }
            base.Update(gameTime);

        }

        /// <summary>
        /// returns the aliensprite at position i
        /// </summary>
        /// <param name="i">index position</param>
        public AlienSprite this[int i]
        {
            get { return squads[i/12, i % 12]; }
        }

        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < numAlien; i++)
            {
                for (int x = 0; x < 12; x++)
                {
                    squads[i, x].Draw(gameTime);
                }
            }
        }


        /// <summary>
        /// Loads the content of the class
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }


        /// <summary>
        /// this method determins the direction and moves the rows of aliens in the direction
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Move(GameTime gameTime)
        {
                FindRight();
                FindLeft();

                if (movingLeft.CurrentDir() == Direction.LEFT)
                {
                    if (movingLeft.tryMove(Direction.LEFT))
                    {
                        current = Direction.LEFT;
                    }
                    else
                        current = Direction.DOWN;

                }
                else if (movingRight.CurrentDir() == Direction.RIGHT)
                {
                    if (movingRight.tryMove(Direction.RIGHT))
                    {
                        current = Direction.RIGHT;
                    }
                    else
                        current = Direction.DOWN;
                }

                if (gameTime.TotalGameTime - previousKeyTime > tolerance)
                {
                    previousKeyTime = gameTime.TotalGameTime;
                    for (int i = 0; i < numAlien; i++)
                    {
                        for (int x = 0; x < 12; x++)
                        {
                            squads[i, x].Move(current, gameTime);
                        }
                    }
                }
          }


        /// <summary>
        /// finds row is most to the right
        /// </summary>
        public void FindRight()
        {
            bool found = false;
            for (int i = squads.GetLength(1) -1; i > 0 ; i--)
            {
                for (int j = 0; j < squads.GetLength(0); j++)
                {
                    if (squads[j, i].Remains)
                    {
                        movingRight = squads[j, i];
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }  
        }


        /// <summary>
        /// finds row is most to the left
        /// </summary>
        public void FindLeft()
        {
            bool found = false;
            for (int i = 0; i < squads.GetLength(1); i++)
            {
                for (int j = 0; j < squads.GetLength(0); j++)
                {
                    if (squads[j, i].Remains)
                    {
                        movingLeft = squads[j, i];
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }
        }

        //true = alien hit player or toch player

        /// <summary>
        /// determines if the game is over
        /// </summary>
        public bool GG(Rectangle player)
        {
            for (int i = squads.GetLength(0) - 1; i >= 0; i--)
            {
                for (int x = 0; x < 12; x++)
                {
                    if (squads[i, x].GG(player))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Makes a random alien shoot
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void RandomFire(GameTime gameTime)
        {
            long length = squads.LongLength;
            Random num = new Random();
            Rectangle position;
            int location = num.Next((int)length);
            bool good = true;
            
            
            if(gameTime.TotalGameTime - shootKeyTime > shootTolerance)
            {
                shootKeyTime = gameTime.TotalGameTime;
                
                while (good) //while aliens still remain
                {
                    if (this[location].Remains) //if the alien still remains  launch a bomb
                    {
                        position = this[location].Boundary();
                        bombFactory.Launch(position, gameTime);
                        good = false;
                    }
                    else
                    {
                        location = num.Next((int)length); // selecte random alien again
                    }
                }
            }
        }

        public void increaseSpeed()
        {
            speed -= 8;
            tolerance = TimeSpan.FromMilliseconds(speed);
            decreaseShooting();
        }

        public void decreaseShooting()
        {
            shootingSpeed += 300;
            shootTolerance = TimeSpan.FromMilliseconds(shootingSpeed);
        }

        public void resetSpeed()
        {
            speed = 350;
            tolerance = TimeSpan.FromMilliseconds(speed);
            shootingSpeed = 2000;
            shootTolerance = TimeSpan.FromMilliseconds(shootingSpeed);
        }


        public void addLaserFactory(LaserFactory pewpew)
        {
            this.pewpew = pewpew;
        }

        public void kill(AlienSprite alien)
        {
            alien.kill();
        }

        public void ressurect()
        {
            for (int i = 0; i < numAlien; i++)
            {
                for (int x = 0; x < 12; x++)
                {
                    squads[i, x].ressurect();
                }
            }
        }


        public bool allDead()
        {
            for (int i = 0; i < numAlien; i++)
            {
                for (int x = 0; x < 12; x++)
                {
                    if (squads[i, x].Remains)
                        return false;
                }
            }
            return true; 
        }

        protected void onKill2(int point)
        {
            if (kill2 != null)
                kill2(point);
        }

        public void addScoreSprite(ScoreSprite scoreSprite)
        {
            this.scoreSprite = scoreSprite;
        }

        public int Length
        {
            get { return length; }
        }
    }
}
