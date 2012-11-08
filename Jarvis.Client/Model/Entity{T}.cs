using System.Runtime.Serialization;

namespace Jarvis.Client.Model
{
    public class Entity<T>
    {
        public T Id { get; set; }
    }
}
