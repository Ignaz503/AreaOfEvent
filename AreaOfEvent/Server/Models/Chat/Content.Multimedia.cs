namespace AreaOfEvent.Server.Models.Chat
{
    public abstract partial class Content
    {
        public class Multimedia : Content
        {
            public enum MediaType
            {
                Image,
                Video,
                Audio
            }

            public MediaType ContentType { get; set; }

            public string FilePath { get; init; }
        }
    }
}
