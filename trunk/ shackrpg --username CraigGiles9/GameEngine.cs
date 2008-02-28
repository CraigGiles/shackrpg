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
using TestProject.Systems;
using TestProject.Utilities;

namespace TestProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Feb. 28th || Tile Engine is working and will need some tweaks in the future,
        /// but its where I want it to be for my first attempt. Next I will be starting on
        /// the Battle engine and MOB classes.
        /// 
        /// TODO: GameStates, Battle Engine, Clean up GameEngine code...
        /// </summary>

        Player player;                      // Player object
        TitleScreen titleScreen;            // Title Screen

        TileEngineV2 tileEngine;            // Tile Engine used for RPG
        GraphicsDeviceManager graphics;     
        SpriteBatch spriteBatch;            
        // GameState curGameState;             // Will be used to hold the current GameState

        /// <summary>
        /// GameStates used by the RPG. Will include states like Title,
        /// Game, GameOver, and CutScene.
        /// </summary>
        // public enum GameState
        // { Title, Scene, }

        /// <summary>
        /// Constructor for GameEngine
        /// </summary>
        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #region Initialize, Load, Unload Content
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Sets the default resolution to 800x600
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

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

            // the Title Screen
            titleScreen = new TitleScreen(Content);

            // Creates the Player object with the Hero SpriteSheet
            player = new Player(Content.Load<Texture2D>(@"MainGame/Graphics/Hero"));

            // the Tile Engine used to draw the map and update player information on the map
            tileEngine = new TileEngineV2(Content, player, spriteBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
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

            InputHelper.GetPlayerInput(player);             //Gets player input

            //Updates the game based on the current game state
            switch (GameStateManager.curGameState)
            {
                case GameStateManager.GameState.Title:
                    titleScreen.Update();                   // used to check if "ENTER" was pressed
                    break;

                case GameStateManager.GameState.Scene:
                    tileEngine.Update(gameTime);            // calls the TileEngine's update method
                    break;
            }

            base.Update(gameTime);
        }//end Update


        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draws the game based on the current game state
            switch (GameStateManager.curGameState)
            {
                case GameStateManager.GameState.Title:
                    titleScreen.Draw(spriteBatch);          // Draws the title screen
                    break;

                case GameStateManager.GameState.Scene:
                    tileEngine.Draw(gameTime);              // calls the TileEngine's Draw method
                    break;
            }//end switch

            base.Draw(gameTime);
        }//end Draw
        #endregion


        #region Unit Tests

        /// <summary>
        /// Unit tests used to test individual parts of the code without 
        /// having to execute the entire program.
        /// 
        /// IE: Testing the title menu sprites locations by calling
        /// testGame.ShowTitleScreen();
        /// </summary>
        #region UnitTest Class
        public delegate void TestDelegate();
        class UnitTest : GameEngine
        {
            TestDelegate testLoop;

            public UnitTest(TestDelegate setTestLoop)
            {
                testLoop = setTestLoop;
            }

            protected override void Draw(GameTime gameTime)
            {
                base.Draw(gameTime);
                testLoop();
            }
        }//end Class UnitTest
        #endregion

        #region StartTest Method
        static UnitTest testGame;
        public static void StartTest(TestDelegate testLoop)
        {
            testGame = new UnitTest(testLoop);
            testGame.Run();
            testGame.Dispose();
        }//end StartTest()
        #endregion

        #region Title Screen
        // Testing Title Screen Sprites
        public static void TestTitleScreenSprites()
        {
            StartTest(
            delegate
            {
                testGame.ShowTitleScreen();
            });
        }

        public void ShowTitleScreen()
        {
            titleScreen = new TitleScreen(Content);
            titleScreen.Draw(spriteBatch);
        }
        #endregion


        #endregion

    }
}
