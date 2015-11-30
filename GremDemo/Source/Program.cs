/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : Program.cs                                             *
* Programmers  : ���������� �.�. �������� �.�.                          *
* Created      : 17/11/15                                               *
* Last Revision: 30/11/15                                               *
* Comments     : MonoGame game project using DirectX                    *
*                                                                       *
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
/*    end of file Program.cs */

