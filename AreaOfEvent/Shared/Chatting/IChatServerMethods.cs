using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Chatting
{
    public interface IChatServerMethods
    {
        public const string EndpointName = "/chathub";

        Task SendMessage( string userName, string message );

    }
}
