using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace ShackRPG
{
    public static class LoadNewMap
    {

        public static Map Load(string mapName)
        {
            Map newMap = new Map();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content/Maps/" + mapName + ".xml");

            foreach (XmlNode nodes in doc)
            {
                foreach (XmlNode node in nodes.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Name":
                            SetName(node, newMap);
                            break;

                        case "Dimentions":
                            SetDimentions(node, newMap);
                            break;

                        case "TileSize":
                            SetTileSize(node, newMap);
                            break;

                        case "Textures":
                            SetTextures(node, newMap);
                            break;

                        case "Music":
                            SetMusic(node, newMap);
                            break;

                        case "StartLocation":
                            SetStartLocation(node, newMap);
                            break;

                        case "NPCs":
                            SetNpcs(node, newMap);
                            break;

                        case "TreasureChests":
                            SetTreasureChests(node, newMap);
                            break;

                        case "Portals":
                            SetPortals(node, newMap);
                            break;

                        case "BaseLayer":
                            SetBaseLayer(node, newMap);
                            break;

                        case "FringeLayer":
                            SetFringeLayer(node, newMap);
                            break;

                        case "ObjectLayer":
                            SetObjectLayer(node, newMap);
                            break;

                        case "CollisionLayer":
                            SetCollisionLayer(node, newMap);
                            break;

                    }

                }//end foreach
            }//end nodes in doc

            InitializeMapLayout(newMap);
            
            return newMap;
        }

        private static void SetName(XmlNode node, Map map)
        {
            map.Name = node.Attributes["Asset"].Value;
            map.Description = node.Attributes["Description"].Value;
        }

        private static void SetDimentions(XmlNode node, Map map)
        {
            map.Width = int.Parse(node.Attributes["Width"].Value);
            map.Height = int.Parse(node.Attributes["Height"].Value);
        }

        private static void SetTileSize(XmlNode node, Map map)
        {
            map.TileWidth = int.Parse(node.Attributes["Width"].Value);
            map.TileHeight = int.Parse(node.Attributes["Height"].Value);
        }

        private static void SetTextures(XmlNode node, Map map)
        {
            map.MapTexture = Globals.Content.Load<Texture2D>(node.Attributes["Map"].Value);
            map.BattleTexture = Globals.Content.Load<Texture2D>(node.Attributes["Battle"].Value);
        }

        private static void SetMusic(XmlNode node, Map map)
        {
            map.BackgroundMusicCue = node.Attributes["Background"].Value;
            map.BattleMusicCue = node.Attributes["Battle"].Value;
            map.BossMusicCue = node.Attributes["Boss"].Value;
        }

        private static void SetStartLocation(XmlNode node, Map map)
        {
            // Portal portal = new Portal()
            // portal.MapPositionX = int.Parse(node.Attributes["X"].Value / etc etc
        }

        private static void SetNpcs(XmlNode node, Map map)
        {
        }

        private static void SetTreasureChests(XmlNode node, Map map)
        {
        }

        private static void SetPortals(XmlNode node, Map map)
        {
        }

        private static void SetBaseLayer(XmlNode node, Map map)
        {
            map.BaseLayer = LoadMapLayer("BaseLayer", node);
        }

        private static void SetFringeLayer(XmlNode node, Map map)
        {
            map.FringeLayer = LoadMapLayer("FringeLayer", node);
        }

        private static void SetObjectLayer(XmlNode node, Map map)
        {
            map.ObjectLayer = LoadMapLayer("ObjectLayer", node);
        }

        private static void SetCollisionLayer(XmlNode node, Map map)
        {
            map.CollisionLayer = LoadMapLayer("CollisionLayer", node);
        }

        private static int[] LoadMapLayer(string layer, XmlNode nodes)
        {
            List<int> layerInfo = new List<int>();
            string testing = string.Empty;

            foreach (XmlNode node in nodes.ChildNodes)
            {
                foreach (char c in node.InnerText)
                {
                    //if the character is a special char, ignore it
                    if (c != '\r' &&
                        c != '\n' &&
                        c != '\t')
                    {
                        //if the character is a ',' (seperates two numbers)
                        //convert it to an int, and add it to the layer info
                        if (c == ',')
                        {
                            if (c.ToString() != "")
                            {

                                layerInfo.Add(int.Parse(testing));
                                testing = string.Empty;
                            }
                        }

                            //if character is not a ',' (seperates two numbers)
                        //add it to the end of the string
                        else
                            testing += c;
                    }
                }
            }

            return layerInfo.ToArray();
        }


        /// <summary>
        /// Initializes map once all data has been loaded
        /// </summary>
        private static void InitializeMapLayout(Map map)
        {
        }


    }
}
