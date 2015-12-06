/************************************************************************
* ��� ������� � ������ ������ ��������� ���������� ����������:          *   
*  1) MonoGame 3.4                                                      *
*  2) Microsoft .Net Framework 4                                        * 
* ������� (solution) ��� Visual Studio 2015 Community                   *
*************************************************************************/

namespace GremDemo
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DemoGame game = new DemoGame())
            {
                game.Run();
            }
        }
    }
#endif
}

