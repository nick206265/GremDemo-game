/************************************************************************
* Для запуска и сборки данной программы необходимо установить:          *   
*  1) MonoGame 3.4                                                      *
*  2) Microsoft .Net Framework 4                                        * 
* Решение (solution) для Visual Studio 2015 Community                   *
*************************************************************************/

using Microsoft.Xna.Framework;

namespace GremDemo
{
    /// <summary>
    /// Абстрактный класс для игровых объектов (все, что отрисовывается в игре)
    /// </summary>
    abstract class Entity
    {
        // Прямоугольник для отрисовки
        public Rectangle drawRect;
    }
}
