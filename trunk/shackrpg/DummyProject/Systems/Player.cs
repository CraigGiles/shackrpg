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
using BattleEngineV1;


namespace DummyProject.Systems
{
    /// <summary>
    /// TODO: Update the StartAttackAnimation method, and the UpdateAnmations method
    /// with the battle animation logic that will be utalized by the player.
    /// 
    /// Next step is to offshoot these animation methods into an Animation Handler class,
    /// so if I ever choose to add a "party system" they can all utalize the same animation
    /// techniques without having to write the code for every one.
    /// 
    /// EDIT: I could alos just add "Player Chrono = new Player()" "Player Marle = new Player()" 
    /// and modify based on that.
    /// </summary>
    public class Player
    {
        #region Variables

        #region Animation Variables
        /// <summary>
        /// Houses the text information until the timers for their
        /// animation has expired
        /// </summary>
        int[] damageTaken = new int[100];
        int[] iHealthRecovered = new int[100];
        int[] iExpAdded = new int[100];

        /// <summary>
        /// LevelUp! text animation timers
        /// </summary>
        float fLevelTextTimer = 0.0f;
        float fLevelTextTimerCap = 2.0f;

        /// <summary>
        /// +## Exp text animation timers
        /// </summary>
        float[] fExpTextTimer = new float[100];
        float fExpTextTimerCap = 2.0f;

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
        /// Player ATB Battle Timers
        /// </summary>
        float fATBTimer = 0.0f;
        float fATBTimerCap = 5.0f;

        /// <summary>
        /// Players battle "idle" timers
        /// </summary>
        float fIdleBattleAnimationTimer = 0.0f;
        float fIdleBattleAnimationTimerCap = 0.50f;

        /// <summary>
        /// Overworld Animation Timers
        /// </summary>
        float fWalkingTimer = 0.0f;
        float fWalkingTimerCap = .32f;

        /// <summary>
        /// Bool values used to determine the last direction
        /// player was moving
        /// </summary>
        bool bMoveDown = true, bMoveLeft, bMoveUp, bMoveRight;

        /// <summary>
        /// Source Rectangles for the Hero Sprite sheet
        /// </summary>
        Rectangle
            rDownStanding = new Rectangle(0, (35 * 0), 35, 35),
            rDownWalking1 = new Rectangle(0, (35 * 1), 35, 35),
            rDownWalking2 = new Rectangle(0, (35 * 2), 35, 35),
            rDownBattleIdle1 = new Rectangle(0, (35 * 3), 35, 35),
            rDownBattleIdle2 = new Rectangle(0, (35 * 4), 35, 35),
            rDownAttack1 = new Rectangle(0, (35 * 5), 35, 35),
            rDownAttack2 = new Rectangle(0, (35 * 6), 35, 35);
        Rectangle
            rLeftStanding = new Rectangle((35 * 1), (35 * 0), 35, 35),
            rLeftWalking1 = new Rectangle((35 * 1), (35 * 1), 35, 35),
            rLeftWalking2 = new Rectangle((35 * 1), (35 * 2), 35, 35),
            rLeftBattleIdle1 = new Rectangle((35 * 1), (35 * 3)+3, 35, 32),
            rLeftBattleIdle2 = new Rectangle((35 * 1), (35 * 4)+3, 35, 32),
            rLeftAttack1 = new Rectangle((35 * 1), (35 * 5)+3, 35, 32),
            rLeftAttack2 = new Rectangle((35 * 1), (35 * 6)+3, 35, 32);
        Rectangle
            rUpStanding = new Rectangle((35 * 2), (35 * 0), 35, 35),
            rUpWalking1 = new Rectangle((35 * 2), (35 * 1), 35, 35),
            rUpWalking2 = new Rectangle((35 * 2), (35 * 2), 35, 35);

        //with the use of the effect flip in the draw method, i don't need this anymore
        //Rectangle
        //    rRightStanding = new Rectangle((35 * 3), (35 * 0), 35, 35),
        //    rRightWalking1 = new Rectangle((35 * 3), (35 * 1), 35, 35),
        //    rRightWalking2 = new Rectangle((35 * 3), (35 * 2), 35, 35);
        #endregion

        #region Character Variables
        /// <summary>
        /// Strings for the players name and title
        /// </summary>
        string sName;               //Players Name
        string sTitle;              //Players Title

        /// <summary>
        /// Players stats
        /// </summary>
        int iHealth = 1000,          //Current Health
            iHealthMax = 1000,       //Max Health for Level
            iStrength = 50,         //Current Strength
            iStrengthBase = 50,     //STR not including buffs or +STR items
            iAttackPower = 70,      //current ATK
            iAttackPowerBase = 50,  //ATK not including buffs or +ATK items
            iArmor = 30,            //current Armor
            iArmorBase = 30,        //Armor not including buffs or +Armor items
            iGold = 0,              //Current gold
            iLevel = 1,             //Current Level
            iExpCur = 0,            //Current Experience Points
            iExpNextLvl;            //Exp needed until next level

        /// <summary>
        /// Exp and Level variables
        /// </summary>
        const int iMaxLevel = 60;
        int[] iExpTable = new int[iMaxLevel + 1];


        /// <summary>
        /// Bonuses added to current stats on LevelUp()
        /// </summary>
        int iLvlBonusHealth = 50,
            iLvlBonusStrength = 9,
            iLvlBonusAttack = 7,
            iLvlBonusArmor = 5;

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
        /// Bool statements to indicate the current state of the player
        /// </summary>
        bool bHaste,            //player hasted (currently unused)
             bSlow,             //player slowed (currently unused)
             bLevelUp;          //used in the level up text animation

        #endregion

        #region System Variables

        //I used this in my old way of doing the animation, before
        //I started using a SpriteSheet.
        /// <summary>
        /// 0 = Walking
        /// 1, 2, 3 = Attack animation frames
        /// 4 = Dead
        /// </summary>
        //Texture2D[] tSprite = new Texture2D[5];       

        /// <summary>
        /// Shows the current sprite to be rendered to screen
        /// </summary>
        Rectangle rCurSprite;
        
        /// <summary>
        /// Sprite sheet housing all sprites used to animate
        /// the player object
        /// </summary>
        Texture2D tSpriteSheet;

        /// <summary>
        /// Sprite box and attack box for player.
        /// </summary>
        Rectangle
            rSpriteRect = new Rectangle(0, 0, 108, 93);  //(0, 0, 72, 62) = real sprite dems
            //rAttackBox = new Rectangle(0,0,36,62);  //Used in SoM style game where there is no "ff style battle"

        /// <summary>
        /// Players location on the field of play
        /// </summary>
        Vector2 vCurLocation, vPrevLocation, vLocBeforeBattle;
        #endregion 

        #region Battle Variables
        Rectangle
            rBattleMenuBackground = new Rectangle(0, 440, 800, 130),
            rBattleActionSelect = new Rectangle(300, 440, 175, 130),
            rBattlePlayerStats = new Rectangle(475, 440, 325, 130);

        /// <summary>
        /// Locations of the battle menu text
        /// </summary>
        Vector2 vFightText, vDefendText, vRunText;
        Vector2 vHealthText, vHealthValue, vHealthMaxValue;
        Vector2 vNameText, vATBTimerText, vATBTimerValue;

        /// <summary>
        /// Timers to control how quickly you can scroll through battle commands
        /// </summary>
        float fChangeBattleCommand, fChangeBattleCommandCap = 0.15f;

        /// <summary>
        /// List of available Battle Commands
        /// </summary>
        public enum BattleCommands
        {
            Fight,
            Defend,
            Run,
        }

        /// <summary>
        /// The current command selected by player
        /// </summary>
        BattleCommands curCommand, prevCommand;
        #endregion

        #endregion

        #region Properties

        #region Health
        /// <summary>
        /// Gets the players current Health
        /// </summary>
        public int Health
        {
            get { return iHealth; }
        }
        #endregion

        #region Strength
        /// <summary>
        /// Gets the players current Strength
        /// </summary>
        public int Strength
        {
            get { return iStrength; }
        }
        #endregion

        #region AttackPower
        /// <summary>
        /// Gets the players current Attack Power including +ATK benefits
        /// </summary>
        public int AttackPower
        {
            get { return iAttackPower; }
        }
        #endregion

        #region Armor
        /// <summary>
        /// Gets the players current Armor including any +Armor benefits
        /// </summary>
        public int Armor
        {
            get { return iArmor; }
            set { iArmor = value; }
        }
        #endregion

        #region Gold
        /// <summary>
        /// Gets the players current gold
        /// </summary>
        public int Gold
        {
            get { return iGold; }
        }
        #endregion

        #region Level
        /// <summary>
        /// Gets the players current Level
        /// </summary>
        public int Level
        {
            get { return iLevel; }
        }
        #endregion

        #region Exp
        /// <summary>
        /// Gets the players current EXP
        /// </summary>
        public int Exp
        {
            get { return iExpCur; }
        }

        /// <summary>
        /// Gets the EXP needed until player gains a level
        /// </summary>
        public int ExpNeededToLevel
        {
            get
            {
                iExpNextLvl = iExpTable[iLevel];
                return iExpNextLvl;
            }
        }
        #endregion

        #region AttackRange
        //This is used in SoM style battles, currently I am making
        // a battle engine for FF style RPG so the AttackBox is unused.
        // The Attack box covers the first 1/3rd of teh sprite (just enough
        // for the sword animation) and is used to see if the 
        //attackBox.Intersects(enemy) for dealing damage
        ///// <summary>
        ///// Gets the Attack Range of the current Weapon
        ///// </summary>
        //public Rectangle AttackRange
        //{
        //    get { return rAttackBox; }
        //}
        #endregion

        #region Battle Commands
        /// <summary>
        /// Gets the current battle command selected by the player
        /// </summary>
        public BattleCommands CurrentBattleCommand
        {
            get { return curCommand; }
        }

        public float ATBTimer
        {
            get { return fATBTimer; }
        }

        public float ATBTimerCap
        {
            get { return fATBTimerCap; }
        }
        #endregion

        #region Location
        /// <summary>
        /// Gets or Sets the current location Vector2
        /// </summary>
        public Vector2 CurrentLocation
        {
            get { return vCurLocation; }
            set { vCurLocation = value; }
        }

        /// <summary>
        /// Gets or sets the previous location Vector2
        /// </summary>
        public Vector2 PreviousLocation
        {
            get { return vPrevLocation; }
            set { vPrevLocation = value; }
        }

        /// <summary>
        /// Stores the players previous map location while the player
        /// is in a battle.
        /// </summary>
        public Vector2 LocationBeforeBattle
        {
            get { return vLocBeforeBattle; }
            set { vLocBeforeBattle = value; }
        }
        /// <summary>
        /// Returns the last direction player was moving in the overworld map
        /// </summary>        
        public string LastDirection
        {
            get
            {
                if (bMoveDown == true)
                    returnValue = "down";

                else if (bMoveLeft == true)
                    returnValue = "left";

                else if (bMoveUp == true)
                    returnValue = "up";

                else if (bMoveRight == true)
                    returnValue = "right";

                return returnValue;
            }
        }
        string returnValue;
        #endregion

        #region Sprites
        /// <summary>
        /// Gets or sets the Current Sprite rectangle for locating
        /// the current sprite from the spritesheet
        /// </summary>
        public Rectangle CurrentSprite
        {
            get { return rCurSprite; }
            set { rCurSprite = value; }
        }

        public Rectangle DownStanding
        {
            get { return rDownStanding; }
            //set { rDownStanding = value; }
        }

        public Rectangle DownWalking1
        {
            get { return rDownWalking1; }
            //set { rDownWalking1 = value; }
        }
        public Rectangle DownWalking2
        {
            get { return rDownWalking2; }
            //set { rDownWalking2 = value; }
        }
        public Rectangle DownBattleIdle1
        {
            get { return rDownBattleIdle1; }
            //set { rDownBattleIdle1 = value; }
        }
        public Rectangle DownBattleIdle2
        {
            get { return rDownBattleIdle2; }
            //set { rDownBattleIdle2 = value; }
        }
        public Rectangle DownAttack1
        {
            get { return rDownAttack1; }
            //set { rDownAttack1 = value; }
        }
        public Rectangle DownAttack2
        {
            get { return rDownAttack2; }
            //set { rDownAttack2 = value; }
        }
        public Rectangle LeftStanding
        {
            get { return rLeftStanding; }
            //set { rLeftStanding = value; }
        }

        public Rectangle LeftWalking1
        {
            get { return rLeftWalking1; }
            //set { rLeftWalking1 = value; }
        }
        public Rectangle LeftWalking2
        {
            get { return rLeftWalking2; }
            //set { rLeftWalking2 = value; }
        }
        public Rectangle LeftBattleIdle1
        {
            get { return rLeftBattleIdle1; }
            //set { rLeftBattleIdle1 = value; }
        }
        public Rectangle LeftBattleIdle2
        {
            get { return rLeftBattleIdle2; }
            //set { rLeftBattleIdle2 = value; }
        }
        public Rectangle LeftAttack1
        {
            get { return rLeftAttack1; }
            //set { rLeftAttack1 = value; }
        }
        public Rectangle LeftAttack2
        {
            get { return rLeftAttack2; }
            //set { rLeftAttack2 = value; }
        }
        public Rectangle UpStanding
        {
            get { return rUpStanding; }
        }
        public Rectangle UpWalking1
        {
            get { return rUpWalking1; }
        }
        public Rectangle UpWalking2
        {
            get { return rUpWalking2; }
        }

        #endregion

        #endregion

        Texture2D tBarBackground;
        #region Constructors
        public Player(ContentManager content)
            {
                //Loads the hero spritesheet into tSpriteSheet
                tSpriteSheet = content.Load<Texture2D>(@"Hero/HeroSprites");
                tBarBackground = content.Load<Texture2D>(@"Hero/BottomBackground");

                //Sets the current sprite to Standing (default)
               CurrentSprite = rDownStanding;

                /*Sets the exp table for how much exp is needed for the character
                 * to level up. Right now its set to times the level by 350. This
                 * leads to a very long grind in later levels due to how exp is 
                 * rewarded in the HasDied() method of the enemies, so this table
                 * may be reduced to level * 250 or something in the future.
                 * 
                 * To be determined when actually playtesting the game!*/
                for (int i = 0; i < (iMaxLevel + 1); i++)
                {
                    int exp;                //declares the exp variable

                    exp = i * 350;          //sets the amnt of exp needed for that level
                    iExpTable[i] = exp;     //fills in the exp table with the exp needed
                }

                sName = "Chrono";
            }//end Player()

        public Player()
        {
        }
        #endregion

        #region Methods

        public void UpdateOverworld(GameTime gameTime)
        {

            Update(gameTime);
        }


        public void UpdateBattle(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fChangeBattleCommand += elapsed;

            if (prevCommand == BattleCommands.Defend && ATBTimer >= ATBTimerCap)
                iArmor = iArmorBase; //+any +AC items 

            #region Highlighting Battle Commands Based on Input
            if (InputHelper.NextBattleCommand && fChangeBattleCommand >= fChangeBattleCommandCap)
            {
                switch (curCommand)
                {
                    case BattleCommands.Fight:
                        curCommand = BattleCommands.Defend;
                        break;
                    case BattleCommands.Defend:
                        curCommand = BattleCommands.Run;
                        break;
                    case BattleCommands.Run:
                        curCommand = BattleCommands.Fight;
                        break;
                    default:
                        break;
                }//end switch

                fChangeBattleCommand = 0.0f;
            }//end NextBattleCommand

            else if (InputHelper.PreviousBattleCommand && fChangeBattleCommand >= fChangeBattleCommandCap)
            {
                switch (curCommand)
                {
                    case BattleCommands.Fight:
                        curCommand = BattleCommands.Run;
                        break;
                    case BattleCommands.Defend:
                        curCommand = BattleCommands.Fight;
                        break;
                    case BattleCommands.Run:
                        curCommand = BattleCommands.Defend;
                        break;
                    default:
                        break;
                }//end switch
                fChangeBattleCommand = 0.0f;
            }//end elseif
            #endregion

            Update(gameTime);
        }

        /// <summary>
        /// Items in this method need to update no matter which state the game is in
        /// Updates the players sprite boxes, animations, and stats
        /// </summary>
        /// <param name="gameTime">Games timer</param>
        public void Update(GameTime gameTime)
        {   
            //Updates the Sprite rectangle to be the same as the players location
            rSpriteRect.X = (int)CurrentLocation.X;
            rSpriteRect.Y = (int)CurrentLocation.Y;

            //iArmor = iArmorBase;                // TODO: + any item mods
            //iStrength = iStrengthBase;          // TODO: + any item mods
            //iAttackPower = iAttackPowerBase;    // TODO: + any item mods

            UpdateTimers(gameTime);             //Updates any active timers ((TODO: Impliment))
            UpdateAnimation(gameTime);          //Updates any animations the player may be executing

            vPrevLocation = CurrentLocation;    //The very last thing we want to update. Sets prev location to cur location
        }//end Update(gameTime)

        /// <summary>
        /// Updates the timers associated with the player. Currently, only
        /// the ATB timer is hosted here.
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        private void UpdateTimers(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (fATBTimer > 0)
            {
                fATBTimer += elapsed;
            }
        }//end UpdateTimers(gameTime)

        /// <summary>
        /// Moves the player in the specified direction
        /// </summary>
        public void Move()
        {            
            vPrevLocation = CurrentLocation;        //sets the previous location to current location
                                                    //Ajusts the current location based on direction player is moving
            if (InputHelper.Up)
            { 
                vCurLocation.Y -= fSpeed;
                bMoveDown = false;
                bMoveLeft = false;
                bMoveUp = true;
                bMoveRight = false;
            }
            else if (InputHelper.Down)
            {
                vCurLocation.Y += fSpeed;
                bMoveDown = true;
                bMoveLeft = false;
                bMoveUp = false;
                bMoveRight = false;
            }
            else if (InputHelper.Left)
            {
                vCurLocation.X -= fSpeed;
                bMoveDown = false;
                bMoveLeft = true;
                bMoveUp = false;
                bMoveRight = false;
            }
            else if (InputHelper.Right)
            {
                vCurLocation.X += fSpeed;
                bMoveDown = false;
                bMoveLeft = false;
                bMoveUp = false;
                bMoveRight = true;
            }
        }//end Move()

        /// <summary>
        /// Sets the character into battle status
        /// </summary>
        /// <param name="gameTime"></param>
        public void StartBattle(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            curCommand = 0;
            LocationBeforeBattle = CurrentLocation; //Stores the current location of the player for later use
            fAttackAnimationTimer = 0.0f;           //Sets the attack animation to 0; 
                                                        //if battle just started, no way you can be attacking
            fATBTimer += elapsed;                   //Initiates ATB battle timer
        }

        /// <summary>
        /// Reverts the character back to overworld map status
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        public void EndBattle(GameTime gameTime)
        {
            fATBTimer = 0.0f;              //sets the battle timer to 0.0 so it will not be updated
            fIdleBattleAnimationTimer = 0.0f;       //sets idle battle animation timer to 0
            
            CurrentLocation = LocationBeforeBattle; //Places the character in the same location as before the battle

            switch (LastDirection)                  //Gets the last direction the character was walking
            {                                       //and sets the sprite accordingly
                case "down":
                    CurrentSprite = DownStanding;
                    break;
                case "left":
                    CurrentSprite = LeftStanding;
                    break;
                case "up":
                    CurrentSprite = UpStanding;
                    break;
                case "right":
                    CurrentSprite = LeftStanding;
                    break;
            }//end LastDirection
        }//end EndBattle(gameTime)

        /// <summary>
        /// Plays attack animation and determins damage delt to opponents in range
        /// </summary>
        /// <param name="gameTime">Games timer</param>
        /// <returns>The damage delt by the player attack</returns>
        public int Attack(GameTime gameTime)
        {
            int iDamageDone = 0;                    //damage done by the attack

            //if the ATB battle timer is not at cap, don't let the player attack
            if (fATBTimer >= fATBTimerCap)             
            {
                StartAttackAnimation(gameTime);     //starts the players attack animation

                //Random Modifier to attack power. This adds a slight random element to the attack
                //currently iDamageMod = 5
                int iRandomMod = RandomHelper.GetRandomInt(iLevel, iLevel * iAttackDamageMod);
                iDamageDone = iAttackPower + iRandomMod;

                fATBTimer = 0.01f;                  //resets the ATB timer
                prevCommand = BattleCommands.Fight; //Sets "Fight" as the previously selected BattleCommand
            }//end start attack

            return iDamageDone;
        }//end Attack(gameTime)

        /// <summary>
        /// Sets the character to "Blocking" raising their armor value
        /// </summary>
        public void Block()
        {
            Armor += Level * 25;                    //sets the ajusted "blocking" armor value
            fATBTimer = 0.01f;                      //Resets the ATB timer
            prevCommand = BattleCommands.Defend;     //sets Defend as the previously selected command
        }//end Block()

        /// <summary>
        /// Adds health to the player
        /// </summary>
        /// <param name="healthAdded">Health to be added</param>
        public void AddHealth(int healed)
        {
            #region Heal the player
            if (healed >= 0)
            {
                bool added = false; //ensures the dmg text animation only adds one instance of dmg taken

                int hlth = (int)MathHelper.Max(0, healed);  //takes the max value so damage can not be 0 or -
                iHealth += hlth;                            //ajusts the players health

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

            }//end HealPlayer
            #endregion
        }

        /// <summary>
        /// Deals physical damage to the player
        /// </summary>
        /// <param name="damage">Amount of damage to be delt</param>
        public void TakePhysicalDamage(int damage)
        {
            //Random Modifier to Defense. This adds a slight random element to the damage received
            //currently iDamageMod = 5
            int iBlockedDamage = RandomHelper.GetRandomInt(iLevel, iLevel * iDefendDamageMod);
            int iDamageDone = damage - (iArmor + iBlockedDamage);  //Determines final Damage Done to player

            DamagePlayer(iDamageDone);
        }//end TakePhysicalDamage(damage)

        /// <summary>
        /// Deals magical damage to the player
        /// </summary>
        /// <param name="damage">Amount of damage to be delt</param>
        public void TakeMagicalDamage(int damage)
        {//NOTE: As of right now this method works EXACTALLY like TakePhysicalDamage()

            //Random Modifier to Defense. This adds a slight random element to the damage received
            //currently iDamageMod = 5
            int iBlockedDamage = RandomHelper.GetRandomInt(iLevel, iLevel * iDefendDamageMod);
            int iDamageDone = damage - (iArmor + iBlockedDamage);  //Determines final Damage Done to player

            DamagePlayer(iDamageDone);
        }//end TakeMagicalDamage(damage)

        /// <summary>
        /// Modifys the players health based on weather or not the player was
        /// damaged or healed in the public method.
        /// </summary>
        /// <param name="damage">Damage delt to player</param>
        /// <param name="healed">Health gained by player</param>
        private void DamagePlayer(int damage)
        {
            #region Damage the player
            //If player has taken any damage
            //if (damage != 0)
            //{
                bool added = false; //ensures the dmg text animation only adds one instance of dmg taken

                int dmg = (int)MathHelper.Max(0, damage);   //takes the max value so damage can not be 0 or -
                iHealth -= dmg;                             //ajusts the players health

                //initiates the array that houses the damage taken this round
                for (int i = 0; i < 100; i++)
                {
                    if (!added)
                    {
                        if (damageTaken[i] == 0)    //if the current block is empty
                        {
                            damageTaken[i] = dmg;   //add the dmg to the current block
                            added = true;           //set bool to true, so dmg is ONLY added to current block
                        }
                    }//end !added
                }//end loop

                //check to see if player has run out of health
                //if (iHealth <= 0)
                //    HasDied();      //if player is dead, run the HasDied() method

            //}//end DamagePlayer
            #endregion

           

        }//end DamagePlayer(damage,healed)

        /// <summary>
        /// Rewards the player with experience points
        /// </summary>
        /// <param name="expAdded">Amount of exp to be awarded to player</param>
        public void AddExp(int expAdded)
        {
            bool added = false;                 //ensures the exp text animation only adds one instance of text

            iExpCur += expAdded;                //add exp to players current exp

            if (iExpCur >= ExpNeededToLevel)    //Check to see if player has enough to level
                LevelUp();                          //if yes, LevelUp() the player

            //initiates the exp array for text animation
            for (int i = 0; i < 100; i++)
            {
                if (!added)
                {
                    if (iExpAdded[i] == 0)          //if current block is empty
                    {
                        iExpAdded[i] = expAdded;    //add value to the current block
                        added = true;               //set bool to true to ensure only 1 block gets updated
                    }//end if
                }//end !added
            }//end loop

        }//end AddExp(expAdded)

        /// <summary>
        /// Levels the player when the player has enough experience to gain a level.
        /// </summary>
        private void LevelUp()
        {
            if (iLevel < iMaxLevel)                     //Only ajusts stats and raises level if player is lower than max level
            {
                iLevel++;                               //increases player level by 1

                iHealthMax += iLvlBonusHealth;          //adds bonus to players health
                iStrengthBase += iLvlBonusStrength;     //adds bonus to players str
                iAttackPowerBase += iLvlBonusAttack;    //adds bonus to players atk
                iArmorBase += iLvlBonusArmor;           //adds bonus to players armor

                iHealth = iHealthMax;                   //sets the players cur health to max health
                iExpCur = 0;                            //Sets the current exp to 0

                bLevelUp = true;                        //sets bool value to true to initiate text animation
                //TODO: Play Sound                      //plays level up "GONG" (ugh, Everquest memories!)
            }

        }//end LevelUp()

        /// <summary>
        /// Rewards the player with gold
        /// </summary>
        /// <param name="goldAdded">amount of gold to be awarded to player</param>
        public void AddGold(int goldAdded)
        {
            iGold += goldAdded;     //adds gold to the players inventory

            //maybe someday i'll have the text animation initiate for gold as well
            //but for now I wont

        }//end AddGold(goldAdded)

        /// <summary>
        /// Kills the player if the players health has dropped below zero
        /// </summary>
        public void HasDied(GameTime gameTime, BaseGame game)
        {
            /* maybe i'll have a method that sends the player back to the last
             * "save point" with an exp hit instead of forcing the player to 
             * restart from the last save file */

            EndBattle(gameTime);                //Runs the EndBattle method for player
            //Add "GameOver" screen             //Creates a GameOver screen
            game.RemoveCurrentGameScreen();     //Removes the current screen
            
        }//end HasDied()

        #endregion

        #region Draw and Animation methods

        /// <summary>
        /// Handles the render logic for the player character in overworld maps
        /// </summary>
        /// <param name="spriteBatch">Helps render sprites to screen</param>
        /// <param name="gameTime">Game Timer</param>
        /// <param name="font">Font for drawing text to screen</param>
        public void DrawPlayer(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;   //elapsed time for timers
            UpdateAnimation(gameTime);                                      //updates the players animations

            #region Draw Player Overworld Map
            if (fATBTimer == 0)
            {
                //if last direction was anything but right, draw sprite normally
                if (LastDirection != "right")
                    spriteBatch.Draw(tSpriteSheet, rSpriteRect, CurrentSprite, Color.White);

                //if direction was right, use the 'left' sprite and flip it horizontally
                else if (LastDirection == "right")
                    spriteBatch.Draw(tSpriteSheet, rSpriteRect, 
                        CurrentSprite, Color.White, 0.0f, 
                        Vector2.Zero, SpriteEffects.FlipHorizontally, 
                        0.0f);
            }
            #endregion

            DrawTextAnimations(spriteBatch, gameTime, font);
        }
        //end DrawPlayer(spriteBatch, gameTime)

        /// <summary>
        /// Handles the render logic for the player character in battle situations
        /// </summary>
        /// <param name="spriteBatch">Renders the sprite to screen</param>
        /// <param name="gameTime">Game Timer</param>
        /// <param name="font">Font for drawing text to screen</param>
        /// <param name="battle">Battle Renderer</param>
        public void DrawPlayer(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font, Battle battle)
        {
            #region Text Menu Vertex2 values
            vFightText = new Vector2(rBattleActionSelect.X + 37, rBattleActionSelect.Y + 7);
            vDefendText = new Vector2(rBattleActionSelect.X + 30, rBattleActionSelect.Y + 47);
            vRunText = new Vector2(rBattleActionSelect.X + 55, rBattleActionSelect.Y + 85);

            vNameText = new Vector2(500, 450);
            vATBTimerText = new Vector2(640, 450);
            vATBTimerValue = new Vector2(725, 450);

            vHealthText = new Vector2(510, 500);
            vHealthValue = new Vector2(590, 500);
            vHealthMaxValue = new Vector2(660, 500);
            #endregion

            #region Command Menu Colors

            Color fightColor, defendColor, runColor;

            switch (curCommand)
            {
                case BattleCommands.Fight:
                    fightColor = Color.Orange;
                    defendColor = Color.White;
                    runColor = Color.White;
                    break;
                case BattleCommands.Defend:
                    fightColor = Color.White;
                    defendColor = Color.Orange;
                    runColor = Color.White;
                    break;
                case BattleCommands.Run:
                    fightColor = Color.White;
                    defendColor = Color.White;
                    runColor = Color.Orange;
                    break;
                default:
                    fightColor = Color.White;
                    defendColor = Color.White;
                    runColor = Color.White;
                    break;
            }
            #endregion
            #region Draw Player in Battle
            if (fATBTimer > 0)  //If ATB is not at 0, battle has begun
            {
                //houses information on where Player1's sprite is to be drawn
                Rectangle battleLocation = battle.PlayerSprite1;    

                //changes the player sprite to location determined by battle engine
                CurrentLocation = new Vector2((float)battleLocation.X, (float)battleLocation.Y);

                //draws the current sprite at new location
                spriteBatch.Draw(tSpriteSheet, rSpriteRect, CurrentSprite, Color.White);
                
                //Draws the menu bar background
                spriteBatch.Draw(tBarBackground, rBattleMenuBackground, Color.White);

                //draws the player stats portion of menubar
                spriteBatch.Draw(tBarBackground, rBattlePlayerStats, Color.White);
                spriteBatch.DrawString(font, sName, vNameText, Color.White);

                #region Draws ATB information
                //Converts iATBText to a % that can be drawn on the screen
                int iATBText = (int)((fATBTimer / fATBTimerCap) * 100);
                if (iATBText >= 100)
                    iATBText = 100;

                spriteBatch.DrawString(font, "ATB:", vATBTimerText, Color.White);
                spriteBatch.DrawString(font, iATBText.ToString() + "%", vATBTimerValue, Color.White);
                #endregion

                #region Draws Health Values
                spriteBatch.DrawString(font, "HP: ", vHealthText, Color.White);
                spriteBatch.DrawString(font, iHealth.ToString(), vHealthValue, Color.White);
                spriteBatch.DrawString(font, " / " + iHealthMax.ToString(), vHealthMaxValue, Color.White);
                #endregion

                #region Draws Command Menu
                if (fATBTimer >= fATBTimerCap) // && bCharacterSelected == true)
                {
                    if (curCommand != BattleCommands.Fight &&
                        curCommand != BattleCommands.Defend &&
                        curCommand != BattleCommands.Run)
                        curCommand = BattleCommands.Fight;

                    //TODO: Play "Ding!" sound to indicate selection is ready
                    //Draw options menu; Fight - Defend - Run
                    spriteBatch.Draw(tBarBackground, rBattleActionSelect, Color.White);
                    spriteBatch.DrawString(font, "Fight", vFightText, fightColor);
                    spriteBatch.DrawString(font, "Defend", vDefendText, defendColor);
                    spriteBatch.DrawString(font, "Run", vRunText, runColor);
                }
                #endregion
            }
            #endregion

            //draws any text animations
            DrawTextAnimations(spriteBatch, gameTime, font);

        }
        //end DrawPlayer in battle

        /// <summary>
        /// Draws any scrolling text animations above the players sprite
        /// </summary>
        /// <param name="spriteBatch">Renders the sprite to screen</param>
        /// <param name="gameTime">Game Timer</param>
        /// <param name="font">Font for drawing text to screen</param>
        public void DrawTextAnimations(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            #region Draw Information Text

            #region levelUp Text
            if (bLevelUp == true)   //if LevelUp is true, draw the LevelUp! text
                {
                    fLevelTextTimer += elapsed; //add elapsed time to the LevelTextTimer

                    //Draws the LevelUp! text
                    spriteBatch.DrawString(font, "Level Up!",
                        new Vector2(CurrentLocation.X - 35, (CurrentLocation.Y -  rSpriteRect.Height + 20) - (5 * fLevelTextTimer)),
                        Color.Gold);
                    
                    //if LevelTextTimer reaches its pre-determined cap, stop the timer
                    if (fLevelTextTimer >= fLevelTextTimerCap)
                    {
                        bLevelUp = false;       //sets bool to false ensuring the text wont be written
                        fLevelTextTimer = 0.0f; //stops the timer
                    }//end if
                }//end LevelUp text
                #endregion

            #region ExpAdded Text
            //goes through the ExpAdded array to determine which text to draw on screen
            for (int i = 0; i < 100; i++)
            {
                if (iExpAdded[i] != 0)              //if current field is not empty
                {
                    fExpTextTimer[i] += elapsed;    //update the current fields timer

                    //Draws the +EXP! text
                    spriteBatch.DrawString(font, "+" + iExpAdded[i] + " Exp",
                        new Vector2(CurrentLocation.X - 35, (CurrentLocation.Y - rSpriteRect.Height + 20) - (20 * fExpTextTimer[i])),
                        Color.Gold);

                    //if the current fields timer reaches its cap
                    if (fExpTextTimer[i] >= fExpTextTimerCap)
                    {
                        fExpTextTimer[i] = 0.0f;    //stop the timer
                        iExpAdded[i] = 0;           //clear the field
                    }//end if
                }//end current field
            }//end loop
            #endregion

            #region Damage Taken Text
            //goes through the ExpAdded array to determine which text to draw on screen
            for (int i = 0; i < 100; i++)
            {
                if (damageTaken[i] != 0)                //if the current field is not empty
                {
                    fDamageTextTimer[i] += elapsed;     //update the current fields timer

                    //draw the dmg taken text on screen
                    spriteBatch.DrawString(font, "-" + damageTaken[i],
                        new Vector2(CurrentLocation.X + 10, (CurrentLocation.Y - rSpriteRect.Height + 20) - (25 * fDamageTextTimer[i])),
                        Color.Red);
                }

                //if the current fields timer reaches the cap
                if (fDamageTextTimer[i] >= fDamageTextTimerCap)
                {
                    damageTaken[i] = 0;         //clear the field
                    fDamageTextTimer[i] = 0;    //stop the timer
                }
            }//end for loop

            #endregion

            #region Health Recovered Text
            //goes through the ExpAdded array to determine which text to draw on screen
            for (int i = 0; i < 100; i++)
            {
                if (iHealthRecovered[i] != 0)           //if the current field is not empty
                {
                    fHealthTextTimer[i] += elapsed;     //update the current fields timer

                    //draws the health recovered text on screen
                    spriteBatch.DrawString(font, "+" + iHealthRecovered[i],
                        new Vector2(CurrentLocation.X + 10, (CurrentLocation.Y - rSpriteRect.Height + 20) - (25 * fHealthTextTimer[i])),
                        Color.Green);
                }

                if (fHealthTextTimer[i] >= fHealthTextTimerCap) //if current fields timer reaches cap
                {
                    iHealthRecovered[i] = 0;                    //clear the current field
                    fHealthTextTimer[i] = 0;                    //reset the current fields timer
                }
            }//end for loop
            #endregion //empty

            #endregion

        }
        //end DrawPlayer(spriteBatch, gameTime)

        /// <summary>
        /// Starts the attack animation for the player
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        private void StartAttackAnimation(GameTime gameTime)
        {
            if (fAttackAnimationTimer == 0)     //if Attack timer is not started
            {
                fAttackAnimationTimer += 0.1f;  //sets the timer to != 0
            }//end if

        }//end StartAttackAnimation(gameTime)

        /// <summary>
        /// Updates the animation frames
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
        private void UpdateAnimation(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            #region Overworld Map Walking / Standing Animation
            if (fATBTimer == 0) //if this timer is ever = 0, the player is not in a battle
            {
                if (PreviousLocation == CurrentLocation)       //if player is not moving
                {                                     
                    switch (LastDirection)                  //Determine which direction the player was last facing
                    {                                       // and set the sprite accordingly
                        case "down":
                            CurrentSprite = rDownStanding;
                            break;
                        case "left":
                            CurrentSprite = rLeftStanding;
                            break;
                        case "up":
                            CurrentSprite = rUpStanding;
                            break;
                        case "right":
                            CurrentSprite = rLeftStanding;
                            break;
                    }//end swithc
                }

                if (CurrentLocation != vPrevLocation)       //if player is currently moving
                {
                    fWalkingTimer += elapsed;               //Update the walking timer

                    if (fWalkingTimer >= fWalkingTimerCap)  //Determine if the animation is ready for next frame
                    {
                        
                        
                        switch (LastDirection)              //Determine which direction the player was last facing
                        {                                   // and set the sprite accordingly
                            case "down":
                                if (CurrentSprite != rDownWalking1 && CurrentSprite != rDownWalking2)
                                    CurrentSprite = rDownWalking1;
                                else if (CurrentSprite == rDownWalking1)
                                    CurrentSprite = rDownWalking2;
                                else if (CurrentSprite == rDownWalking2)
                                    CurrentSprite = rDownWalking1;
                                break;

                            case "left":
                                if (CurrentSprite != rLeftWalking1 && CurrentSprite != rLeftWalking2)
                                    CurrentSprite = rLeftWalking1;
                                else if (CurrentSprite == rLeftWalking1)
                                    CurrentSprite = rLeftWalking2;
                                else if (CurrentSprite == rLeftWalking2)
                                    CurrentSprite = rLeftWalking1;
                                break;

                            case "up":
                                if (CurrentSprite != rUpWalking1 && CurrentSprite != rUpWalking2)
                                    CurrentSprite = rUpWalking1;
                                else if (CurrentSprite == rUpWalking1)
                                    CurrentSprite = rUpWalking2;
                                else if (CurrentSprite == rUpWalking2)
                                    CurrentSprite = rUpWalking1;
                                break;

                            case "right":
                                if (CurrentSprite != rLeftWalking1 && CurrentSprite != rLeftWalking2)
                                    CurrentSprite = rLeftWalking1;
                                else if (CurrentSprite == rLeftWalking1)
                                    CurrentSprite = rLeftWalking2;
                                else if (CurrentSprite == rLeftWalking2)
                                    CurrentSprite = rLeftWalking1;
                                break;
                        }//end swithc

                        fWalkingTimer = 0.0f;       //reset the walking timer
                    }
                }//end standing
            }//end worldanimation
            #endregion

            #region Battle Animations
            else if (fATBTimer > 0) //if this timer is ever > 0, player is in a battle
            {
                #region Battle Idle Animation
                if (fAttackAnimationTimer == 0)   //if attack animation timer is 0, attack has not begun
                {
                    fIdleBattleAnimationTimer += elapsed;   //advance the Idle Battle Animation Timer

                    if (fIdleBattleAnimationTimer >= fIdleBattleAnimationTimerCap)
                    {   //if current animation frame is not an idle animation frame, set it to be idle1
                        if (CurrentSprite != rLeftBattleIdle1 && CurrentSprite != rLeftBattleIdle2)
                            CurrentSprite = rLeftBattleIdle1;

                        else if (CurrentSprite == rLeftBattleIdle1)         //if current frame is idle frame 1, set it to idle frame 2
                            CurrentSprite = rLeftBattleIdle2;
                        else if (CurrentSprite == rLeftBattleIdle2)    //if current frame is idle frame 2, set it to idle frame 1
                            CurrentSprite = rLeftBattleIdle1;

                        fIdleBattleAnimationTimer = 0.1f;
                    }
                }//end idle animation
                #endregion

                #region Attack Animation
                else if (fAttackAnimationTimer > 0) //if attack animation is over 0, attack is in progress
                {
                    fAttackAnimationTimer += elapsed;

                    if (CurrentSprite == rLeftBattleIdle1 || CurrentSprite == rLeftBattleIdle2)     //if frame is an idle animation
                    {
                        CurrentSprite = rLeftAttack1;                                               //change to attack animation
                        fAttackAnimationTimer = 0.1f;
                    }

                    if (fAttackAnimationTimer >= fAttackAnimationTimerCap)  //if attack timer has reached cap, change frame
                    {

                        if (CurrentSprite == rLeftAttack1)                                    //change to next attack anim frame
                        {
                            CurrentSprite = rLeftAttack2;
                            fAttackAnimationTimer = 0.1f;
                        }
                        else if (CurrentSprite == rLeftAttack2)                                 //change to idle frame and stop
                        {                                                                       // attack timer
                            CurrentSprite = rLeftBattleIdle1;
                            fAttackAnimationTimer = 0.0f;
                        }
                    }//end change frame                    
                }//end attack animation
            }//end Attack Animation
                #endregion

            #endregion

        }//end UpdateAnimation
        #endregion


        public bool Run()
        {
            bool success = false;
            int rNum = RandomHelper.GetRandomInt(1, 1000);

            if (rNum > 800)
                success = true;

            fATBTimer = 0.01f;
            return success;
        }
    }//end Player
}
