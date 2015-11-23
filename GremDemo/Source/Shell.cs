using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GremDemo
{
    abstract class Shell : Effect
    {

        protected const float MAX_SPEED = 5f;

        protected Vector2 velocity = new Vector2(0, 0);

        protected Texture2D sprite;

       

    }
}
