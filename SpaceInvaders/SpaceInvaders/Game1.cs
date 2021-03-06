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
    delegate void Handler(AlienSprite alien);
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private PlayerSprite player;
        private AlienSquad squad;
        int numAlians = 8;
        private BombFactory bombFactory;
        private LaserFactory laserFactory;
        private ScoreSprite score;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1080;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new PlayerSprite(this);
            bombFactory = new BombFactory(this);
            laserFactory = new LaserFactory(this);
            squad = new AlienSquad(this, numAlians, bombFactory);
            score = new ScoreSprite(this);
            
            bombFactory.addPlayer(player);
            laserFactory.addSquad(squad);
            player.addLaserFactory(laserFactory);
            squad.addLaserFactory(laserFactory);
            score.addFactory(laserFactory);
            score.addPlayer(player);
            score.addSquad(squad);
            squad.addScoreSprite(score);
            laserFactory.addScoreScrprite(score);
            bombFactory.addScoreScrprite(score);

            Components.Add(laserFactory);
            Components.Add(bombFactory);
            Components.Add(player);
            Components.Add(squad);
            Components.Add(score);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            player.checkInput(gameTime);

            if (squad.allDead())
            {
                squad.ressurect();
                squad.resetSpeed();
            }
            else
            {
                score.gameOver(gameTime);
            } 
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
