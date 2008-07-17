using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShackRPG
{
    public class Camera
    {
        public float Speed = 5;
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// Matrix used for drawing in-game WorldItems
        /// </summary>
        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Position, 0f));
            }
        }

    }//end Camera Class
}
