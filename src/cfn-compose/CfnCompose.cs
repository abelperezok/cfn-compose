using System;

namespace cfn_compose
{
    public class CfnCompose
    {
        public static string Compose(string input, string searchPath)
        {
            YamlSerializer.SearchPath = searchPath;
            var data = YamlSerializer.Deserialize(input);
            var output = YamlSerializer.Serialize(data);
            return output;
        }
    }
}
