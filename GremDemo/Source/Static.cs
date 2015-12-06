/************************************************************************
* Для запуска и сборки данной программы необходимо установить:          *   
*  1) MonoGame 3.4                                                      *
*  2) Microsoft .Net Framework 4                                        * 
* Решение (solution) для Visual Studio 2015 Community                   *
*************************************************************************/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GremDemo
{
    /// <summary>
    /// Абстрактный класс для статических игровых объектов
    /// </summary>
    abstract class Static : Entity
    {
        
        #region Fields
            
        // Drawing support
        protected Texture2D sprite;

        #endregion


        #region Properties

        #endregion


        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite">Спрайт (текстура) для отрисовки</param>
        public Static(Texture2D sprite)
        {
            this.sprite = sprite;

        }

        #endregion


        #region Public methods
        // Метод, реализующий логику обновления состояния объекта
        public void Update(GameTime gameTime)
        {
            // m b yes m b no
            // prallax
            
        }

        // Метод, отвечающий за отрисовку объекта
        public void Draw(SpriteBatch spriteBatch)
        {
            // вызов метода отрисовки
            spriteBatch.Draw(sprite, drawRect, Color.White);
            
        }

        #endregion#

        #region Private methods
      
        #endregion
    }
}
