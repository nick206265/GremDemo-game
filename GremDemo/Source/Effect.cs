/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : Effect.cs                                              *
* Programmers  : Колесников А.П. Кириллин С.Д.                          *
* Created      : 17/11/15                                               *
* Last Revision: 30/11/15                                               *
* Comments     : MonoGame game project using DirectX                    *
*                                                                       *
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
/*    end of file Effect.cs */
