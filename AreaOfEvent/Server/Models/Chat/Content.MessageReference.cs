namespace AreaOfEvent.Server.Models.Chat
{
    public abstract partial class Content
    {
        public class MessageReference : Text
        {
            public long RefrencedMessageID { get; init; }
        }
    }
}
