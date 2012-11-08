using System.Runtime.Serialization;

namespace Jarvis.Client.Model
{
    public class TerminateProgramAction : Action
    {
        public string ProcessName { get; set; }
    }
}
