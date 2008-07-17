using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml;

namespace ShackRPG
{
    public class Map
    {

        #region Name / Description


        /// <summary>
        /// Map name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string name = string.Empty;


        /// <summary>
        /// Map description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string description = String.Empty;


        #endregion


        #region Dimentions


        /// <summary>
        /// Gets or sets map Height
        /// </summary>
        public int Height
        {
            get { return mapDimentions.Y; }
            set { mapDimentions.Y = value; }
        }
        /// <summary>
        /// Gets or sets map width
        /// </summary>
        public int Width
        {
            get { return mapDimentions.X; }
            set { mapDimentions.X = value; }
        }
        Point mapDimentions;


        /// <summary>
        /// Gets or sets tile height
        /// </summary>
        public int TileHeight
        {
            get { return tileSize.Y; }
            set { tileSize = new Point(value, value); }
        }
        /// <summary>
        /// Gets or sets tile width
        /// </summary>
        public int TileWidth
        {
            get { return tileSize.X; }
            set { tileSize = new Point(value, value); }
        }
        Point tileSize;


        /// <summary>
        /// Gets the amount of tiles per row
        /// of the MapTexture
        /// </summary>
        public int TilesPerRow
        {
            get { return (MapTexture.Width / TileWidth); }
        }


        #endregion


        #region Graphics Data


        /// <summary>
        /// Tileset used by the map
        /// </summary>
        public Texture2D MapTexture
        {
            get { return tileset; }
            set { tileset = value; }
        }
        Texture2D tileset;


        /// <summary>
        /// Battle texture used by the map
        /// </summary>
        public Texture2D BattleTexture
        {
            get { return battleTexture; }
            set { battleTexture = value; }
        }
        Texture2D battleTexture;


        #endregion


        #region Music Data

        /// <summary>
        /// Maps background music
        /// </summary>
        public string BackgroundMusicCue
        {
            get { return backgroundMusicCue; }
            set { backgroundMusicCue = value; }
        }
        string backgroundMusicCue = String.Empty;


        /// <summary>
        /// Maps battle music for random battles
        /// </summary>
        public string BattleMusicCue
        {
            get { return battleMusicCue; }
            set { battleMusicCue = value; }
        }
        string battleMusicCue = string.Empty;


        /// <summary>
        /// Maps boss music for random battles
        /// </summary>
        public string BossMusicCue
        {
            get { return bossMusicCue; }
            set { bossMusicCue = value; }
        }
        string bossMusicCue = string.Empty;


        #endregion


        #region NPCs

        /// <summary>
        /// List of all NPCs on the map
        /// </summary>
        public List<NPC> NPCs
        {
            get { return npcs; }
        }
        List<NPC> npcs = new List<NPC>();

        #endregion
        

        #region Treasure Chests


        /// <summary>
        /// List of all treasure chests on the map
        /// </summary>
        public List<Treasure> TreasureChests
        {
            get { return treasureChests; }
        }
        List<Treasure> treasureChests = new List<Treasure>();


        #endregion


        #region Portals

        /// <summary>
        /// List of all portals to other maps
        /// </summary>
        public List<Portal> Portals
        {
            get { return portals; }
        }
        List<Portal> portals = new List<Portal>();


        /// <summary>
        /// Gets the start location Tile for the player
        /// </summary>
        public Portal StartLocationPortal
        {
            get { return startLocationPortal; }
            set { startLocationPortal = value; }
        }
        Portal startLocationPortal;


        #endregion


        #region Map Layers


        /// <summary>
        /// Current Map BaseLayer
        /// </summary>
        public int[] BaseLayer
        {
            get { return baseLayer; }
            set { baseLayer = (int[])value.Clone(); }
        }
        /// <summary>
        /// Current Map FringeLayer
        /// </summary>
        public int[] FringeLayer
        {
            get { return fringeLayer; }
            set { fringeLayer = (int[])value.Clone(); }
        }
        /// <summary>
        /// Current Map ObjectLayer
        /// </summary>
        public int[] ObjectLayer
        {
            get { return objectLayer; }
            set { objectLayer = (int[])value.Clone(); }
        }
        /// <summary>
        /// Current Map CollisionLayer
        /// </summary>
        public int[] CollisionLayer
        {
            get { return collisionLayer; }
            set { collisionLayer = (int[])value.Clone(); }
        }
        int[] baseLayer,
            fringeLayer,
            objectLayer,
            collisionLayer;


        public Rectangle GetSourceRectangle(MapLayer layer, Point mapPosition)
        {
            if ((mapPosition.X < 0) || (mapPosition.X >= mapDimentions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= mapDimentions.Y))
            {
                return Rectangle.Empty;
            }

            int cellIndex = -1;
            switch (layer)
            {
                case MapLayer.BaseLayer:
                    cellIndex = baseLayer[mapPosition.Y * mapDimentions.X + mapPosition.X];
                     break;

                case MapLayer.FringeLayer:
                    cellIndex = fringeLayer[mapPosition.Y * mapDimentions.X + mapPosition.X];
                    break;

                case MapLayer.ObjectLayer:
                    cellIndex = objectLayer[mapPosition.Y * mapDimentions.X + mapPosition.X];
                    break;

                default:
                    cellIndex = -1;
                    break;
            }

            if (cellIndex < 0)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                (cellIndex % TilesPerRow) * tileSize.X,
                (cellIndex / TilesPerRow) * tileSize.Y,
                tileSize.X, tileSize.Y);
        }


        #endregion



    }//end class
}//end namespace
