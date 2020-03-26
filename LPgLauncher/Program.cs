using System;
using System.IO;
using System.Text.RegularExpressions;
using IniParser;
using IniParser.Model;

namespace LPgLauncher
{
  class Program
  {
    static void Main(string[] args)
    {
      string CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      FileIniDataParser parser = new FileIniDataParser();
      IniData ini = null;

      try
      {
        ini = parser.ReadFile(CurrentDirectory + "\\LPgLauncher.ini");
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("Unable to locate LPgLauncher.ini in the current directory.");
        Environment.Exit(-1);
      }

      if (args.Length > 0)
      {
        if (args.Length == 1)
        {
          if (args[0].Length < 255)
          {
            string[] firstArg = Regex.Split(args[0], "://");
      
            string firstKey = firstArg[0].ToLower() + ".exe";
            string secondKey = firstArg[0].ToLower() + ".parms";

            string firstValue = ini["URI"][firstKey.ToString()];
            string secondValue = ini["URI"][secondKey.ToString()];
            firstArg[1] = firstArg[1].Replace("/", "");
            firstArg[1] = firstArg[1].Replace(@"\", "");

            secondValue += " " + firstArg[1];

            // if there are double spaces, replace with single space
            Regex.Replace(secondValue, @"\s+", " ");

            if (!string.IsNullOrEmpty(firstValue))
            {
              if (!string.IsNullOrEmpty(secondValue))
              {
                Console.WriteLine("Executing: " + firstValue + " " + secondValue);
                Console.ReadKey();
                System.Diagnostics.Process.Start(firstValue, secondValue);
                Environment.Exit(0);
              }
              else
              {
                Console.WriteLine("The read value (" + secondKey + ") from LPgLauncher.ini is empty. Closing.");
                Environment.Exit(-5);
              }
            }
            else
            {
              Console.WriteLine("The read value (" + firstKey + ") from LPgLauncher.ini is empty. Closing.");
              Environment.Exit(-4);
            }
          }
          else
          {
            Console.WriteLine("The passed in argument seems a bit long. Greater than 255 charaters. Closing.");
            Environment.Exit(-3);
          }
        }
        else
        {
          Console.WriteLine("Only one argument allowed. Closing.");
          Environment.Exit(-4);
        }
      }
      else
      {
        Console.WriteLine("No URI argument passed in. Closing.");
        Environment.Exit(-2);
      }
    }
  }
}
