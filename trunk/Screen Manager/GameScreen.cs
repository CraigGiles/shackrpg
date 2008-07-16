#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace ShackRPG
{
    public abstract class GameScreen
    {

        #region Screen Settings


        /// <summary>
        /// Screen manager managing all game screens
        /// </summary>
        public static ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }
        static ScreenManager screenManager;


        /// <summary>
        /// The game screens Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string name;


        /// <summary>
        /// if true, no screen under this one will update
        /// </summary>
        public bool BlocksUpdate
        {
            get { return blocksUpdate; }
            set { blocksUpdate = value; }
        }
        bool blocksUpdate = false;


        /// <summary>
        /// if true, no screen under this one will draw
        /// </summary>
        public bool BlocksDraw
        {
            get { return blocksDraw; }
            set { blocksDraw = value; }
        }
        bool blocksDraw = false;


        /// <summary>
        /// Should the engine remove this screen
        /// from the stack
        /// </summary>
        public bool RemoveScreen
        {
            get { return removeScreen; }
            set { removeScreen = value; }
        }
        bool removeScreen = false;


        #endregion


        public virtual void LoadContent()
        {
        }


        public virtual void UnloadContent()
        {
        }


        public virtual void Update(GameTime gameTime)
        {
        }


        public virtual void Draw(GameTime gameTime)
        {
        }
        

    }//end class
}//end namespace
