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
using TestProject.Utilities;

namespace TestProject.Systems
{
    /// <summary>
    /// Player class will house all the information associated with the player
    /// object. Since this RPG is being designed with 1 player in mind ala
    /// Dragon Warrior 1, and not with a party of players ala Final Fantasy,
    /// one player object is sufficient. 
    /// 
    /// Craig Giles
    /// Feb.2008
    /// </summary>
    class Player
    {
        /// <summary>
        /// Sprites used by the player.
        /// tSprite is the SpriteSheet, while tWalking is the 
        /// individual sprite 'frame'
        /// </summary>
        Texture2D tSprite;
        Texture2D tWalking;

        /// <summary>
        /// Rectangles used to look up the individual sprite frames
        /// on the sprite sheet
        /// </summary>
        Rectangle
            SpriteFront1 = new Rectangle((0 * 32), 0, 32, 32),
            SpriteFront2 = new Rectangle((1 * 32), 0, 32, 32),
            SpriteBack1 = new Rectangle((2 * 32), 0, 32, 32),
            SpriteBack2 = new Rectangle((3 * 32), 0, 32, 32),
            SpriteLeft1 = new Rectangle((4 * 32), 0, 32, 32),
            SpriteLeft2 = new Rectangle((5 * 32), 0, 32, 32),
            SpriteRight1 = new Rectangle((6 * 32), 0, 32, 32),
            SpriteRight2 = new Rectangle((7 * 32), 0, 32, 32);

        Rectangle srcRect;

        /// <summary>
        /// Current location on the game map 
        /// </summary>
        Vector2 vLocation = new Vector2(135, 135);

        /// <summary>
        /// Speed modifiers. fSpeed is the current speed, while
        /// fDefaultSpeed is used to reset the current speed
        /// </summary>
        float fSpeed = 3.0f;
        const float fDefaultSpeed = 3.0f;

        /// <summary>
        /// Character Stats 
        /// </summary>
        int iHealth;
        int iMaxHealth;

        int iMana;
        int iMaxMana;

        int iExp;
        int iNextLevel;

        /// <summary>
        /// The bounding box of the player. Used to draw the player 
        /// sprite as well as used in collision detection for the tilemap.
        /// </summary>
        Rectangle rRect;

        /// <summary>
        /// Constructor of the Player object
        /// </summary>
        /// <param name="SpriteSheet">SpriteSheet used for the Hero Sprite</param>
        public Player(Texture2D SpriteSheet)
        {
            rRect = new Rectangle((int)vLocation.X, (int)vLocation.Y, 26, 26);
            
            tSprite = SpriteSheet;

            tWalking = SpriteSheet;
            srcRect = SpriteFront1;

            iHealth = 100;
            iMana = 100;
            iExp = 0;
        }

        #region Properties
        public float Speed
        {
            get { return fSpeed; }
            set { fSpeed = value; }
        }

        public float DefaultSpeed
        {
            get { return fDefaultSpeed; }
        }

        public Rectangle SpriteSheet
        {
            get { return srcRect; }
        }

        public Texture2D Sprite
        {
            get { return tWalking; }
        }

        public float LocX
        {
            get { return vLocation.X; }
            set 
            { 
                vLocation.X = value;
                rRect.X = (int)vLocation.X;
            }
        }//end LocX

        public float LocY
        {
            get { return vLocation.Y; }
            set
            {
                vLocation.Y = value;
                rRect.Y = (int)vLocation.Y;
            }
        }//end LocY

        public int Health
        {
            get { return iHealth; }
            set { iHealth = value; }
        }

        public int Mana
        {
            get { return iMana; }
            set { iMana = value; }
        }

        public int Exp
        {
            get { return iExp; }
            set { iExp = value; }
        }

        public Rectangle Rectangle
        {
            get { return rRect; }
        }
        #endregion

        /// <summary>
        /// Updates the sprites walking texture to be facing the correct direction
        /// </summary>
        public void UpdateSprite()
        {
            if (InputHelper.Left)
            { srcRect = SpriteLeft1; }

            if (InputHelper.Right)
            { srcRect = SpriteRight1; }

            if (InputHelper.Up)
            { srcRect = SpriteBack1; }

            if (InputHelper.Down)
            { srcRect = SpriteFront1; }
        }//end UpdateSprite

    }// end Class
}//end Namespace
