using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public static class Globals
    {

        /// <summary>
        /// Base Game Data
        /// </summary>
        public static BaseGame Game;

        /// <summary>
        /// Content Manager
        /// </summary>
        public static ContentManager Content;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        public static SpriteBatch SpriteBatch;

        /// <summary>
        /// Sprite Font
        /// </summary>
        public static SpriteFont Font;

        /// <summary>
        /// Game Screen Manager
        /// </summary>
        public static ScreenManager ScreenManager;


        /// <summary>
        /// Input object
        /// </summary>
        public static Input Input;

        /// <summary>
        /// Camera object for 2D
        /// </summary>
        public static Camera Camera;


        /// <summary>
        /// Populates globals used for the game engine
        /// </summary>
        /// <param name="game"></param>
        /// <param name="content"></param>
        /// <param name="batch"></param>
        public static void PopulateGlobals(
            BaseGame game,
            ContentManager content,
            SpriteBatch batch)
        {
            Game = game;
            Content = content;
            SpriteBatch = batch;

            //input any extra created global objects here
            Font = content.Load<SpriteFont>(@"Courier New");

            //Starts up the screen manager
            ScreenManager = new ScreenManager();
            ScreenManager.Initialize(new GameMenu());

            //Sets up the games Input
            Input = new Input();

            //Creates game camera
            Camera = new Camera();

            //TileEngine tileEngine = new TileEngine();
            

        }//end PopulateGlobals()
    
    }//end Class
}//end namespace
