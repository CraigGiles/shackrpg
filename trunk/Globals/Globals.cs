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
        /// Audio Manager
        /// </summary>
        public static AudioManager AudioManager;


        /// <summary>
        /// Tile Engine
        /// </summary>
        public static TileEngine TileEngine;


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

            //creates audio manager
            AudioManager = new AudioManager(
                "Content\\Audio\\ShackAudio.xgs",
                "Content\\Audio\\Wave Bank.xwb",
                "Content\\Audio\\Sound Bank.xsb");

            //input any extra created global objects here
            Font = content.Load<SpriteFont>(@"Courier New");

            //Sets up the games Input
            Input = new Input();

            //Creates game camera
            Camera = new Camera();

            TileEngine = new TileEngine();

            //Starts up the screen manager
            ScreenManager = new ScreenManager();
            ScreenManager.Initialize(new StartMenu());

        }//end PopulateGlobals()
    
    }//end Class
}//end namespace
