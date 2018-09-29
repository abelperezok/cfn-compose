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
            parser.Expect<MappingEnd>();

            if (key.Value != "File")
            {
                throw new YamlException(key.Start, val.End, "Expected a scalar named 'File'");
            }

            //Console.WriteLine($"Reading file {val.Value}");
            var input = File.ReadAllText(Path.Combine(YamlSerializer.SearchPath, val.Value));
            //Console.WriteLine($"Successfully loaded file {val.Value}");

            Console.WriteLine($"Deserializing file {val.Value}");
            var data = YamlSerializer.Deserialize(input);
            //Console.WriteLine($"Successfully Deserialized file {val.Value}");
            return data;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
        }
    }
}