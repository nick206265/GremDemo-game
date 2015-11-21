using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GremDemo
{
    class Background : Static
    {
        public Background(GraphicsDeviceManager graphics, Texture2D sprite)
            :base(sprite)
        {
            drawRect.X = 0;
            drawRect.Y = 0;

            drawRect.Width = graphics.PreferredBackBufferWidth;
            drawRect.Height = graphics.PreferredBackBufferHeight;
        }
    }
}
