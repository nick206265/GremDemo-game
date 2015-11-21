using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GremDemo
{
    abstract class Static : Entity
    {
        
        #region Fields

    
        // Drawing support
        protected Texture2D sprite;
    
        // draw rectangle
       // protected Rectangle drawRect;


        #endregion


        #region Properties

        #endregion


        #region Constructors

        public Static(Texture2D sprite)
        {
            this.sprite = sprite;

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
