using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ShackRPG
{
    public static class KeyBindings
    {

        #region Keyboard Movement Keys


        public static Keys KeyboardMoveDown
        {
            get { return moveDown; }
            set { moveDown = value; }
        }
        static Keys moveDown = Keys.S;

        public static Keys KeyboardMoveLeft
        {
            get { return moveLeft; }
            set { moveLeft = value; }
        }
        static Keys moveLeft = Keys.A;

        public static Keys KeyboardMoveUp
        {
            get { return moveUp; }
            set { moveUp = value; }
        }
        static Keys moveUp = Keys.W;

        public static Keys KeyboardMoveRight
        {
            get { return moveRight; }
            set { moveRight = value; }
        }
        static Keys moveRight = Keys.D;


        #endregion


        #region Keyboard Arrow Keys


        public static Keys KeyboardArrowDown
        {
            get { return arrowDown; }
            set { arrowDown = value; }
        }
        static Keys arrowDown = Keys.Down;

        public static Keys KeyboardArrowLeft
        {
            get { return arrowLeft; }
            set { arrowLeft = value; }
        }
        static Keys arrowLeft = Keys.Left;

        public static Keys KeyboardArrowUp
        {
            get { return arrowUp; }
            set { arrowUp = value; }
        }
        static Keys arrowUp = Keys.Up;

        public static Keys KeyboardArrowRight
        {
            get { return arrowRight; }
            set { arrowRight = value; }
        }
        static Keys arrowRight = Keys.Right;


        #endregion


        #region Keyboard Button Keys

        //okay
        public static Keys KeyboardOkay
        {
            get { return keyboardOkay; }
            set { keyboardOkay = value; }
        }
        static Keys keyboardOkay = Keys.J;

        //cancel
        public static Keys KeyboardCancel
        {
            get { return keyboardCancel; }
            set { keyboardCancel = value; }
        }
        static Keys keyboardCancel = Keys.K;

        //inventory
        public static Keys KeyboardInventory
        {
            get { return keyboardInventory; }
            set { keyboardInventory = value; }
        }
        static Keys keyboardInventory = Keys.I;

        //back
        public static Keys KeyboardBack
        {
            get { return keyboardBack; }
            set { keyboardBack = value; }
        }
        static Keys keyboardBack = Keys.U;

        
        public static Keys KeyboardStart
        {
            get { return keyboardStart; }
            set { keyboardStart = value; }
        }
        static Keys keyboardStart = Keys.Enter;


        public static Keys KeyboardSelect
        {
            get { return keyboardSelect; }
            set { keyboardSelect = value; }
        }
        static Keys keyboardSelect = Keys.RightShift;

        #endregion


        #region Gamepad Button Keys

        /// <summary>
        /// Inventory Button - Default Y
        /// </summary>
        public static Buttons GamepadInventory
        {
            get { return gamepadInventory; }
            set { gamepadInventory = value; }
        }
        static Buttons gamepadInventory = Buttons.Y;

        /// <summary>
        /// Cancel Button - Default B
        /// </summary>
        public static Buttons GamepadCancel
        {
            get { return gamepadCancel; }
            set { gamepadCancel = value; }
        }
        static Buttons gamepadCancel = Buttons.B;

        /// <summary>
        /// Okay button - Default A
        /// </summary>
        public static Buttons GamepadOkay
        {
            get { return gamepadOkay; }
            set { gamepadOkay = value; }
        }
        static Buttons gamepadOkay = Buttons.A;

        /// <summary>
        /// Back Button - Default X
        /// </summary>
        public static Buttons GamepadBack
        {
            get { return gamepadBack; }
            set { gamepadBack = value; }
        }
        static Buttons gamepadBack = Buttons.X;

        /// <summary>
        /// Start Button - Cannot be changed
        /// </summary>
        public static Buttons GamepadStart
        {
            get { return gamepadStart; }
            set { gamepadStart = value; }
        }
        static Buttons gamepadStart = Buttons.Start;

        /// <summary>
        /// Select Button - Can not be changed
        /// </summary>
        public static Buttons GamepadSelect
        {
            get { return gamepadSelect; }
            set { gamepadSelect = value; }
        }
        static Buttons gamepadSelect = Buttons.Back;

        #endregion

    }
}
