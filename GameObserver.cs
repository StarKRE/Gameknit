using System;

namespace GameElements
{
    /// <summary>
    ///     <para>Observes a game state.</para>
    /// </summary>
    public interface IGameObserver
    {
        /// <summary>
        ///     <para>Called when a game is prepared. <see cref="PrepareGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGamePrepared;
        
        /// <summary>
        ///     <para>Called when a game is ready. <see cref="ReadyGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameReady;

        /// <summary>
        ///     <para>Called when a game is started. <see cref="StartGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameStarted;

        /// <summary>
        ///     <para>Called when game is paused. <see cref="PauseGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGamePaused;

        /// <summary>
        ///     <para>Called when game is resumed. <see cref="ResumeGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameResumed;

        /// <summary>
        ///     <para>Called when game is finished. <see cref="FinishGame"/></para>
        /// </summary>
        /// <param name="sender"></param>
        event Action<object> OnGameFinished;

        /// <summary>
        ///     <para>Setups a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void PrepareGame(object sender);

        /// <summary>
        ///     <para>Sets ready a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void ReadyGame(object sender);

        /// <summary>
        ///     <para>Starts a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void StartGame(object sender);

        /// <summary>
        ///     <para>Pauses a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void PauseGame(object sender);
        
        /// <summary>
        ///     <para>Resumes a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void ResumeGame(object sender);

        /// <summary>
        ///     <para>Finishes a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void FinishGame(object sender);

        /// <summary>
        ///     <para>Destroys a game.</para>
        /// </summary>
        /// <param name="sender"></param>
        void DestroyGame(object sender);
    }
}