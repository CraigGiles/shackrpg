using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public class StartMenu : GameScreen
    {

        #region Constructor(s)

        public StartMenu()
        {
            this.Name = "StartMenu";
            this.BlocksDraw = true;
            this.BlocksUpdate = true;

            LoadContent();
        }

        public override void LoadContent()
        {
            logoGraphic = Globals.Content.Load<Texture2D>(@"Textures/MainMenu/GameLogo");
            logoBackdrop = Globals.Content.Load<Texture2D>(@"Textures/MainMenu/MainMenuInfoSpace");

            Globals.AudioManager.PlayMusic("RisingEmpire");

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        #endregion


        public override void Update(GameTime gameTime)
        {
            if (Globals.Input.IsKeyPressed(KeyBindings.KeyboardStart) ||
                Globals.Input.IsButtonPressed(KeyBindings.GamepadStart))
            {
                //stop menu music
                Globals.AudioManager.StopMusic();

                //add next game screen to the stack
                ScreenManager.AddScreen(new GameSelect());
            }

            UpdateTimers(gameTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// Timer used for Press Start text
        /// </summary>
        float introTimer = 0.0f;
        bool drawPressStart = false;

        private void UpdateTimers(GameTime gameTime)
        {
            introTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (introTimer >= 43.5f)
                drawPressStart = true;
        }


        public override void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            //draw game logo
            DrawGameLogo();
            DrawText();

            base.Draw(gameTime);

            Globals.SpriteBatch.End();
        }


        #region Draw Game Logo

        Texture2D logoGraphic;
        Point logo = new Point(464, 243);

        Texture2D logoBackdrop;
        Point logoBackdropPoint = new Point(494, 486);

        private void DrawGameLogo()
        {

            //main game logo Backdrop
            Globals.SpriteBatch.Draw(logoBackdrop,
                new Vector2(
                (Globals.Game.GraphicsDevice.Viewport.Width / 2) - (logoBackdropPoint.X / 2),
                (Globals.Game.GraphicsDevice.Viewport.Height / 2) - (logoBackdropPoint.Y / 2)),
                Color.White);

            //main game logo
            Globals.SpriteBatch.Draw(logoGraphic,
                new Vector2(
                (Globals.Game.GraphicsDevice.Viewport.Width / 2) - (logo.X / 2),
                (Globals.Game.GraphicsDevice.Viewport.Height / 2) - (logo.Y / 2)),
                Color.White);

        }

        #endregion


        #region Draw Text


        private void DrawText()
        {
            if (drawPressStart)
            {
                Globals.SpriteBatch.DrawString(Globals.Font,
                    "Press Start",
                    new Vector2(270, 500),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3f, 
                    SpriteEffects.None, 
                    0f);
            }
        }


        #endregion


    }
}
