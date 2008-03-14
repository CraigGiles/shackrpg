using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace DummyProject.HelperClasses
{
    /// <summary>
    /// Input helper is a helper class designed to get the current
    /// key being pressed by the user, and set a bool value associated
    /// with that key to True if the key is pressed, and False if the key
    /// is not being pressed.
    /// 
    /// Craig Giles
    /// Feb.2008
    /// </summary>
    class InputHelper
    {
        static public KeyboardState curKeyboardState, prevKeyboardState;

        /// <summary>
        /// public Bool values to indicate weather or not a specific 
        /// key is being pressed
        /// </summary>

        static public bool FacingLeft = true;
        static public bool FacingRight = false;

        static public bool Quit = false;
        static public bool Pause = false;

        static public bool Space = false;
        static public bool Enter = false;
        static public bool Esc = false;

        static public bool Up = false;
        static public bool Down = false;
        static public bool Left = false;
        static public bool Right = false;

        static public bool C = false;
        static public bool B = false;
                

        /// <summary>
        /// Gets the keys being pressed by the player and sets the
        /// appropriate bool value for that key to "true" if pressed
        /// and "false" if not.
        /// </summary>
        /// <param name="player">Player object</param>
        static public void GetPlayerInput()
        {
            curKeyboardState = Keyboard.GetState();

            if (curKeyboardState.IsKeyDown(Keys.Down))
                Down = true; 
            else
                Down = false; 

            if (curKeyboardState.IsKeyDown(Keys.Up))
                Up = true; 
            else
                Up = false;

            if (curKeyboardState.IsKeyDown(Keys.Left))
            { Left = true; FacingLeft = true; FacingRight = false; }
            else
            { Left = false; }

            if (curKeyboardState.IsKeyDown(Keys.Right))
            { Right = true; FacingRight = true; FacingLeft = false; }
            else
            { Right = false; }

            //Ensure the player can only move one direction at a time, Up, Down, Left, or Right.
            if (curKeyboardState.IsKeyDown(Keys.Down) && curKeyboardState.IsKeyDown(Keys.Right))
            { Down = true; Right = false; }
            if (curKeyboardState.IsKeyDown(Keys.Down) && curKeyboardState.IsKeyDown(Keys.Left))
            { Down = true; Left = false; }
            if (curKeyboardState.IsKeyDown(Keys.Up) && curKeyboardState.IsKeyDown(Keys.Right))
            { Up = true; Right = false; }
            if (curKeyboardState.IsKeyDown(Keys.Up) && curKeyboardState.IsKeyDown(Keys.Left))
            { Up = true; Left = false; }

            if (curKeyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space))
                Space = true;
            else
                Space = false;

            if (curKeyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter))
             Enter = true; 
            else
             Enter = false; 

            if (curKeyboardState.IsKeyDown(Keys.Escape) && prevKeyboardState.IsKeyUp(Keys.Escape))
                Esc = true;
            else
                Esc = false;

            if (curKeyboardState.IsKeyDown(Keys.C) && prevKeyboardState.IsKeyUp(Keys.C))
                C = true;
            else
                C = false;

            if (curKeyboardState.IsKeyDown(Keys.B) && prevKeyboardState.IsKeyUp(Keys.B))
                B = true;
            else
                B = false;

            if (curKeyboardState.IsKeyDown(Keys.F10) && prevKeyboardState.IsKeyUp(Keys.F10))
                Quit = true;
            else 
                Quit = false;

            if (curKeyboardState.IsKeyDown(Keys.P) && prevKeyboardState.IsKeyUp(Keys.P))
                Pause = true;
            else
                Pause = false;

            prevKeyboardState = curKeyboardState;
        }//end GetPlayerInput

        public static void ResetAllInput()
        {
            Quit = false;
            Pause = false;

            Enter = false;
            Esc = false;

            Up = false;
            Down = false;
            Left = false;
            Right = false;

            C = false;
            B = false;
        }
    }//end class
}//end namespace
