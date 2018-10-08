using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace cfn_compose
{
    public class IntToStringConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return typeof(string).IsAssignableFrom(type);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            return null;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            //Console.WriteLine($"writing {value} {type}");
            var strValue = value.ToString();
            int val;
            if (int.TryParse(strValue, out val))
            {
                emitter.Emit(new Scalar(null, null, strValue, ScalarStyle.DoubleQuoted, true, false));
            }
            else
            {
                emitter.Emit(new Scalar(strValue));
            }
        }
    }
}