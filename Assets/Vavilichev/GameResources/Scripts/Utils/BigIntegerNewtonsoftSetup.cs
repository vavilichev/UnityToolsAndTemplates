using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Vavilichev.GameResources.Utils
{
    public static class BigIntegerNewtonsoftSetup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitNewtonsoftBigIntegerConverter()
        {
            var serializationSettings = JsonConvert.DefaultSettings != null
                ? JsonConvert.DefaultSettings() : new JsonSerializerSettings();

            if (serializationSettings.Converters.All(c => c.GetType() != typeof(BigIntegerConverter)))
            {
                serializationSettings.Converters.Add(new BigIntegerConverter());
                
                JsonConvert.DefaultSettings = () => serializationSettings;
            }
        }
    }
}
