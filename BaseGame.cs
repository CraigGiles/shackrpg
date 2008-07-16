using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ShackRPG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BaseGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FpsCounter FPS = new FpsCounter();

        public BaseGame()
        {
            graphics = new GraphicsDeviceManager(this);
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

            Globals.PopulateGlobals(this, Content, spriteBatch);

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

            FPS.Update(gameTime);

            Globals.ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            #region #IF DEBUG
#if DEBUG
            //start spritebatch
            Globals.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            //if control + alt is pressed, draw all screens
            //in the GameScreens stack
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) &&
                Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                Globals.ScreenManager.DrawScreenList();
            }

            //draws the current FPS to screen
            FPS.Draw();

            //ends spritebatch for debug
            Globals.SpriteBatch.End();
            
#endif
            #endregion

            //draw game screens
            Globals.ScreenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
