using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    public class GameplayScreen : GameScreen
    {

        #region Constructor(s)


        public GameplayScreen()
        {
            this.Name = "GameplayScreen";
            this.BlocksUpdate = true;
            this.BlocksDraw = true;
        }


        public override void LoadContent()
        {
            Globals.TileEngine.LoadMap("Map001");
            base.LoadContent();
        }


        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        #endregion


        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, Globals.Camera.TransformMatrix);

            Globals.SpriteBatch.DrawString(Globals.Font,
                "This is where main \ngame logic will go.",
                new Vector2(100, 100),
                Color.White);

            //draw base layer
            Globals.TileEngine.DrawLayers(true, false, false);

            //draw game objects, player, npcs, etc

            //draw fringe and object layers
            Globals.TileEngine.DrawLayers(false, true, true);

            base.Draw(gameTime);
            Globals.SpriteBatch.End();
        }

    }
}
