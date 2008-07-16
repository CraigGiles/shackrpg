using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ShackRPG
{
    public class Input
    {
        
        float timer = 0;


        public void GetUserInput(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            keyboard.Update();
            gamepad.Update();

        }


        #region GamePad Input


        /// <summary>
        /// Gamepad input object
        /// </summary>
        GamePadInput gamepad = new GamePadInput();


        public bool IsButtonPressed(Buttons button)
        {
            return gamepad.IsButtonPressed(button);
        }


        public bool IsButtonHeld(Buttons button)
        {
            return gamepad.IsButtonHeld(button);
        }


        public bool IsLeftThumbstickHeld()
        {
            return gamepad.LeftThumbstickHeld();
        }

        
        public Vector2 LeftThumbstick()
        {
            return gamepad.LeftThumbstick * 3;
        }
        

        #endregion


        #region Keyboard


        /// <summary>
        /// Keyboard input object
        /// </summary>
        KeyboardInput keyboard = new KeyboardInput();


        /// <summary>
        /// Checks to see if a key was newly pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True: Key is pressed - False: Key is not pressed, or key is held</returns>
        public bool IsKeyPressed(Keys key)
        {
            return keyboard.IsKeyPressed(key);
        }


        /// <summary>
        /// Checks to see if a key is being held down
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True: Key is being held down - False: Key is not being held down, or was pressed</returns>
        public bool IsKeyHeld(Keys key)
        {
            return keyboard.IsKeyHeld(key);
        }

        /// <summary>
        /// Converts a key on the keyboard to a Char
        /// </summary>
        /// <param name="key">key to convert</param>
        /// <returns>Char</returns>
        public Char ConvertKeyToChar(Keys key)
        {
            bool shiftPressed = (keyboard.IsKeyHeld(Keys.LeftShift) ||
                                 keyboard.IsKeyHeld(Keys.RightShift));

            return keyboard.KeyToChar(key, shiftPressed);
        }


        public List<Keys> GetPressedKeys
        {
            get
            {
                if (timer >= .01f)
                {
                    timer = 0;
                    return keyboard.GetPressedKeys;
                }
                else return new List<Keys>();
            }
        }
        

        #endregion


    }//end class 
}//end namespace
