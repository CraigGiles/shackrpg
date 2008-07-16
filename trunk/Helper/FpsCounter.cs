using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShackRPG
{
    /// <summary>
    /// Frames Per Second counter class used to display the
    /// current frames per second.
    /// </summary>
    public class FpsCounter
    {
        /// <summary>
        /// Gets or sets the Draw FPS command
        /// </summary>
        public static bool DrawFramesPerSecond
        {
            get { return _drawFps; }
            set { _drawFps = value; }
        }
        static bool _drawFps = true;


        /// <summary>
        /// Total frames between resets
        /// </summary>
        int _totalFrames;


        /// <summary>
        /// Current Frames Per Second
        /// </summary>
        int _fps;


        /// <summary>
        /// Elapsed Time since last call
        /// </summary>
        float _elapsedTime;


        /// <summary>
        /// Updates the Frames Per Second counter
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            _totalFrames++;

            if (_elapsedTime >= 1.0f)
            {
                _fps = _totalFrames;
                _totalFrames = 0;
                _elapsedTime = 0;
            }
        }


        /// <summary>
        /// Draws the current Frames Per Second to screen
        /// </summary>
        public void Draw()
        {
            if (DrawFramesPerSecond)
            {
                Globals.SpriteBatch.DrawString(
                    Globals.Font,
                    "FPS: " + _fps.ToString(),
                    new Vector2(5, 5),
                    Color.Yellow,
                    0f,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    0f);
            }
        }

    }//end FPSCounter
}
