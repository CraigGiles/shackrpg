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
            //Check player ending conditions
            if (player.Health <= 0)
                player.HasDied(gameTime, game);

            /* This will be changed to a foreach (Enemy enemy in EnemyArray[])
             * then check to see if any enemies need to be updated and add a counter
             * to the end of the statement. If that counter = 0 (possibly a list, and if
             * the enemy is dead take it out of the list) then the battle has been won
             * and the battle screen will be removed */

            if (!mantis.IsDead)                     //if mantis is not dead
                UpdateBattleLogic(gameTime);        //update all logic associated with battle

            else if (mantis.IsDead)                 //if mantis IS dead
            {
                //launch victory screen / anim      //TODO: Launch the victory animation
                player.EndBattle(gameTime);         //end the battle logic for player
                game.RemoveCurrentGameScreen();     //remove the current game screen
            }//end EndBattle

            #region Debug
            if (InputHelper.C)
            {
                player.EndBattle(gameTime);
                game.RemoveCurrentGameScreen();
            }
            #endregion
        }

        private void UpdateBattleLogic(GameTime gameTime)
        {
            player.UpdateBattle(gameTime);      //update the player
            mantis.Update(gameTime, player);    //update the mantis

            //If the players ATB timer has reached its cap, and the enter key is pressed
            if (InputHelper.Enter && player.ATBTimer >= player.ATBTimerCap)
            {
                switch (player.CurrentBattleCommand)                //Get the current battle command selected
                {
                    case Player.BattleCommands.Fight:               //if CurrentBattleCommand is Fight
                        int Damage = player.Attack(gameTime);           //Have the player attack
                        mantis.TakePhysicalDamage(Damage, player);      //Damage the Mantis (changed later)
                        break;

                    case Player.BattleCommands.Defend:
                        player.Block();
                        break;

                    case Player.BattleCommands.Run:
                        bool success = player.Run();

                        if (success)
                        {
                            player.EndBattle(gameTime);
                            game.RemoveCurrentGameScreen();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Renders the battle to screen
        /// </summary>
        /// <param name="gameTime">Game Timer</param>
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
