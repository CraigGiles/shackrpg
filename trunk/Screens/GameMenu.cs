using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public class GameMenu : GameScreen
    {

        public GameMenu()
        {
            this.Name = "GameMenu";
            this.BlocksDraw = true;
            this.BlocksUpdate = false;

            LoadContent();
        }

        public override void LoadContent()
        {
            logoGraphic = Globals.Content.Load<Texture2D>(@"Textures/MainMenu/GameLogo");
            logoBackdrop = Globals.Content.Load<Texture2D>(@"Textures/MainMenu/MainMenuInfoSpace");
            
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (Globals.Input.IsKeyPressed(KeyBindings.KeyboardStart) ||
                Globals.Input.IsButtonPressed(KeyBindings.GamepadStart))
            {
                ScreenManager.AddScreen(new GameMenu());
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            //draw game logo
            DrawGameLogo();

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


    }
}
