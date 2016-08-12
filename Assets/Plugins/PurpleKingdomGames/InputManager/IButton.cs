namespace PurpleKingdomGames.Unity.InputManager
{
    public interface IButton
    {
        bool Invert { get; set; }
        string Name { get; }

        float GetCurrentValue();

        float GetCurrentRawValue();

        bool IsDown();

        bool IsUp();

        bool IsHeld();
    }
}