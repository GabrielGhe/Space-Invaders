using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{
    enum Direction { LEFT, RIGHT, DOWN };

    /// <summary>
    /// this is the alien object which the antaganist of the game
    /// </summary>
    class Alien
    {
        private Vector2 originalPos;
        private Vector2 position;
        private int alienHeight;
        private int alienWidth;
        private int screenWidth;
        private int screenHeight;
        private Direction currentDirection = Direction.LEFT;
        private Direction oldDir = Direction.LEFT;
        private int pts;
       


        /// <summary>
        /// is a constructer for the Alien Object
        /// </summary>
        /// <param name="alienWidth"></param>
        /// <param name="alienHeight"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="Xposition"></param>
        /// <param name="Yposition"></param>
        public Alien(int alienWidth, int alienHeight, int screenWidth, int screenHeight, float Xposition, float Yposition, int pts)
        {
            this.alienWidth = alienWidth;
            this.alienHeight = alienWidth;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.pts = pts;
            position = new Vector2(Xposition, Yposition);
            originalPos = position;
        }


        /// <summary>
        /// returns the boundary or recatangle that determins the size of the alien
        /// </summary>
        public Rectangle Boundary
        {
            get { return new Rectangle((int)position.X, (int)position.Y, alienWidth, alienHeight); }
        }


        /// <summary>
        /// gets the position of a given alien
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }

        public int Pts
        {
            get { return pts; }
        }


        /// <summary>
        /// this actually performs the movement depending o nthe dirction with a delay
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="gameTime"></param>
        public void Move(Direction dir, GameTime gameTime)
        {

            switch (dir)
            {
                case Direction.LEFT:
                    position.X -= (alienWidth/2) + 5; 
                    break;
                case Direction.RIGHT:
                    position.X += (alienWidth/2) + 5; 
                    break;
                case Direction.DOWN:
                    position.Y += alienHeight/3;
                    if (oldDir == Direction.LEFT)
                    {
                        currentDirection = Direction.RIGHT;
                        oldDir = Direction.RIGHT;
                    }
                    else
                    {
                        currentDirection = Direction.LEFT;
                        oldDir = Direction.LEFT;
                    }
                    break;
            
            }
           
        }


        /// <summary>
        /// this returns the direction the aliens are currently going
        /// </summary>
        /// <returns>direction</returns>
        public Direction CurrentDir()
        {
           return currentDirection;
        }


        /// <summary>
        /// this changes direction of the aliens movement
        /// </summary>
        /// <param name="dir"></param>
        public void changeDir(Direction dir)
        {
            this.currentDirection = dir;
        }


        /// <summary>
        /// this checks if alien can move
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>bool</returns>
        public bool tryMove(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    if (position.X <= 0)
                        return false;
                    else
                        return true;

                case Direction.RIGHT:
                    if(position.X >= (screenWidth - alienWidth))
                        return false;
                    else
                        return true;

                case Direction.DOWN:
                    if(position.Y <= screenHeight)
                        return false;
                    else
                        return true;
                default: 
                    return false;
            }
        }


         /// <summary>
         /// this is not working .... but should end game when lowest row of aliens touch player
         /// </summary>
         /// <param name="player"></param>
         /// <returns></returns>
        public bool GG(Rectangle player)
        {
            if (this.Boundary.Intersects(player) || this.Boundary.Bottom >= player.Top)
                return true;
            else
                return false;
        }
        
        public void resetPos()
        {
            originalPos.Y += 30;
            position = originalPos;
        }
    }
}
