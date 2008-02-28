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

namespace TestProject.Systems.Maps
{
    /// <summary>
    /// Test Field is just the basic test map designed to test the capabilities
    /// of the Tile Engine. Eventually each map file will house the map
    /// data (base map, walkability, and graphics) as well as monster / NPC
    /// data associated with that map. (which monsters and NPCs will be present
    /// in that map. For example, Dragons wont be seen in towns, and NPCs wont
    /// be visiting dragons lairs)
    /// 
    /// Since this is just a test, it will most likely not make any future versions
    /// past testing phases, and real map data will take its place.
    /// 
    /// Craig Giles
    /// Feb.2008
    /// </summary>
    class TestField : TileEngineV2
    {
        
        /// <summary>
        /// Contains the layout of this map.
        /// 0 = wall
        /// 1 = grass
        /// 2 = dirt road
        /// 3 = Deep (unwalkable) water
        /// 4 = shallow (walkable) water
        /// 5 = cave enterance
        /// </summary>
        static public int[,] iBaseMap = new int[iMapHeight, iMapWidth]{
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
            {0,0,5,5,0,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
            {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
            {0,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            {0,1,1,0,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            {0,1,0,0,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,0},
            {0,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,0},
            {0,4,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,2,2,2,2,2,2,1,0},
            {3,4,4,1,1,1,1,1,1,1,2,1,1,1,2,2,2,2,1,1,1,1,1,1,0},
            {3,4,4,1,1,1,1,1,1,1,2,1,1,1,2,1,1,1,3,3,3,1,1,1,0},
            {3,3,4,4,4,4,1,1,1,1,2,2,2,2,2,1,3,3,3,3,3,1,1,1,0},
            {3,3,3,3,3,4,1,1,1,1,2,1,1,1,1,3,3,3,3,3,3,3,1,1,0},
            {3,3,3,3,3,4,4,1,1,1,2,1,1,1,1,3,3,3,3,3,3,3,1,1,0},
            {3,3,3,3,3,3,4,4,4,1,2,2,2,2,2,1,1,1,3,3,3,1,1,0,0},
            {3,3,3,3,3,3,3,3,4,4,4,1,1,1,2,1,1,1,1,1,1,1,1,0,0},
            {3,3,3,3,3,3,3,3,3,3,4,4,4,4,2,1,1,1,1,1,1,0,0,0,0},
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0},
        };

        /// <summary>
        /// Contains the walkable data for the map
        /// 0 = character can walk over tile
        /// 1 = character can not walk over tile
        /// </summary>
        static public int[,] iWalkableMap = new int[iMapHeight, iMapWidth]{
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1},
            {1,1,0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
            {1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,1},
            {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,1},
            {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,1},
            {1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1,1},
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        //TODO: Array of Monsters associated with this area
        //TODO: Array of any NPCs associated with this area



    }//end Class
}//end NameSpace
