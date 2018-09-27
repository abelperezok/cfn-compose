using System;
using System.IO;
using cfn_compose;
using NUnit.Framework;

namespace cfn_compose_tests
{
    public class CfnComposeTests
    {
        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            var parentDir = Path.GetFullPath("../../../");
            System.Environment.CurrentDirectory = parentDir;
        }

        [Test]
        public void NoTags_NoChange()
        {
            var searchDir = GetTestCaseSearchDirectory("NoTags");
            var input = File.ReadAllText(Path.Combine(searchDir, "input.yaml"));
            var output = CfnCompose.Compose(input, searchDir);
            var expectedOutput = File.ReadAllText(Path.Combine(searchDir, "output.yaml"));
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void IncludeTag_1Level_1Tag()
        {
            var searchDir = GetTestCaseSearchDirectory("1Level_1Tag");
            var input = File.ReadAllText(Path.Combine(searchDir, "input.yaml"));
            var output = CfnCompose.Compose(input, searchDir);
            var expectedOutput = File.ReadAllText(Path.Combine(searchDir, "output.yaml"));
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void IncludeTag_1Level_2Tags()
        {
            var searchDir = GetTestCaseSearchDirectory("1Level_2Tags");
            var input = File.ReadAllText(Path.Combine(searchDir, "input.yaml"));
            var output = CfnCompose.Compose(input, searchDir);
            var expectedOutput = File.ReadAllText(Path.Combine(searchDir, "output.yaml"));
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void IncludeTag_2Levels()
        {
            var searchDir = GetTestCaseSearchDirectory("2Levels");
            var input = File.ReadAllText(Path.Combine(searchDir, "input.yaml"));
            var output = CfnCompose.Compose(input, searchDir);
            var expectedOutput = File.ReadAllText(Path.Combine(searchDir, "output.yaml"));
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void IncludeTag_2Levels_SeveralTags()
        {
            var searchDir = GetTestCaseSearchDirectory("2Levels_SeveralTags");
            var input = File.ReadAllText(Path.Combine(searchDir, "input.yaml"));
            var output = CfnCompose.Compose(input, searchDir);
            // TestContext.Progress.WriteLine(output);
            var expectedOutput = File.ReadAllText(Path.Combine(searchDir, "output.yaml"));
            Assert.AreEqual(expectedOutput, output);
        }

        private static string GetTestCaseSearchDirectory(string directory)
        {
            return Path.GetFullPath($"TestCases/{directory}/");
        }
    }
}