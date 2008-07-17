using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public class GameSelect : GameScreen
    {
        public GameSelect()
        {
            this.Name = "GameSelect";
            this.BlocksUpdate = true;
            this.BlocksDraw = true;
        }

        public override void LoadContent()
        {
            Globals.AudioManager.PlayMusic("GameSelect");

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
                //stop game select music
                Globals.AudioManager.StopMusic();

                //remove this screen from the stack
                ScreenManager.RemoveScreen(this);

                //add next game screen to the stack
                ScreenManager.AddScreen(new GameplayScreen());
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            Globals.SpriteBatch.DrawString(Globals.Font,
                "This is where you select \nyour game. Press start \nto begin testing",
                new Vector2(120, 150),
                Color.White,
                0f,
                Vector2.Zero,
                3f,
                SpriteEffects.None,
                0f);

            base.Draw(gameTime);

            Globals.SpriteBatch.End();
        }
    }
}
