﻿using System;
using System.IO;

namespace cfn_compose
{
    public class CfnCompose
    {
        public static string Compose(string input, string searchPath)
        {
            YamlSerializer.SearchPath = searchPath;
            var data = YamlSerializer.Deserialize(input);

            Console.WriteLine(data);

            var output = YamlSerializer.Serialize(data);

            return output;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = @""; // read from input file

            var output = CfnCompose.Compose(input, "");

            Console.WriteLine("---output---");
            Console.WriteLine(output);
        }
    }
}
