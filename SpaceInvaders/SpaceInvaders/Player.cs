using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{

    /// <summary>
    /// this is the class that defines the player object ... and 
    /// also you can not hate the player but you can hate the game1.cs
    /// </summary>
    class Player
    {
        readonly private float SPEED;
        private int screenWidth;
        private int playerHeight;
        private int playerWidth;
        private Vector2 position;
        private int hp;


        /// <summary>
        /// onstructor for the player object
        /// </summary>
        /// <param name="playerHeight"></param>
        /// <param name="playerWidth"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="speed"></param>
        public Player(int playerHeight, int playerWidth, int screenWidth, int screenHeight, float speed, int hp)
        {
            this.playerHeight = playerHeight;
            this.playerWidth = playerWidth;
            this.screenWidth = screenWidth;
            this.SPEED = speed;
            this.hp = hp;
            position = new Vector2( (screenWidth/2) - (playerWidth/2) , (screenHeight - playerHeight) - 26);
        }


        /// <summary>
        /// gives the boundary of the player object/ rectangle
        /// </summary>
        public Rectangle Boundary
        {
            get { return new Rectangle((int)position.X, (int)position.Y, playerWidth, playerHeight); }
        }


        /// <summary>
        /// gives the position of the player object
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }


        /// <summary>
        /// moves the player left
        /// </summary>
        public void MoveLeft()
        {
            if (position.X - SPEED >= 0)
                position.X -= SPEED;
            else
                position.X = 0;
        }

        /// <summary>
        /// Takes away hit points from player
        /// </summary>
        public void Hit()
        {
            if (hp > 0)
                hp--;
        }


        /// <summary>
        /// moves the player right
        /// </summary>
        public void MoveRight()
        {
            if (position.X + playerWidth + SPEED <= screenWidth)
                position.X += SPEED;
            else
                position.X = screenWidth - playerWidth;
        }

        public int HP
        {
            get { return hp; }
        }
    }
}