using System;
using System.IO;
using cfn_compose;
using NUnit.Framework;

namespace cfn_compose_tests
{
    public class CfnComposeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var parentDir = Path.GetFullPath("../../../");
            System.Environment.CurrentDirectory = parentDir;

            var currentPath = Path.GetFullPath(".");
            Console.WriteLine(currentPath);

            var input = File.ReadAllText("cloud-api.yaml");
            var output = CfnCompose.Compose(input);
            Console.WriteLine(output);

            Assert.Pass();
        }
    }
}