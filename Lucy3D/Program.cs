using Lucy3DLib;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Lucy3D
{
    public static class Program
    {
        private static void Main()
        {
            //Render();

            TestScript();
        }

        private static void TestScript()
        {
            SourceCode source = TestCase.SysFunc01();

            LucyScript tilde = new LucyScript(source);

            tilde.Execute();
        }

        private static void Render()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1000, 1000),
                Title = "LearnOpenTK - Camera",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
