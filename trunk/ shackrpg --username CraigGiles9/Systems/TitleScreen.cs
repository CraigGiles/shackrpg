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


namespace TestProject
{
    /// <summary>
    /// Title Screen is a basic class designed to show the Title
    /// Screen of the ShackRPG - In Search of Remo, and check to 
    /// see if the "ENTER" button is pressed. If pressed, the
    /// current gamestate will be changed from "title" to "introduction"
    /// or "game"
    /// 
    /// Craig Giles
    /// Feb.2008
    /// </summary>
    class TitleScreen
    {
        /// <summary>
        /// Textures and Rectangles used to render the textures.
        /// </summary>
        Texture2D tBackground, tMenu;
        Rectangle
            MenuBackground = new Rectangle(0, 0, 800, 600),
            MenuTitle = new Rectangle(245, 100, 310, 70),
            MenuPressEnter = new Rectangle(312, 400, 175, 105);

        /// <summary>
        /// Constructor used to initialize textures
        /// </summary>
        /// <param name="Content">Content Manager from the main program</param>
        public TitleScreen(ContentManager Content)
        {
            tBackground = Content.Load<Texture2D>(@"TitleScreen/Graphics/MenuBackground");
            tMenu = Content.Load<Texture2D>(@"TitleScreen/Graphics/MenuTitleScreen");
        }//end Constructor

        /// <summary>
        /// Checks to see if player has pressed ENTER, and if so updates the GameState
        /// to "Scene" ... This may change later to Update the gamestate to an "Intro"
        /// and once the Intro has finished, having Intro update the gamestate to Scene or
        /// NewGame.
        /// </summary>
        public void Update()
        {
            if (InputHelper.Enter)
            { GameStateManager.ChangeGameState(GameStateManager.GameState.Scene); }
        }//end Update()

        /// <summary>
        /// Draws the title screen onto the main screen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from Main Program</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            // Draws the Background
            spriteBatch.Draw(tBackground, MenuBackground, Color.DarkCyan);

            // Draws the RPG title
            spriteBatch.Draw(tMenu, MenuTitle,
                new Rectangle(0, 0, 310, 70),
                Color.RoyalBlue);

            // Draws "PRESS ENTER" text
            spriteBatch.Draw(tMenu, MenuPressEnter,
                new Rectangle(0, 70, 175, 105),
                Color.Salmon);

            spriteBatch.End();
        }//end ShowMenu

    }//End Class
}//end NameSpace
