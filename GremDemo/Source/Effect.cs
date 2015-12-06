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
    // Абстрактный класс для эффектов
    abstract class Effect : Entity
    {
        // Виртуальный метод, реализующий логику обновления состояния объекта
        public virtual void Update(GameTime gameTime)
        {

        }

        // Виртуальный метод, отвечающий за отрисовку объекта
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
