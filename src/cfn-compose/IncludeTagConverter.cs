using System;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace cfn_compose
{
    public class IncludeTagConverter: IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return typeof(IncludeTag).IsAssignableFrom(type);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            parser.Expect<MappingStart>();
            var key = parser.Expect<Scalar>();
            var val = parser.Expect<Scalar>();
            Console.WriteLine($"ReadYaml - {key.Value}, {val.Value}");
            parser.Expect<MappingEnd>();

            if (key.Value != "File")
            {
                throw new YamlException(key.Start, val.End, "Expected a scalar named 'File'");
            }

            var input = File.ReadAllText(Path.Combine(YamlSerializer.SearchPath, val.Value));
            var data = YamlSerializer.Deserialize(input);
            return data;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
        }
    }
}