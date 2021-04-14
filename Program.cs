using System;

namespace LearnOpenTK
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = new OpenTK.Windowing.Desktop.GameWindowSettings();
            var nst = new OpenTK.Windowing.Desktop.NativeWindowSettings();

            nst.WindowBorder = OpenTK.Windowing.Common.WindowBorder.Fixed;
            //nst.Size = new OpenTK.Mathematics.Vector2i(200,200);
            using (Game game=new Game (st, nst))
            {
                game.Run();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
