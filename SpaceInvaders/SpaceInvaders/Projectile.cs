using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{
    /// <summary>
    /// defines the behaviour of the bullets
    /// </summary>
    class Projectile
    {
        private int projectileHeight;
        private int projectileWidth;
        private Vector2 position;
        private Vector2 velocity;

        public Projectile(int projectileWidth, int projectileHeight, float Xposition, float Yposition, Vector2 velocity)
        {
            this.projectileWidth = projectileWidth;
            this.projectileHeight = projectileHeight;
            position = new Vector2(Xposition, Yposition);
            this.velocity = velocity;
        }


        /// <summary>
        /// returns the boundary or recatangle that determins the size of the projectile
        /// </summary>
        public Rectangle Boundary
        {
            get { return new Rectangle((int)position.X, (int)position.Y, projectileWidth, projectileHeight); }
        }


        /// <summary>
        /// returns the position of the projectile
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }


        /// <summary>
        /// moves the projectile
        /// </summary>
        public void move()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
        }
    }
}
