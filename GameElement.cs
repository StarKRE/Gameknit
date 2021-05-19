using UnityEngine;

namespace GameElements
{
    /// <summary>
    ///     <para>A contract of game element.</para>
    /// </summary>
    public interface IGameElement : IGameStateable
    {
        /// <summary>
        ///     <para>Called when this element is registered into the game context.</para>
        /// </summary>
        /// <param name="parent">Who registers this element.</param>
        /// <param name="system">Game system.</param>
        void OnRegistered(object parent, IGameSystem system);

        /// <summary>
        ///     <para>Called when a game prepares.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnPrepareGame(object sender);

        /// <summary>
        ///     <para>Called when a game is ready.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnReadyGame(object sender);

        /// <summary>
        ///     <para>Called when a game is started.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnStartGame(object sender);

        /// <summary>
        ///     <para>Called when a game is paused.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnPauseGame(object sender);

        /// <summary>
        ///     <para>Called when a game is resumed.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnResumeGame(object sender);

        /// <summary>
        ///     <para>Called when a game is ended.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnFinishGame(object sender);

        /// <summary>
        ///     <para>Called when a game is destroyed.</para>
        /// </summary>
        /// <param name="sender"></param>
        void OnDestroyGame(object sender);

        /// <summary>
        ///     <para>Called when this element is unregistered from the game context.</para>
        /// </summary>
        void OnUnregistered();
    }

    /// <inheritdoc cref="IGameElement"/>
    public abstract class GameElement : MonoBehaviour, IGameElement
    {
        /// <summary>
        ///     <para>Game state of this element.</para>
        /// </summary>
        public GameState State { get; protected set; }

        /// <summary>
        ///     <para>A game system reference.</para>
        /// </summary>
        protected IGameSystem GameSystem { get; private set; }

        /// <summary>
        ///     <para>A parent element reference.</para>
        /// </summary>
        protected object Parent { get; private set; }

        #region Lifecycle

        void IGameElement.OnRegistered(object parent, IGameSystem system)
        {
            this.Parent = parent;
            this.GameSystem = system;
            this.State = GameState.CREATE;
            this.OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }

        void IGameElement.OnPrepareGame(object sender)
        {
            if (this.State == GameState.CREATE)
            {
                this.State = GameState.PREPARE;
                this.OnPrepareGame(sender);
            }
        }

        protected virtual void OnPrepareGame(object sender)
        {
        }

        void IGameElement.OnReadyGame(object sender)
        {
            if (this.State == GameState.PREPARE)
            {
                this.State = GameState.READY;
                this.OnReadyGame(sender);
            }
        }

        protected virtual void OnReadyGame(object sender)
        {
        }

        void IGameElement.OnStartGame(object sender)
        {
            if (this.State == GameState.READY)
            {
                this.State = GameState.PLAY;
                this.OnStartGame(sender);
            }
        }

        protected virtual void OnStartGame(object sender)
        {
        }

        void IGameElement.OnPauseGame(object sender)
        {
            if (this.State == GameState.PLAY)
            {
                this.State = GameState.PAUSE;
                this.OnPauseGame(sender);
            }
        }

        protected virtual void OnPauseGame(object sender)
        {
        }

        void IGameElement.OnResumeGame(object sender)
        {
            if (this.State == GameState.PAUSE)
            {
                this.OnResumeGame(sender);
                this.State = GameState.PLAY;
            }
        }

        protected virtual void OnResumeGame(object sender)
        {
        }

        void IGameElement.OnFinishGame(object sender)
        {
            if (this.State < GameState.FINISH)
            {
                this.State = GameState.FINISH;
                this.OnFinishGame(sender);
            }
        }

        protected virtual void OnFinishGame(object sender)
        {
        }

        void IGameElement.OnDestroyGame(object sender)
        {
            if (this.State < GameState.DESTROY)
            {
                this.State = GameState.DESTROY;
                this.OnDestroyGame(sender);
            }
        }

        protected virtual void OnDestroyGame(object sender)
        {
        }

        void IGameElement.OnUnregistered()
        {
            this.OnUnregistered();
        }

        protected virtual void OnUnregistered()
        {
        }

        #endregion
    }
}