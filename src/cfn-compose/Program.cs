using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace cfn_compose
{
    class Program
    {
        [Option(ShortName = "v")]
        public bool Verbose { get; set; }

        [Required]
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
            var directory = Path.GetDirectoryName(Input);

            if (Verbose)
                Console.WriteLine("Using search directory: " + directory);

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
