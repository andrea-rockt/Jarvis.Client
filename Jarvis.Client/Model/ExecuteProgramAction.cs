using System.Runtime.Serialization;

namespace Jarvis.Client.Model
{
    public class ExecuteProgramAction : Action
    {
        public string CommandString { get; set; }
        public string ExecuteInDirectory { get; set; }
    }
}
