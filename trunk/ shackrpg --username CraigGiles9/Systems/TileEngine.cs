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
using TestProject.Systems.Maps;
using TestProject.Systems;
using TestProject.Utilities;


namespace TestProject
{
    class TileEngine
    {
        /// <summary>
        /// Objects passed to the TileEngine from GameEngine
        /// </summary>
        SpriteBatch spriteBatch;
        ContentManager Content;
        Player player;
        GameTime gameTime;

        Texture2D tileSet;
        Rectangle recSource;

        /// <summary>
        /// Timers for the map scroll animation
        /// </summary>
        float elapsed;
        float fTotalElapsedTime = 0.0f;
        float fKeyPressDelay = 0.05f;        

        /// <summary>
        /// The Map arrays. BaseMap is drawn on screen while walkable map
        /// checks to see if the player can walk on a specific tile.
        /// </summary>
        int[,] iBaseMap = new int[iMapHeight, iMapWidth];

        /// <summary>
        /// Upper left corner of the viewable map
        /// </summary>
        int iMapX = 0;
        int iMapY = 0;

        /// <summary>
        /// Tile Height and Width in Pixels
        /// </summary>
        public int iTileHeight = 32;
        public int iTileWidth = 32;

        /// <summary>
        /// Height and Width of the map in Tiles
        /// </summary>
        public const int iMapHeight = 19;
        public const int iMapWidth = 25;
        public const int iMapDisplayHeight = 19;
        public const int iMapDisplayWidth = 25;

        /// <summary>
        /// Sets curGameArea
        /// </summary>
        public GameAreas curGameArea, prevGameArea;

        /// <summary>
        /// Current list of game areas within Abernathy
        /// </summary>
        public enum GameAreas
        {
            NewGame,
            TestField,
            TestDungeon,
        }

        /// <summary>
        /// Constructor for TileEngine.
        /// </summary>
        /// <param name="Content">Content Manager from GameEngine</param>
        /// <param name="setPlayer">Player object from GameEngine</param>
        public TileEngine(ContentManager setContent, Player setPlayer, SpriteBatch setSpriteBatch)
        {
            Content = setContent;
            player = setPlayer;
            spriteBatch = setSpriteBatch;
            curGameArea = GameAreas.TestField;
            prevGameArea = GameAreas.NewGame;
        }
        public TileEngine()
        { }

        /// <summary>
        /// The core of the Tile engine, this connects all of the methods together.
        /// </summary>
        public void Run(GameTime setGameTime)
        {
            gameTime = setGameTime;

            Initialize();
            LoadMap();
            Update();
            Draw();
        }

        /// <summary>
        /// Initializes all map data and loads the needed textures.
        /// </summary>
        private void Initialize()
        {
            tileSet = Content.Load<Texture2D>(@"MainGame/Graphics/TestTiles2");
        }

        /// <summary>
        /// Fills the current iBaseMap array to the new map data.
        /// </summary>
        private void LoadMap()
        {
            if (curGameArea != prevGameArea)
            {
                //Eventually this will be a complex method using GameArea enum
                TestField field = new TestField();

                for (int y = 0; y < iMapHeight; y++)
                {
                    for (int x = 0; x < iMapWidth; x++)
                    {
                        iBaseMap[y, x] = field.iBaseMap[y, x];
                    }
                }//end for loops

                // sets prev game area to cur game area
                prevGameArea = curGameArea;
            }
        }

        private void Update()
        {
            // Sets the elapsed time to the TotalSeconds elapsed.
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Adds the elapsed time to our TotalElapsedTime timer
            fTotalElapsedTime += elapsed;

            // Gets the players input
            InputHelper.GetPlayerInput(player);

            
            // If the TotalElapsedTime is greater than the delay, execute movement
            // commands.
            
            if (fTotalElapsedTime >= fKeyPressDelay)
            {
                MovePlayer();
                fTotalElapsedTime = 0.0f;
            }

            // Updates the player sprite based on the direction they're moving
            player.UpdateSprite();
        }

        /// <summary>
        /// Moves the player across the tiles based on what key was pressed
        /// </summary>
        private void MovePlayer()
        {
            /*
            if (InputHelper.MoveLeft)
            {
                    player.LocX--;                    
            }

            if (InputHelper.MoveRight)
            {
                    player.LocX++;
            }

            if (InputHelper.MoveUp)
            {
                    player.LocY--;
            }

            if (InputHelper.MoveDown)
            {
                    player.LocY++;
            }
             * */
        }//end MovePlayer

        /// <summary>
        /// Draws the current iMap array to the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from Main Engine</param>
        private void Draw()
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            #region Draw Map
            for (int y = 0; y < iMapDisplayHeight; y++)
            {
                for (int x = 0; x < iMapDisplayWidth; x++)
                {
                    // Gets information on which tile should be drawn on screen
                    int iTileToDraw = iBaseMap[y + iMapY, x + iMapX];

                    // Gets the correct tile from the TileSheet
                    recSource = new Rectangle((iTileWidth * iTileToDraw), 0, iTileWidth, iTileHeight);

                    // Draws the correct tile onscreen
                    spriteBatch.Draw(tileSet,
                        new Rectangle((x * iTileWidth), (y * iTileHeight), iTileWidth, iTileHeight),
                        recSource,
                        Color.White);
                }
            }//end for loop
            #endregion

            #region Draw Player
            spriteBatch.Draw(player.Sprite, 
                new Rectangle(((int)player.LocX * iTileWidth), ((int)player.LocY * iTileHeight),32,32), 
                player.SpriteSheet, 
                Color.White);
            #endregion

            spriteBatch.End();
        }//end Draw


    }//end Class
}//end Namespace
