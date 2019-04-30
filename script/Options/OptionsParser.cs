using System;
using System.Collections.Generic;

namespace Options
{
    class OptionsParser
    {
        static List<string> parameter;

        public static void Parse(string[] args)
        {
            try
            {
                parameter = Config.OptionSetting.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                return;
            }
        }

        public static void ShowHelp(OptionSet optionSetting)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            optionSetting.WriteOptionDescriptions(Console.Out);

            Environment.Exit(0);
        }
    }
}
