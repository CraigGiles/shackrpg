using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DummyProject.HelperClasses;

namespace DummyProject.GameScreens
{
    class MainMenu : IGameScreen
    { 
        ContentManager Content;
        SpriteBatch spriteBatch;
        BaseGame game;
        SpriteFont font;

        public MainMenu(ContentManager sContent, SpriteBatch sSpriteBatch, BaseGame sGame, SpriteFont sFont)
        {
            Content = sContent;
            spriteBatch = sSpriteBatch;
            game = sGame;
            font = sFont;
        }

        public void Update(GameTime gameTime)
        {
            /* TODO:
             * eventually have this screen animating, but for now I
             * just want to get something up and running so I'll check
             * to see if the user has pressed "Enter" and if so, change
             * the gamescreen to the game. */

            if (InputHelper.Enter)
            {
                game.AddNewGameScreen(new TestGame(Content, spriteBatch, game, font));
            }
        }//end Update(gameTime)

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.DrawString(font, "Welcome to the most worthless\n        title screen ever!",
                new Vector2(115, 70), Color.White);

            spriteBatch.DrawString(font, "Press Enter to start TestGame",
                new Vector2(125, 250), Color.White);

            spriteBatch.End();
        }


    }//end Class
}//end namespace
