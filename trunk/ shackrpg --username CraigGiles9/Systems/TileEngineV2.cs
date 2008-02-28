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

namespace TestProject.Systems
{
    /// <summary>
    /// Tile Engine
    /// Version 2.0
    /// 
    /// Basic RPG style Tile Engine using MapHelper to load data from each
    /// individual map file. Version 1.0 only let the player move 1 tile at 
    /// a time, and since that was not very fluid I developed v2.0 with a new
    /// movement structure and collision detection system.
    /// 
    /// Craig Giles
    /// Feb. 2008
    /// </summary>
    class TileEngineV2
    {

        /// <summary>
        /// Objects passed to the TileEngine from GameEngine
        /// </summary>
        SpriteBatch spriteBatch;
        ContentManager Content;
        Player player;
        GameTime gameTime;

        /// <summary>
        /// The MapHelper utility class
        /// </summary>
        MapHelper mapHelper;

        /// <summary>
        /// TileSet textures and Rectangle used to locate the correct tile 
        /// from the Sprite Sheet
        /// </summary>
        Texture2D tileSet;
        Rectangle recSource;

        /// <summary>
        /// Upper left corner of the viewable map
        /// </summary>
        int iMapX = 0;
        int iMapY = 0;

        /// <summary>
        /// Tile Height and Width in Pixels
        /// </summary>
        public const int iTileHeight = 32;
        public const int iTileWidth = 32;

        /// <summary>
        /// Height and Width of the map in Tiles
        /// </summary>
        public const int iMapHeight = 19;
        public const int iMapWidth = 25;
        public const int iMapDisplayHeight = 19;
        public const int iMapDisplayWidth = 25;

        /// <summary>
        /// Constructor for TileEngine.
        /// </summary>
        /// <param name="Content">Content Manager from GameEngine</param>
        /// <param name="setPlayer">Player object from GameEngine</param>
        /// <param name="setSpriteBatch">SpriteBatch from GameEngine</param>
        public TileEngineV2(ContentManager setContent, Player setPlayer, SpriteBatch setSpriteBatch)
        {
            Content = setContent;
            player = setPlayer;
            spriteBatch = setSpriteBatch;

            mapHelper = new MapHelper();
            mapHelper.prevGameArea = MapHelper.GameAreas.NewGame;
            mapHelper.curGameArea = MapHelper.GameAreas.TestField;

            tileSet = Content.Load<Texture2D>(@"MainGame/Graphics/TestTiles2");
        }
        public TileEngineV2()
        { }

        /// <summary>
        /// Checks to see if a new map needs to be loaded, moves the player around
        /// the current map and checks to see if the player is moving onto a blocked
        /// tile
        /// </summary>
        /// <param name="setGameTime">Game Timer</param>
        public void Update(GameTime setGameTime)
        {
            gameTime = setGameTime; // Sets the current GameTime timer

            mapHelper.LoadMap();                    // Checks to see if a new map needs to be loaded
            MovePlayer();                           // Update Player Location on Map
            mapHelper.CheckBoundires(player);       // Determines if the player is on a valid tile
            
        }//end Update()

        /// <summary>
        /// Fills the map arrays with the correct map data.
        /// </summary>
        private void LoadMap()
        {
            if (mapHelper.curGameArea != mapHelper.prevGameArea)
            {
                //Eventually this will be a complex method using GameArea enum
                // TestField field = new TestField();

                mapHelper.LoadMap();

                #region Moved To MapHelper
                /*
                for (int y = 0; y < iMapHeight; y++)
                {
                    for (int x = 0; x < iMapWidth; x++)
                    {
                        //Fills the iBaseMap array with the tile information
                        iBaseMap[y, x] = TestField.iBaseMap[y, x];

                        //fils the walkable map array with the boundry information
                        iWalkableMap[y,x] = TestField.iWalkableMap[y,x];

                        //if the walkable map's current location is 0, mark the tile as
                        // unwalkable by placing a rectangle over the tile. This rectangle
                        // will be located at the same spot as the unwalkable tile, and 
                        // have slightly smaller width / height. I used the -2 pixels so 
                        // the player can still squeeze between two unwalkable tiles.
                        if (iWalkableMap[y, x] != 0)
                        { rWalkable[y, x] = new Rectangle(x * iTileWidth, y * iTileHeight, iTileWidth - 10, iTileHeight - 10); }
                    }
                }//end for loops
                 * */
                #endregion

            }
        }//End LoadMap()

        /// <summary>
        /// Takes player input and moves the player avatar accordingly
        /// </summary>
        private void MovePlayer()
        {
            // Moves the player in a specific direction based on user input
            if (InputHelper.Up)
            { player.LocY -= player.Speed; }
            if (InputHelper.Down)
            { player.LocY += player.Speed; }
            if (InputHelper.Left)
            { player.LocX -= player.Speed; }
            if (InputHelper.Right)
            { player.LocX += player.Speed; }

            // Updates the player sprite to be facing the correct way
            player.UpdateSprite();

        }//end MovePlayer()

        /// <summary>
        /// Draws the curret map data and player location onto the screen
        /// </summary>
        /// <param name="setGameTime">Game Timer</param>
        public void Draw(GameTime setGameTime)
        {
            // sets the game timer to gameTime
            gameTime = setGameTime;

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            #region Draw Map
            for (int y = 0; y < iMapDisplayHeight; y++)
            {
                for (int x = 0; x < iMapDisplayWidth; x++)
                {
                    // Gets information on which tile should be drawn on screen
                    int iTileToDraw = mapHelper.iBaseMap[y + iMapY, x + iMapX];

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
                player.Rectangle,
                player.SpriteSheet,
                Color.White);
            #endregion

            spriteBatch.End();
        }//end Draw

    }//end Class
}//end Namespace
