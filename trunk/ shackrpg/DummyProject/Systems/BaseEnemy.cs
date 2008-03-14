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

using DummyProject.HelperClasses;


namespace DummyProject.Systems
{
    public abstract class BaseEnemy
    {
        #region Variables

        #region Animation Variables
        /// <summary>
        /// Houses the text information until the timers for their
        /// animation has expired
        /// </summary>
        int[] iDamageTaken = new int[100];
        int[] iHealthRecovered = new int[100];
        int[] iExpAdded = new int[100];

        /// <summary>
        /// Damage received text animation timers
        /// </summary>
        float[] fDamageTextTimer = new float[100];
        float fDamageTextTimerCap = 2.0f;

        /// <summary>
        /// Health gained text animation timers
        /// </summary>
        float[] fHealthTextTimer = new float[100];
        float fHealthTextTimerCap = 2.0f;

        /// <summary>
        /// Attacking animation timers
        /// </summary>
        float fAttackAnimationTimer = 0.0f;
        float fAttackAnimationTimerCap = 0.35f;

        /// <summary>
        /// Enemy ATB Battle Timers
        /// </summary>
        float fAtbTimer = 0.0f;
        float fAtbTimerCap = 5.0f;

        /// <summary>
        /// Players battle "idle" timers
        /// </summary>
        float fIdleBattleAnimationTimer = 0.0f;
        float fIdleBattleAnimationTimerCap;

        /// <summary>
        /// Rectangle used to find the current sprite in spritesheet
        /// </summary>
        Rectangle rCurSprite;

            #endregion

            #region Character Variables
            /// <summary>
            /// Strings for the enemy name and title
            /// </summary>
            string sName;               //enemy Name
            string sTitle;              //enemy Title

            /// <summary>
            /// enemy stats
            /// </summary>
            int iHealth = 300,          //Current Health
                iHealthMax = 300,       //Max Health for Level
                iStrength = 20,         //Current Strength
                iStrengthBase = 20,     //STR not including buffs or +STR items
                iAttackPower = 20,      //current ATK
                iAttackPowerBase = 20,  //ATK not including buffs or +ATK items
                iArmor = 20,            //current Armor
                iArmorBase = 20,        //Armor not including buffs or +Armor items
                iGold = 0,              //Current gold
                iLevel = 1;             //Current Level

            /// <summary>
            /// Damage and Defend Mods Current formula is
            /// Random int between current level, and level * damage mod
            /// </summary>
            int iAttackDamageMod = 5,
                iDefendDamageMod = 5;

            /// <summary>
            /// Walking speed
            /// </summary>
            float fSpeed = 3.2f;
            float fSpeedReset = 3.2f;

            /// <summary>
            /// Bool statements to indicate the current state of the enemy
            /// </summary>
            bool bHaste,            //enemy hasted (currently unused)
                 bSlow;             //enemy slowed (currently unused)

            #endregion

            #region System Variables
            /// <summary>
            /// 0 = Walking
            /// 1, 2, 3 = Attack animation frames
            /// 4 = Dead
            /// </summary>
            Texture2D tSpriteSheet;       

            /// <summary>
            /// Shows the current sprite to be rendered to screen
            /// </summary>
            Texture2D tCurSprite;

            /// <summary>
            /// Sprite box and attack box for enemy.
            /// </summary>
            Rectangle
                rSpriteRect = new Rectangle(0,0,72,62),
                rAttackBox = new Rectangle(0,0,36,62);

            /// <summary>
            /// enemy location on the field of play
            /// </summary>
            Vector2 vLocation;

            /// <summary>
            /// The last direction the enemy was facing
            /// </summary>
            bool bFacingLeft = true,
                bFacingRight = false;

            #endregion 

        #endregion

        #region Properties

        #region Animation Properties
        /// <summary>
        /// Enemies ATB timer
        /// </summary>
        public float ATBTimer
        {
            get { return fAtbTimer; }
            set { fAtbTimer = value; }
        }

        /// <summary>
        /// Enemies ATB timer Cap. 
        /// </summary>
        public float ATBTimerCap
        {
            get { return fAtbTimerCap; }
            set { fAtbTimerCap = value; }
        }

        /// <summary>
        /// Houses the Damage Received until the animation has
        /// had time to finish playing
        /// </summary>
        public int[] DamageTaken
        {
            get { return iDamageTaken; }
            set { iDamageTaken = value; }
        }

        /// <summary>
        /// Damage Received text timers
        /// </summary>
        public float[] TimerDamageText
        {
            get { return fDamageTextTimer; }
            set { fDamageTextTimer = value; }
        }

        /// <summary>
        /// Damage Received text timer Cap (Read Only)
        /// </summary>
        public float TimerDamageTextCap
        {
            get { return fDamageTextTimerCap; }
            //set { fDamageTextTimerCap = value; }
        }

        /// <summary>
        /// Houses the Health Recovered until the animation has
        /// had time to finish playing
        /// </summary>
        public int[] HealthRecovered
        {
            get { return iHealthRecovered; }
            set { iHealthRecovered = value; }
        }

        /// <summary>
        /// Health Recovered text timers
        /// </summary>
        public float[] TimerHealthText
        {
            get { return fHealthTextTimer; }
            set { fHealthTextTimer = value; }
        }

        /// <summary>
        /// Health Recovered text timer Cap (Read Only)
        /// </summary>
        public float TimerHealthTextCap
        {
            get { return fHealthTextTimerCap; }
            //set { fHealthTextTimerCap = value; }
        }

        /// <summary>
        /// Enemy attack animation timer
        /// </summary>
        public float TimerAttackAnimation
        {
            get { return fAttackAnimationTimer; }
            set { fAttackAnimationTimer = value; }
        }

        /// <summary>
        /// Enemy attack animation timer Cap
        /// </summary>
        public float TimerAttackAnimationCap
        {
            get { return fAttackAnimationTimerCap; }
            set { fAttackAnimationTimerCap = value; }
        }

        /// <summary>
        /// Enemy Battle idle animation timer
        /// </summary>
        public float TimerIdleAnimation
        {
            get { return fIdleBattleAnimationTimer; }
            set { fIdleBattleAnimationTimer = value; }
        }

        /// <summary>
        /// Enemy battle idle animation timer cap
        /// </summary>
        public float TimerIdleAnimationCap
        {
            get { return fIdleBattleAnimationTimerCap; }
            set { fIdleBattleAnimationTimerCap = value; }
        }
        
        #endregion

        #region Name, Title, and Status
        /// <summary>
        /// Gets or Sets the enemy name
        /// </summary>
        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy title
        /// </summary>
        public string Title
        {
            get { return sTitle; }
            set { sTitle = value; }
        }

        /// <summary>
        /// Current enemy location Vector2
        /// </summary>
        public Vector2 Location
        {
            get { return vLocation; }
            set { vLocation = value; }
        }

        /// <summary>
        /// Determines if the enemy is currently hasted
        /// </summary>
        public bool Haste
        {
            get { return bHaste; }
            set { bHaste = true; }
        }

        /// <summary>
        /// Determines if the enemy is currently slowed
        /// </summary>
        public bool Slow
        {
            get { return bSlow; }
            set { bSlow = true; }
        }
        #endregion

        #region Enemy Stats
        /// <summary>
        /// Gets or Sets the enemy Max Health
        /// </summary>
        public int HealthMax
        {
            get { return iHealthMax; }
            set { iHealthMax = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy Health
        /// </summary>
        public int Health
        {
            get { return iHealth; }
            set { iHealth = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current Base Strength
        /// </summary>
        public int BaseStrength
        {
            get { return iStrengthBase; }
            set { iStrengthBase = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current Strength
        /// </summary>
        public int Strength
        {
            get { return iStrength; }
            set { iStrength = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current Base Attack Power
        /// </summary>
        public int BaseAttackPower
        {
            get { return iAttackPowerBase; }
            set { iAttackPowerBase = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current Attack Power
        /// </summary>
        public int AttackPower
        {
            get { return iAttackPower; }
            set { iAttackPower = value; }
        }

        /// <summary>
        /// Gets or Sets the attacking damage mod
        /// </summary>
        public int AttackDamageMod
        {
            get { return iAttackDamageMod; }
            set { iAttackDamageMod = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy Base Armor 
        /// </summary>
        public int BaseArmor
        {
            get { return iArmorBase; }
            set { iArmorBase = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current Armor 
        /// </summary>
        public int Armor
        {
            get { return iArmor; }
            set { iArmor = value; }
        }

        /// <summary>
        /// Gets or Sets the defending damage mod
        /// </summary>
        public int DefendDamageMod
        {
            get { return iDefendDamageMod; }
            set { iDefendDamageMod = value; }
        }

        /// <summary>
        /// Gets the current speed or sets the "speed reset" value
        /// </summary>
        public float Speed
        {
            get { return fSpeed; }
            set { fSpeedReset = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current Level
        /// </summary>
        public int Level
        {
            get { return iLevel; }
            set { iLevel = value; }
        }

        /// <summary>
        /// Gets or Sets the enemy current gold
        /// </summary>
        public int Gold
        {
            get { return iGold; }
            set { iGold = value; }
        }
        #endregion

        #region Sprites

        /// <summary>
        /// The enemy sprite sheet housing all frames of animation
        /// </summary>
        public Texture2D SpriteSheet
        {
            get { return tSpriteSheet; }
            set { tSpriteSheet = value; }
        }

        /// <summary>
        /// Rectangle to where the enemy sprite is to be drawn
        /// </summary>
        public Rectangle BoundingBox
        {
            get { return rSpriteRect; }
            set { rSpriteRect = value; }
        }

        /// <summary>
        /// Gets or sets the Current Sprite rectangle for locating
        /// the current sprite from the spritesheet
        /// </summary>
        public Rectangle CurrentSprite
        {
            get { return rCurSprite; }
            set { rCurSprite = value; }
        }

        /// <summary>
        /// Determines weather or not the enemy was last facing left
        /// </summary>
        public bool FacingLeft
        {
            get { return bFacingLeft; }
            set { bFacingLeft = true; }
        }

        /// <summary>
        /// Determines weather or not the enemy was last facing right
        /// </summary>
        public bool FacingRight
        {
            get { return bFacingRight; }
            set { bFacingRight = true; }
        }

        #endregion

        #endregion

        #region Constructors and Initialization Methods
        //I'm not sure i need a constructor like this for BaseEnemy since its an
        //abstract class and will never actually be created, only inherited from
        public BaseEnemy()
            {
            }

        /// <summary>
        /// Sets the starting stats for enemy
        /// </summary>
        public abstract void SetStartingStats();

        /// <summary>
        /// Sets the timers used by the enemy in battle 
        /// </summary>
        public abstract void SetTimerCaps();
        #endregion

        #region Methods
        /// <summary>
        /// Updates the enemy sprite boxes, animations, and stats
        /// </summary>
        /// <param name="gameTime">Games timer</param>
        public abstract void Update(GameTime gameTime,Player player);

        /// <summary>
        /// Moves the enemy in the specified direction
        /// </summary>
        public abstract void Move(GameTime gameTime, Player player);

        /// <summary>
        /// Plays attack animation and determins damage delt to opponents in range
        /// </summary>
        /// <param name="gameTime">Games timer</param>
        /// <returns>The damage delt by the player attack</returns>
        public abstract int Attack(GameTime gameTime);

        #region Old Attack(GameTime gameTime) method
        //public int Attack(GameTime gameTime)
        //{
        //    int iDamageDone = 0;    //damage done by the attack

        //    //If the player is not in the middle of an attack, start one
        //    if (fAttackAnimationTimer == 0)
        //    {
        //        StartAttackAnimation(gameTime);  //starts the player animation

        //        //Random Modifier to attack power. This adds a slight 
        //        //random element to the attack
        //        //currently iDamageMod = 5
        //        int iRandomMod = RandomHelper.GetRandomInt(iLevel, iLevel * iAttackDamageMod);
        //        iDamageDone = iAttackPower + iRandomMod;
        //    }

        //    return iDamageDone;
        //}//end Attack(gameTime)
        #endregion

        /// <summary>
        /// Sets the enemy to "Blocking" raising their armor and stopping their walking speed
        /// </summary>
        public abstract void Block();
        #region Old Block() method
        //{
        //    iArmor += iLevel * 25;          //sets the ajusted "blocking" armor value
        //}//end Block()
        #endregion

        /// <summary>
        /// Adds health to the enemy
        /// </summary>
        /// <param name="healthAdded">Health to be added</param>
        public void AddHealth(int healthAdded)
        {
            //only runs if health is actually added (IE: Can not be a neg number)
            if (healthAdded >= 0)
            {
                bool added = false; //ensures the dmg text animation only adds one instance of dmg taken

                int hlth = (int)MathHelper.Max(0, healthAdded);  //takes the max value so damage can not be 0 or -
                iHealth += hlth;                            //ajusts the enemy health

                if (iHealth >= iHealthMax)                  //if players health is over its max health
                    iHealth = iHealthMax;                   //set players health to max health

                //initiates the array that houses the health healed this round
                for (int i = 0; i < 100; i++)
                {
                    if (!added)
                    {
                        if (iHealthRecovered[i] == 0)   //if the current block is empty
                        {
                            iHealthRecovered[i] = hlth; //add the health rewarded to current block
                            added = true;               //set bool to true, so health is only added to one block
                        }//end if
                    }//end !added
                }//end loop
            }

        }//end AddHealth(healthAdded)

        /// <summary>
        /// Deals physical damage to the enemy
        /// </summary>
        /// <param name="damage">Amount of damage to be delt</param>
        public abstract void TakePhysicalDamage(int damage, Player player);
        #region Old TakePhyiscalHealth(dmg) method
        ////Random Modifier to Defense. This adds a slight 
            ////random element to the damage received
            ////currently iDamageMod = 5
            //int iBlockedDamage = RandomHelper.GetRandomInt(iLevel, iLevel * iDefendDamageMod);
            //int iDamageDone = damage - (iArmor + iBlockedDamage);  //Determines final Damage Done to player

            //ModifyHealth(iDamageDone, player);
        #endregion

        /// <summary>
        /// Deals magical damage to the enemy
        /// </summary>
        /// <param name="damage">Amount of damage to be delt</param>
        public abstract void TakeMagicalDamage(int damage, Player player);
        #region Old TakeMagicalDamage() method
        //{//NOTE: As of right now this method works EXACTALLY like TakePhysicalDamage()

        //    //Random Modifier to Defense. This adds a slight 
        //    //random element to the damage received
        //    //currently iDamageMod = 5
        //    int iBlockedDamage = RandomHelper.GetRandomInt(iLevel, iLevel * iDefendDamageMod);
        //    int iDamageDone = damage - (iArmor + iBlockedDamage);  //Determines final Damage Done to player

        //    ModifyHealth(iDamageDone, player);
        //}//end TakeMagicalDamage(damage)
        #endregion

        /// <summary>
        /// Modifys the enemy health based on weather or not the enemy was
        /// damaged or healed in the public method.
        /// </summary>
        /// <param name="damage">Damage delt to player</param>
        /// <param name="healed">Health gained by player</param>
        public void ModifyHealth(int damage, Player player)
        {
            if (damage != 0)        //If enemy has taken any damage
            {
                bool added = false; //ensures the dmg text animation only adds one instance of dmg taken

                int dmg = (int)MathHelper.Max(0, damage);   //takes the max value so damage can not be 0 or -
                iHealth -= dmg;                             //ajusts the players health

                //initiates the array that houses the damage taken this round
                for (int i = 0; i < 100; i++)
                {
                    if (!added)
                    {
                        if (iDamageTaken[i] == 0)    //if the current block is empty
                        {
                            iDamageTaken[i] = dmg;   //add the dmg to the current block
                            added = true;           //set bool to true, so dmg is ONLY added to current block
                        }
                    }//end !added
                }//end loop

                //check to see if player has run out of health
                if (iHealth <= 0)
                    HasDied(player);      //if player is dead, run the HasDied() method

            }//end Damage
        }//end ModifyHealth(damage,healed)

        /// <summary>
        /// Rewards the enemy with gold
        /// </summary>
        /// <param name="goldAdded">amount of gold to be awarded to player</param>
        public void AddGold(int goldAdded)
        {
            iGold += goldAdded;     //adds gold to the players inventory
        }//end AddGold(goldAdded)

        /// <summary>
        /// Kills the enemy if the enemy health has dropped below zero
        /// </summary>
        private void HasDied(Player player)
        {
            int exp = 0;
            int iLevelDifference = iLevel - player.Level;

            if (iLevelDifference >= 5)      //if the player is +5 levels+ above the monster
                iLevelDifference = 5;           //set the switch value to be 5

            if (iLevelDifference <= -5)     //if the player is -5 levels+ under the monster
                iLevelDifference = -5;          //set the switch value to be -5

            //Exp is based on the level difference of the player to monster.
            // If the monster is 5 levels under the player, the player will 
            // only be awarded 10 exp, but if they're the same level, the
            // player will be awarded 100 exp.
            switch (iLevelDifference)
            {
                case -5:
                    exp = 10;
                    break;
                case -4:
                    exp = 20;
                    break;
                case -3:
                    exp = 30;
                    break;
                case -2:
                    exp = 60;
                    break;
                case -1:
                    exp = 80;
                    break;
                case 0:         //same level
                    exp = 100;
                    break;
                case 1:
                    exp = 150;
                    break;
                case 2:
                    exp = 200;
                    break;
                case 3:
                    exp = 225;
                    break;
                case 4:
                    exp = 250;
                    break;
                case 5:
                    exp = 275;
                    break;
            }//end switch

            player.AddExp(exp);
            player.AddGold(iGold);

        }//end HasDied()

        #endregion

        #region Draw and Animation methods

        /// <summary>
        /// Handles the render logic for the enemy character
        /// </summary>
        /// <param name="spriteBatch">Helps render sprites to screen</param>
        /// <param name="gameTime">Game Timer</param>
        /// <param name="font">Font for drawing text to screen</param>
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// Handles the scrolling text animations above the enemy sprite
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        public void DrawScrollingText(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;   //elapsed time for timers

            #region Damage Taken Text
            //goes through the ExpAdded array to determine which text to draw on screen
            for (int i = 0; i < 100; i++)
            {
                if (DamageTaken[i] != 0)                //if the current field is not empty
                {
                    TimerDamageText[i] += elapsed;     //update the current fields timer

                    //draw the dmg taken text on screen
                    spriteBatch.DrawString(font, "-" + DamageTaken[i],
                        new Vector2(Location.X + 10, (Location.Y - CurrentSprite.Height + 20) - (25 * TimerDamageText[i])),
                        Color.Red);
                }

                //if the current fields timer reaches the cap
                if (TimerDamageText[i] >= TimerDamageTextCap)
                {
                    DamageTaken[i] = 0;         //clear the field
                    TimerDamageText[i] = 0;    //stop the timer
                }
            }//end for loop

            #endregion

            #region Health Recovered Text
            //goes through the ExpAdded array to determine which text to draw on screen
            for (int i = 0; i < 100; i++)
            {
                if (HealthRecovered[i] != 0)           //if the current field is not empty
                {
                    TimerHealthText[i] += elapsed;     //update the current fields timer

                    //draws the health recovered text on screen
                    spriteBatch.DrawString(font, "+" + HealthRecovered[i],
                        new Vector2(Location.X + 10, (Location.Y - CurrentSprite.Height + 20) - (25 * TimerHealthText[i])),
                        Color.Green);
                }

                if (TimerHealthText[i] >= TimerHealthTextCap) //if current fields timer reaches cap
                {
                    HealthRecovered[i] = 0;                    //clear the current field
                    TimerHealthText[i] = 0;                    //reset the current fields timer
                }
            }//end for loop
            #endregion //empty

        }
        
        /// <summary>
        /// Starts the idle animation for when the enemy is waiting for their turn
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        public abstract void StartIdleAnimation(GameTime gameTime);

        /// <summary>
        /// Starts the attack animation for the enemy
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        public abstract void StartAttackAnimation(GameTime gameTime);
        #region Old StartAttackAnimation(Gametime gameTime)
        //{
        //    if (fAttackAnimationTimer == 0)     //if Attack timer is not started
        //    {
        //        iFrame = 1;                     //sets the current frame to the first frame of animation
        //        fAttackAnimationTimer += 0.1f;  //sets the timer to != 0
        //        fSpeed = 0.0f;                  //Sets the player speed to 0 for the remainder of the animation

        //    }//end if
        //}//end StartAttackAnimation(gameTime)
        #endregion

        /// <summary>
        /// Updates the animation frames
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        public abstract void UpdateAnimations(GameTime gameTime);
        #region Old UpdateAnimation(GameTime gameTime)
        //{
        //    if (fAttackAnimationTimer == 0)         // if animation timer is at 0, attack has not happened
        //    {
        //        iFrame = 0;                         //play the walking frame
        //    }//end walking animation

        //    if (fAttackAnimationTimer > 0)          //if animation timer is > 0, attack is in progress
        //    {
        //        //Updates the total seconds elapsed since last call
        //        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        //        //updates the attack animation timer
        //        fAttackAnimationTimer += elapsed;

        //        //If the total seconds elapsed is greater than the frame count
        //        // execute next frame of the animation
        //        if (fAttackAnimationTimer > fAnimationFrames)
        //        {
        //            //If the current frame of animation isn't the final frame (3rd frame)
        //            if (iFrame != 3)
        //            {
        //                iFrame++;                       //increase frame by 1
        //                fAttackAnimationTimer = 0.01f;  //reset animation timer
        //            }

        //            //if the current frame of animation IS the final frame (3rd frame)
        //            else if (iFrame == 3)
        //            {
        //                iFrame = 0;                     //set the frame to the walking frame
        //                fAttackAnimationTimer = 0.0f;   //stop animation
        //                fSpeed = fSpeedReset;           //re-initiates the player speed so they can move
        //            }
        //        }//end Animation If Statement
        //    }//end play attack animation

        //    tCurSprite = tSprite[iFrame];       //sets the sprite texture to the correct frame

        //}//end UpdateAnimation
        #endregion

        #endregion

    }//end enemy
}
        