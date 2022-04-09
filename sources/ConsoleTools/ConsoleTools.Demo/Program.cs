using System;

namespace ConsoleTools.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Load all plugins

            // Display menu

            // Execute chosen action
        }
    }

    public interface IDemoPackage
    {
        string ShortDescription { get; }

        void ExecuteDemo();
    }
}