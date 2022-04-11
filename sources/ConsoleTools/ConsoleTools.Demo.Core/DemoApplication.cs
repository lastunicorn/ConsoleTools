using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.Core
{
    public class DemoApplication
    {
        private static ControlRepeater controlRepeater;

        public void Run()
        {
            DemoPackages demoPackages = LoadDemoPackages();
            MainMenu mainMenu = new MainMenu(demoPackages, this);

            controlRepeater = new ControlRepeater
            {
                Control = new StackPanel
                {
                    Controls =
                    {
                        new ApplicationHeader(),
                        mainMenu
                    }
                }
            };

            controlRepeater.Display();
        }

        private static DemoPackages LoadDemoPackages()
        {
            DemoPackages demoPackages = new DemoPackages();
            demoPackages.Load();

            return demoPackages;
        }

        public void RequestExit()
        {
            controlRepeater.RequestClose();
        }
    }
}