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
    // Абстрактный класс для эффектов снаряда
    abstract class Shell : Effect
    {

        // Определяем максимальную скорость снаряда
        protected const float MAX_SPEED = 5f;

        // Информация о направлении вектора скорости на плоскости
        protected Vector2 velocity = new Vector2(0, 0);

        // Спрайт (текстура) для отрисовки эффекта
        protected Texture2D sprite;
        
    }
}
