using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GremDemo
{
    class StaticObj
    {
        
        #region Fields

    
        // Drawing support
        Texture2D sprite;
    
        // draw rectangle
        Rectangle drawRect;


        #endregion


        #region Properties

        #endregion


        #region Constructors

        public StaticObj(GraphicsDeviceManager graphics, Texture2D sprite)
        {
            this.sprite = sprite;

            drawRect.X = 0;
            drawRect.Y = 0;

            drawRect.Width = graphics.PreferredBackBufferWidth;
            drawRect.Height = graphics.PreferredBackBufferHeight;


        }

        #endregion


        #region Public methods




        public void Update(GameTime gameTime)
        {
            // m b yes m b no
            // prallax
            
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(sprite, drawRect, Color.White);
            
        }

        #endregion#

        #region Private methods
      
        #endregion
    }
}
