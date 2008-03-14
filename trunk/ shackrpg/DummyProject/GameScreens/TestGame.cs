using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using DummyProject.HelperClasses;
using DummyProject.Systems;

namespace DummyProject.GameScreens
{
    /// <summary>
    /// TODO:
    /// 
    /// As of right now, "foreach (Enemy enemy in enemyArray) is messing me up
    /// Its creating 3 enemies right on top of eachother, and right now I do not
    /// have the methods set up to create them in different locations of the screen.
    /// I will have to work on this. The udnerlying structure is in place however
    /// and working nice
    /// </summary>
    class TestGame : IGameScreen
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        BaseGame game;
        SpriteFont font;

        Player player;
        Mantis[] enemyArray = new Mantis[2];


        public TestGame(ContentManager sContent, SpriteBatch sSpriteBatch, BaseGame sGame, SpriteFont sFont)
        {
            Content = sContent;
            spriteBatch = sSpriteBatch;
            game = sGame;
            font = sFont;

            InitializeGame();
        }

        private void InitializeGame()
        {

            player = new Player(Content);
         

        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            player.Update(gameTime);

            #region Debug Commands
            if (InputHelper.Enter)
            {
                game.AddNewGameScreen(new BattleScene(Content, spriteBatch, game, font, gameTime, player));
            }
            if (InputHelper.C)
            {
                player.EndBattle(gameTime);
            }
            #endregion

            if (InputHelper.Space)
            {
                player.StartBattle(gameTime);
     
            }

            if (InputHelper.Left || InputHelper.Right || InputHelper.Up || InputHelper.Down)
                player.Move();

            //Check to see if any enemies has died
            //foreach (Enemy enemy in enemyArray)
            //{
            //    if (enemy.Health <= 0)
            //    {
            //        enemy.HasDied(player);
            //        enemy.Dispose();
            //    }
            //}

        }//end Update

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            player.DrawPlayer(spriteBatch, gameTime,font);
            spriteBatch.End();
        }

    }
}
