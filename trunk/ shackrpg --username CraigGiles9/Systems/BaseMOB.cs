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

namespace TestProject.Systems
{
    /// <summary>
    /// Basic MOB class used for monsters within the game
    /// </summary>
    class BaseMOB
    {
        /// <summary>
        /// MOBs texture
        /// </summary>
        Texture2D tSprite;

        /// <summary>
        /// MOBs Stats 
        /// </summary>
        int iHealth;        
        int iMana;
        int iExpGiven;          // How much EXP the mob gives on death

        /// <summary>
        /// Timers associated with battle turns for the MOB
        /// </summary>
        float fTimer;           
        float fTimerDefault;

        #region Properties
        public int Health
        {
            get { return iHealth; }
        }
        public int Mana
        {
            get { return iMana; }
        }
        public int ExpGiven
        {
            get { return iExpGiven; }
        }

        public float Timer
        {
            get { return fTimer; }
        }

        #endregion

        /// <summary>
        /// Constructor for MOB class
        /// </summary>
        /// <param name="setSprite">The Sprite used to display the MOB in battle</param>
        public BaseMOB(Texture2D setSprite)
        {
            tSprite = setSprite;
        }

    }//end Class
}//end Namespace
