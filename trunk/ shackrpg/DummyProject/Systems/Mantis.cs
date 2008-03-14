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
    class Mantis : BaseEnemy
    {
        #region Variables

        #region System Variables
        SpriteBatch spriteBatch;
        GameTime gameTime;
        SpriteFont font;

        Player player;
        #endregion

        #region Character Variables
        #endregion

        #region Animation Variables
        /// <summary>
        /// Enemy spritesheet holding all frames of animation
        /// </summary>
        Texture2D tSpriteSheet;
        
        /// <summary>
        /// Source Rectangles for Idle Animation
        /// </summary>
        Rectangle
            rBattleIdle1 = new Rectangle((0 * 85), 0, 85, 65),
            rBattleIdle2 = new Rectangle((1 * 85), 0, 85, 65),
            rBattleIdle3 = new Rectangle((2 * 85), 0, 85, 65);

        /// <summary>
        /// Source Rectangles for Attack Animation
        /// </summary>
        Rectangle
            rBattleAttack1 = new Rectangle(250, (0 * 70), 75, 65),
            rBattleAttack2 = new Rectangle(0, (1 * 70), 75, 65),
            rBattleAttack3 = new Rectangle(75, (1 * 70), 75, 65),
            rBattleAttack4 = new Rectangle(150, (1 * 73), 75, 65),
            rBattleAttack5 = new Rectangle(230, (1 * 73), 80, 65),
            rBattleAttack6 = new Rectangle(0, (2 * 70), 70, 65);

        #endregion

        #endregion

        #region Constructors and Initialization methods
        public Mantis(SpriteBatch sSpriteBatch, SpriteFont sFont, ContentManager content)
        {
            spriteBatch = sSpriteBatch;
            font = sFont;

            LoadTextures(content);
            SetStartingStats();
            SetTimerCaps();

            Location = new Vector2(200, 165);
        }

        public Mantis(GameTime gameTime)
        {
        }

        private void LoadTextures(ContentManager content)
        {
             tSpriteSheet = content.Load<Texture2D>(@"Monsters/Mantisant");
             CurrentSprite = rBattleIdle1;
        }

        public override void SetStartingStats()
        {

            Name = "Mantis";            //Name of enemy
            Title = "Normal Enemy";     //Title of enemy
            HealthMax = 300;            //Max Health
            BaseStrength = 20;          //Strength not including buffs
            BaseAttackPower = 80;       //Attack Power not including buffs
            BaseArmor = 20;             //Armor not including buffs 
            Gold = 100;                 //Gold Given to player after killed
            Level = 1;                  //Level
            Speed = 3.2f;               //Sets the speed of the monster

            Health = HealthMax;
            Strength = BaseStrength;
            AttackPower = BaseAttackPower;
            Armor = BaseArmor;

            AttackDamageMod = 5;        //Sets the number used in the Attacking Damage modifier
            DefendDamageMod = 5;        //Sets the number used in the Defending Damage modifier
        }

        public override void SetTimerCaps()
        {
            TimerAttackAnimationCap = 0.14f;
            TimerIdleAnimationCap = 0.3f;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime, Player player)
        {
            UpdateAnimations(gameTime);
            //update timers
        }

        public override void Move(GameTime gameTime, Player player)
        {
        }

        public override int Attack(GameTime gameTime)
        {
            int dmg = 0;                    //damage done by the attack

            StartAttackAnimation(gameTime);

            

                //Random Modifier to attack power. This adds a slight 
                //random element to the attack
                //currently iDamageMod = 5
            int iRandomMod = RandomHelper.GetRandomInt(Level, Level * AttackDamageMod);
            dmg = AttackPower + iRandomMod;

            return dmg;
        }

        public override void Block()
        {
        }

        public override void TakePhysicalDamage(int damage, Player player)
        {
            //Random Modifier to Defense. This adds a slight 
            //random element to the damage received
            //currently iDamageMod = 5
            int iBlockedDamage = RandomHelper.GetRandomInt(Level, Level * DefendDamageMod);
            int iDamageDone = damage - (Armor + iBlockedDamage);  //Determines final Damage Done to player

            ModifyHealth(iDamageDone, player);
        }

        public override void TakeMagicalDamage(int damage, Player player)
        {
            //Random Modifier to Defense. This adds a slight 
            //random element to the damage received
            //currently iDamageMod = 5
            int iBlockedDamage = RandomHelper.GetRandomInt(Level, Level * DefendDamageMod);
            int iDamageDone = damage - (Armor + iBlockedDamage);  //Determines final Damage Done to player

            ModifyHealth(iDamageDone, player);
        }
        #endregion

        #region Draw methods
        public override void Draw(GameTime gameTime)
        {
            //todo this draw method will change 
            spriteBatch.Draw(tSpriteSheet, 
                new Rectangle(200,165,185,170),
            this.CurrentSprite, Color.White);

            DrawScrollingText(spriteBatch, gameTime, font);
        }


        #endregion

        #region Animations
        public override void StartIdleAnimation(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CurrentSprite = rBattleIdle1;
            TimerAttackAnimation = 0.0f;
        }

        public override void StartAttackAnimation(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TimerIdleAnimation = 0.0f;          //sets the idle animation to 0
            TimerAttackAnimation += elapsed;    //starts the Attack Animation timer
        }

        public override void UpdateAnimations(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (TimerAttackAnimation == 0)      //if attack anim timer is 0, the enemy is idle
            {
                TimerIdleAnimation += elapsed;

                if (TimerIdleAnimation >= TimerIdleAnimationCap)
                {
                    if (CurrentSprite != rBattleIdle1 && CurrentSprite != rBattleIdle2 && CurrentSprite != rBattleIdle3)
                        CurrentSprite = rBattleIdle1;
                    else if (CurrentSprite == rBattleIdle1)
                        CurrentSprite = rBattleIdle2;
                    else if (CurrentSprite == rBattleIdle2)
                        CurrentSprite = rBattleIdle3;
                    else if (CurrentSprite == rBattleIdle3)
                        CurrentSprite = rBattleIdle1;

                    TimerIdleAnimation = 0.0f;
                }
            }

            else if (TimerAttackAnimation != 0)     //if attack anim timer isn't 0, attack is in progress
            {
                TimerAttackAnimation += elapsed;

                if (CurrentSprite != rBattleAttack1 &&
                    CurrentSprite != rBattleAttack2 &&
                    CurrentSprite != rBattleAttack3 &&
                    CurrentSprite != rBattleAttack4 &&
                    CurrentSprite != rBattleAttack5 &&
                    CurrentSprite != rBattleAttack6)
                {
                    CurrentSprite = rBattleAttack1;
                }

                if (TimerAttackAnimation >= TimerAttackAnimationCap)
                {


                    if (CurrentSprite == rBattleAttack1)
                    {
                        CurrentSprite = rBattleAttack2;
                        TimerAttackAnimation = 0.01f;
                    }
                    else if (CurrentSprite == rBattleAttack2)
                    {
                        CurrentSprite = rBattleAttack3;
                        TimerAttackAnimation = 0.01f;
                    }
                    else if (CurrentSprite == rBattleAttack3)
                    {
                        CurrentSprite = rBattleAttack4;
                        TimerAttackAnimation = 0.01f;
                    }
                    else if (CurrentSprite == rBattleAttack4)
                    {
                        CurrentSprite = rBattleAttack5;
                        TimerAttackAnimation = 0.01f;
                    }
                    else if (CurrentSprite == rBattleAttack5)
                    {
                        CurrentSprite = rBattleAttack6;
                        TimerAttackAnimation = 0.01f;
                    }
                    else if (CurrentSprite == rBattleAttack6)
                    {
                        CurrentSprite = rBattleIdle1;
                        TimerAttackAnimation = 0.0f;
                    }                    
                }
            }//end attack animation
        }
        #endregion



    }
}
