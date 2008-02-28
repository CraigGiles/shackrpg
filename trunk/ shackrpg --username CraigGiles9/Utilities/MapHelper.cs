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

namespace TestProject.Utilities
{
    /// <summary>
    /// MapHelper is a utility class developed to translate all data from
    /// the map file to the Tile Engine. Currently all maps are being
    /// housed in a "MapName.cs" file, but in the future this will be
    /// changed after the development of a Tilemap Editor.
    /// 
    /// Craig Giles
    /// Feb.2008
    /// </summary>
    class MapHelper : TileEngineV2
    {
        /// <summary>
        /// an array of rectangles used as un-walkable tiles for the player.
        /// </summary>
        public Rectangle[,] rWalkable = new Rectangle[iMapHeight, iMapWidth];

        /// <summary>
        /// The Map arrays. BaseMap is drawn on screen while walkable map
        /// checks to see if the player can walk on a specific tile.
        /// </summary>
        public int[,] iBaseMap = new int[iMapHeight, iMapWidth];
        public int[,] iWalkableMap = new int[iMapHeight, iMapWidth];

        /// <summary>
        /// Hosts the current and previous game areas visited by the player
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

        public void LoadMap()
        {
            // Loads the map for the current game area
            switch (curGameArea)
            {
                #region TestField
                case GameAreas.TestField:
                    for (int y = 0; y < iMapHeight; y++)
                    {
                        for (int x = 0; x < iMapWidth; x++)
                        {
                            //Fills the iBaseMap array with the tile information
                            iBaseMap[y, x] = TestField.iBaseMap[y, x];

                            //fils the walkable map array with the boundry information
                            iWalkableMap[y, x] = TestField.iWalkableMap[y, x];

                            //if the walkable map's current location is 0, mark the tile as
                            // unwalkable by placing a rectangle over the tile. This rectangle
                            // will be located at the same spot as the unwalkable tile, and 
                            // have slightly smaller width / height. I used the -2 pixels so 
                            // the player can still squeeze between two unwalkable tiles.
                            if (iWalkableMap[y, x] != 0)
                            { rWalkable[y, x] = new Rectangle(x * iTileWidth, y * iTileHeight, iTileWidth - 10, iTileHeight - 10); }
                        }
                    }//end for loops
                    break;
                    #endregion

            }//end Switch
        }
        public void CheckBoundires(Player player)
        {
            for (int y = 0; y < iMapHeight; y++)
            {
                for (int x = 0; x < iMapWidth; x++)
                {
                    // Determines which direction the player is moving to ensure the player
                    // is not moved further on an invalid tile.
                    if (InputHelper.Up)
                    {
                        if (player.Rectangle.Intersects(rWalkable[y, x]))
                        { player.LocY += player.Speed; }
                    }
                    if (InputHelper.Down)
                    {
                        if (player.Rectangle.Intersects(rWalkable[y, x]))
                        { player.LocY -= player.Speed; }
                    }
                    if (InputHelper.Left)
                    {
                        if (player.Rectangle.Intersects(rWalkable[y, x]))
                        { player.LocX += player.Speed; }
                    }
                    if (InputHelper.Right)
                    {
                        if (player.Rectangle.Intersects(rWalkable[y, x]))
                        { player.LocX -= player.Speed; }
                    }
                }
            }//end For Loops
        }
        public void ChangeGameArea(GameAreas setGameArea)
        {

        }

    }//end Class
}//end NameSpace
