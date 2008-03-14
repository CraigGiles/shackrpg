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

namespace BattleEngineV1
{
    public class Battle
    {
        #region Variables
 
        #region System
        ContentManager Content;
        SpriteBatch spriteBatch;
        SpriteFont font;
        #endregion

        #region Graphics
        Texture2D[] Background = new Texture2D[5];
        Texture2D tBattleBackground;
        Texture2D tRunAway, tFight, tDefend, tMagic, tHealth, tMana;
        Texture2D tBottomBackgrounds, tInfoBarBackground, tPlayerSprite, tEnemySprite;
        #endregion

        #region Rectangles
        /// <summary>
        /// Player Commands section of the battle screen. These include
        /// Background: background texture for player commands
        /// Fight, Magic, Defend, Run: Font textures for these commands.
        /// </summary>
        Rectangle
            rPlayerCommandsBackground = new Rectangle(0, 400, 400, 200),
            rCommandFight = new Rectangle(140, 425, 150, 50),
            rCommandMagic = new Rectangle(225, 475, 150, 50),
            rCommandDefend = new Rectangle(25, 475, 150, 50),
            rCommandRun = new Rectangle(145, 525, 150, 50);

        /// <summary>
        /// Player information section of the battle scene. These include
        /// Background: background texture for player information section
        /// Health, Mana: Font textures displaying "Health:" "Mana:"
        /// Value Rectangles: Font displaying numerical values of Health / Mana
        /// </summary>
        Rectangle
            rPlayerInfoBackground = new Rectangle(400, 400, 400, 200),
            rHealth = new Rectangle(425, 450, 100, 50),
            rHealthValue = new Rectangle(550, 450, 200, 50),
            rMana = new Rectangle(425, 520, 100, 50),
            rManaValue = new Rectangle(550, 520, 200, 50);

        /// <summary>
        /// Main background of the battle scene, Information bar,
        /// as well as monster and player sprite boxes
        /// </summary>
        Rectangle
            rBattleScene = new Rectangle(0, 0, 800, 400),
            rPlayerSprite = new Rectangle(675, 240, 75, 75),
            rInfoBar = new Rectangle(0, 0, 800, 50);
        #endregion

        #region Properties
        public Rectangle PlayerSprite1
        {
            get { return rPlayerSprite; }
            set { rPlayerSprite = value; }
        }

        //Eventually there will be 4 player sprite rectangles
        //because there will be a 4 person party

        //public Rectangle PlayerSprite1
        //{
        //    get { return rPlayerSprite; }
        //    set { rPlayerSprite = value; }
        //}
        //public Rectangle PlayerSprite1
        //{
        //    get { return rPlayerSprite; }
        //    set { rPlayerSprite = value; }
        //}
        //public Rectangle PlayerSprite1
        //{
        //    get { return rPlayerSprite; }
        //    set { rPlayerSprite = value; }
        //}

        #endregion

        #endregion

        #region Constructors and Initialize()
        public Battle(ContentManager sContent, 
                            SpriteBatch sSpriteBatch, 
                            SpriteFont sFont)
        {
            Content = sContent;
            spriteBatch = sSpriteBatch;
            font = sFont;

            Initialize();
        }

        private void Initialize()
        {

            Background[0] = Content.Load<Texture2D>(@"BattleBackground1");
            Background[1] = Content.Load<Texture2D>(@"BattleBackground2");
            Background[2] = Content.Load<Texture2D>(@"BattleBackground3");
            Background[3] = Content.Load<Texture2D>(@"BattleBackground4");
            Background[4] = Content.Load<Texture2D>(@"BattleBackground5");

            tRunAway = Content.Load<Texture2D>(@"run");
            tDefend = Content.Load<Texture2D>(@"defend");
            tMagic = Content.Load<Texture2D>(@"magic");
            tFight = Content.Load<Texture2D>(@"fight");
            tHealth = Content.Load<Texture2D>(@"health");
            tMana = Content.Load<Texture2D>(@"mana");
            tPlayerSprite = Content.Load<Texture2D>(@"PlayerSprite");
            tBottomBackgrounds = Content.Load<Texture2D>(@"BottomBackground");
            tInfoBarBackground = Content.Load<Texture2D>(@"InfoBar");

            int rNum = RandomHelper.GetRandomInt(1, 5);
            tBattleBackground = Background[rNum];
                
            //initialize any camera systems
        }

        #endregion

        #region Methods
        #endregion

        #region Draw Methods
        public void DrawBattle(GameTime gameTime)
        {
            //Draws the battle layout, menus, etc. Everything but things associated with the players and enemies

            // Draws the background
            spriteBatch.Draw(tBattleBackground, rBattleScene, Color.White);

            // Draws player info background (bottom right corner)
            spriteBatch.Draw(tBottomBackgrounds, rPlayerInfoBackground, Color.White);

            // Draws player command background (bottom left corner)
            spriteBatch.Draw(tBottomBackgrounds, rPlayerCommandsBackground, Color.White);

        }
        #endregion

    }//end RandomBattle

}//end BattleEngineV1
