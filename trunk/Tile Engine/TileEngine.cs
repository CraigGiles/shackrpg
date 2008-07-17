using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public class TileEngine
    {
        public Map Map
        {
            get { return currentMap; }
            set { currentMap = value; }
        }
        Map currentMap, previousMap;


        #region Load Map


        /// <summary>
        /// Loads a map by map name
        /// </summary>
        /// <param name="mapName">name of map</param>
        public void LoadMap(string mapName)
        {
            Map newMap = LoadNewMap.Load(mapName);
            LoadMap(newMap, null);
        }


        /// <summary>
        /// Loads a new map by name
        /// </summary>
        /// <param name="mapName">name of map</param>
        /// <param name="portalEntry">portal entry location</param>
        public void LoadMap(string mapName, Portal portalEntry)
        {
            Map newMap = LoadNewMap.Load(mapName);
            LoadMap(newMap, portalEntry);
        }


        /// <summary>
        /// Loads the given map
        /// </summary>
        /// <param name="map">map to load</param>
        public void LoadMap(Map map)
        {
            LoadMap(map, null);
        }


        /// <summary>
        /// Loads the given map
        /// </summary>
        /// <param name="map">map to load</param>
        /// <param name="portalEntry">portal entry location</param>
        public void LoadMap(Map map, Portal portalEntry)
        {
            if (map == null)
                return;

            //if current map = prev map, re-load prev map
            //which will save all NPC information etc
            if (currentMap != null && previousMap != null &&
                currentMap.Name == previousMap.Name)
            {
                Map temp = currentMap;

                currentMap = previousMap;
                previousMap = temp;
            }
            else
            {
                previousMap = currentMap;
                currentMap = map;
            }
            

            LoadCharacterOnPoint(portalEntry);
        }


        /// <summary>
        /// Loads the character on a portal after map loads
        /// </summary>
        /// <param name="portalEntry"></param>
        private void LoadCharacterOnPoint(Portal portalEntry)
        {
            //if portalEntry is null, replace with start location
            if (portalEntry == null)
            {
                if (Map.StartLocationPortal != null)
                    portalEntry = Map.StartLocationPortal;
            }

            //if portalEntry != null
                //change character location to be portalEntry + direction +x


        }


        #endregion


        #region Draw Layers


        /// <summary>
        /// Draws the layers of the tile map
        /// </summary>
        /// <param name="drawBase">Draw Base Layer?</param>
        /// <param name="drawFringe">Draw Fringe Layer?</param>
        /// <param name="drawObject">Draw Object Layer?</param>
        public void DrawLayers(bool drawBase, bool drawFringe, bool drawObject)
        {
            //if spritebatch is null, throw exception
            if (Globals.SpriteBatch == null)
            {
                throw new ArgumentNullException("SpriteBatch");
            }

            //if no layers are to be drawn, exit out of method
            if (!drawBase && !drawFringe && !drawObject)
            {
                return;
            }

            if (Map == null)
                return;

            Rectangle destinationRectangle = new Rectangle(
                0, 0, Map.TileWidth, Map.TileHeight);

            //cycle through all 'tiles' on the map
            for (int y = 0; y < Map.Height; y++)
            {
                for (int x = 0; x < Map.Width; x++)
                {
                    //sets up map position, and current tile to draw
                    Point mapPosition = new Point(x, y);
                    destinationRectangle.X = x * Map.TileWidth;
                    destinationRectangle.Y = y * Map.TileHeight;

                    //Draws the base layer
                    if (drawBase)
                    {
                        Rectangle sourceRectangle = 
                            Map.GetSourceRectangle(MapLayer.BaseLayer, mapPosition);

                        if (sourceRectangle != Rectangle.Empty)
                        {
                            Globals.SpriteBatch.Draw(Map.MapTexture,
                                destinationRectangle,
                                sourceRectangle,
                                Color.White);
                        }
                    }


                    //Draws the fringe layer
                    if (drawFringe)
                    {
                        Rectangle sourceRectangle = 
                            Map.GetSourceRectangle(MapLayer.FringeLayer, mapPosition);

                        if (sourceRectangle != Rectangle.Empty)
                        {
                            Globals.SpriteBatch.Draw(Map.MapTexture,
                                destinationRectangle,
                                sourceRectangle,
                                Color.White);
                        }
                    }

                    //Draws the object layer
                    if (drawObject)
                    {
                        Rectangle sourceRectangle =
                            Map.GetSourceRectangle(MapLayer.ObjectLayer, mapPosition);

                        if (sourceRectangle != Rectangle.Empty)
                        {
                            Globals.SpriteBatch.Draw(Map.MapTexture,
                                destinationRectangle,
                                sourceRectangle,
                                Color.White);
                        }
                    }
                }
            }//end for loops

        }
        


        #endregion

    }//end TileEngine
}//end namespace
