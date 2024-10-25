using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace Engine
{
    public class Camera
    {

        public static Matrix Transform { get; private set; }

        public static void Follow(Vector2 target)
        {
            {
                //take current position of the target
                var position = Matrix.CreateTranslation(-target.X, -target.Y, 0);
                //take center of the screen
                var offset = Matrix.CreateTranslation(ExtendedGame.windowSize.X / 2, ExtendedGame.windowSize.Y / 2, 0);
                Transform = position * offset;
            }
        }
    }
}
