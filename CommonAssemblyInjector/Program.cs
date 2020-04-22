﻿using System;
using System.Threading.Tasks;

namespace CommonAssemblyInjector
{
    class Program
    {
        private const string SOL_DIR = "/solDir:";
        private const string PATH = "/path:";
        private const string VERSION = "/version:";

        static async Task Main(string[] args)
        {
            //const string SOLUTION_DIR = @"C:\Users\MA101802\source\repos\CommonAssemblyTestProject";
            //const string COMMON_ASSEMBLY_INFO_PATH = @"C:\Users\MA101802\source\repos\CommonAssemblyTestProject\CommonAssemblyInfo.cs";
            //const string VERSION = "1.0.0.0";

            if (args.Length != 3)
            {
                PrintUsage();
                return;
            }

            foreach (string arg in args)
            {
                if (arg.StartsWith(SOL_DIR))
                {
                    Injector.SolutionDir = arg.Substring(SOL_DIR.Length).StripQuotes();
                }

                if (arg.StartsWith(PATH))
                {
                    Injector.CommonAssemblyInfoPath = arg.Substring(PATH.Length).StripQuotes();
                }

                if (arg.StartsWith(VERSION))
                {
                    Injector.TargetVersion = arg.Substring(VERSION.Length).StripQuotes();
                }
            }

            await Injector.TryAddCommonAssemblyToProjects();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: CommonAssemblyInjector "
                              + "[/solDir:<directory_of_solution_to_inject>] "
                              + "[/path:<path_of_CommonAssemblyInfo.cs_File>] "
                              + "[/version:<version_of_assemblies_to_inject(e.g. \"1.0.0.0\")>]");
        }

    }

    public static class Extensions
    {
        public static string StripQuotes(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            if (text.StartsWith("\"") && text.EndsWith("\"") && text.Length > 2)
            {
                text = text.Substring(1, text.Length - 2);
            }

            if (text.StartsWith("'") && text.EndsWith("'") && text.Length > 2)
            {
                text = text.Substring(1, text.Length - 2);
            }

            return text;
        }
    }
}
