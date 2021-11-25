namespace AreaOfEvent.Client.Components.Rendering
{
    public struct ClickConext
    {
        public int MouseX { get; init; }
        public int MouseY { get; init; }

        public bool IsUsed { get; private set; }

        public void Use()
        {
            IsUsed = true;
        }

        public override string ToString()
            => $"{{x: {MouseX}, y: {MouseY}, Used: {IsUsed}}}";

    }

}
