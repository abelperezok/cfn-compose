using System.IO;
using YamlDotNet.Serialization;

namespace cfn_compose
{
    public class YamlSerializer
    {
        public static string ParentDirectory = "";
        private const string IncludeTag = "!Include";

        public static object Deserialize(string yaml)
        {
            var reader = new StringReader(yaml);
            var deserializer = new DeserializerBuilder()
                .WithTypeConverter(new IncludeTagConverter())
                .WithTagMapping(IncludeTag, typeof(IncludeTag))
                .WithTagMapping("!Ref", typeof(string))
                .Build();
            var data = deserializer.Deserialize(reader);
            return data;
        }

        public static string Serialize(object data)
        {
            var serializer = new SerializerBuilder()
                .WithTypeConverter(new IncludeTagConverter())
                .Build();
            var yaml = serializer.Serialize(data);
            return yaml;
        }
    }
}