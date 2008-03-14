using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using DummyProject.HelperClasses;
using DummyProject.Systems;
using BattleEngineV1;

namespace DummyProject.GameScreens
{
    class BattleScene : IGameScreen
    {
        Battle battleEngine;
        ContentManager content;
        SpriteBatch spriteBatch;
        BaseGame game; 
        SpriteFont font;
        Player player;
        Mantis mantis;

        public BattleScene(ContentManager sContent, SpriteBatch sSpriteBatch, BaseGame sGame, SpriteFont sFont, GameTime gameTime, Player sPlayer)
        {
            content = sContent;
            spriteBatch = sSpriteBatch;
            game = sGame;
            font = sFont;
            player = sPlayer;

            Initialize(gameTime);
        }
        public BattleScene()
        {
        }

        private void Initialize(GameTime gameTime)
        {
            battleEngine = new Battle(content, spriteBatch, font);
            mantis = new Mantis(spriteBatch, font, content);

            player.StartBattle(gameTime);
            mantis.StartIdleAnimation(gameTime);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            mantis.Update(gameTime, player);
            #region Debug
            if (InputHelper.C)
            {
                player.EndBattle(gameTime);
                game.RemoveCurrentGameScreen();
            }


            if (InputHelper.Left)
            {
                int Damage = mantis.Attack(gameTime);
                player.TakePhysicalDamage(Damage);
            }
            if (InputHelper.Space)
            {
                int Damage = player.Attack(gameTime);
                mantis.TakePhysicalDamage(Damage,player);
     
            }
            #endregion
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            battleEngine.DrawBattle(gameTime);  //draws the battle scene minus players and enemies

            player.DrawPlayer(spriteBatch, gameTime, font, battleEngine);
            mantis.Draw(gameTime);

            //foreach (Enemy e in Enemy[])
            //  e.DrawEnemy(spriteBatch,gameTime,font);

            spriteBatch.End();
        }

    }
}
