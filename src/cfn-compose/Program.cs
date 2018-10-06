using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace cfn_compose
{
    class Program
    {
        [Option(ShortName = "V")]
        public bool Version { get; set; }

        [Option(ShortName = "v")]
        public bool Verbose { get; set; }

        [FileExists]
        [Argument(0, "input", "input file name")]
        public string Input { get; }

        [Option(ShortName = "o", Description = "The output file name")]
        public string Output { get; }

        static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        private void OnExecute()
        {
            if (Version)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyVersion = assembly.GetName().Version;
                Console.WriteLine($"AssemblyVersion {assemblyVersion}");

                var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                var fileVersion = fileVersionInfo.FileVersion;
                Console.WriteLine($"FileVersion {fileVersion}");

                var informationVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
                Console.WriteLine($"InformationalVersion {informationVersion}");
                return;
            }

            if (string.IsNullOrEmpty(Input))
            {
                Console.WriteLine("Input argument is required");
                return;
            }

            var directory = Path.GetDirectoryName(Input);

            if (Verbose)
                Console.WriteLine($"Using search directory: {directory}");

            var input = File.ReadAllText(Input);

            var output = CfnCompose.Compose(input, directory);

            if (string.IsNullOrEmpty(Output))
            {
                Console.WriteLine("No output file specified");
                Console.WriteLine("----- output -----");
                Console.WriteLine(output);
                Console.WriteLine("----- output -----");
            }
            else
            {
                Console.WriteLine($"Using output file {Output}");
                File.WriteAllText(Output, output);
            }
        }
    }
}
