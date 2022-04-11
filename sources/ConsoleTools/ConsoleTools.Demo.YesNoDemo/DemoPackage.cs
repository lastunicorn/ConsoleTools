using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.TextMenuDemo
{
    public class DemoPackage : IDemoPackage
    {
        private static ControlRepeater menuRepeater;

        public string Name => "YesNoQuestion Demo";


        public void ExecuteDemo()
        {
            DisplayApplicationHeader();

            menuRepeater = new ControlRepeater
            {
                Control = new MainMenu()
            };

            menuRepeater.Display();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader()
            {
                Appendix = "YesNoQuestion Demo"
            };
            applicationHeader.Display();
        }

        public static void RequestStop()
        {
            menuRepeater.RequestClose();
        }
    }
}