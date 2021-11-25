namespace AreaOfEvent.Server.Models.Chat
{
    public abstract partial class Content
    {
        public class Text : Content
        {
            public string Data { get; init; }
        }
    }
}
