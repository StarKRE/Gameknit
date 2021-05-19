namespace GameElements
{
    /// <summary>
    ///     <para>A state of game.</para>
    /// </summary>
    public enum GameState
    {
        CREATE = 1,
        PREPARE = 2,
        READY = 3,
        PLAY = 4,
        PAUSE = 5,
        FINISH = 6,
        DESTROY = 7
    }

    /// <summary>
    ///     <para>Provides a game state.</para>
    /// </summary>
    public interface IGameStateable
    {
        GameState State { get; }
    }
}