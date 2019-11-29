using System.Reflection;
using Auction.Buddy.Core.Common.Storage;
using Newtonsoft.Json;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventSerializer
    {
        string Serialize<TId>(DomainEvent<TId> domainEvent) 
            where TId : Identity;

        DomainEvent<TId> Deserialize<TId>(PersistenceEvent persistenceEvent) 
            where TId : Identity;
    }

    public class JsonDomainEventSerializer : DomainEventSerializer
    {
        private static readonly MethodInfo DeserializeMethod =
            typeof(JsonDomainEventSerializer).GetMethod(nameof(DeserializeEvent), BindingFlags.Static | BindingFlags.NonPublic);
        
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateFormatString = "o",
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateParseHandling = DateParseHandling.DateTimeOffset
        };
        

        private readonly DomainEventNameToTypeTranslator _translator;

        public JsonDomainEventSerializer()
            : this(new TypeNameDomainEventNameToTypeTranslator())
        {
            
        }
        
        public JsonDomainEventSerializer(DomainEventNameToTypeTranslator translator)
        {
            _translator = translator;
        }

        public string Serialize<TId>(DomainEvent<TId> domainEvent)
            where TId : Identity
        {
            return JsonConvert.SerializeObject(domainEvent, SerializerSettings);
        }

        public DomainEvent<TId> Deserialize<TId>(PersistenceEvent persistenceEvent) 
            where TId : Identity
        {
            var eventType = _translator.GetDomainEventType(persistenceEvent.EventName);
            var deserializeMethod = DeserializeMethod.MakeGenericMethod(eventType);
            return (DomainEvent<TId>) deserializeMethod
                .Invoke(this, new object[] {persistenceEvent.SerializedEvent});
        }

        private static T DeserializeEvent<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, SerializerSettings);
        }
    }
}