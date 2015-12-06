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
    /// Класс объекта фона
    /// </summary>
    class Background : Static
    {


        // Конструктор; sprite - текстура для отрисовки на экран
        public Background(GraphicsDeviceManager graphics, Texture2D sprite)
            :base(sprite)
        {
            // верхний левый угол прямоугольника отрисовки совмещаем с левым верхним углом игрового окна
            drawRect.X = 0;
            drawRect.Y = 0;

            // растягиваем текстуру на все пространство игрового окна
            drawRect.Width = graphics.PreferredBackBufferWidth;
            drawRect.Height = graphics.PreferredBackBufferHeight;
        }


    }
}
