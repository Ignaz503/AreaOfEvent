namespace AreaOfEvent.Shared.Languages
{
    public partial class Language
    {
        static class DefaultLanguages
        {
            public static readonly Language[] Data = new Language[]
            {
                new Language
                {
                    ID = -1,
                    Name = "Deutsch",
                    ISO639_2_B_Identifier ="deu"
                },
                new Language
                {
                    ID = -2,
                    Name ="English",
                    ISO639_2_B_Identifier ="eng"
                }
            };
        }
    }
}
