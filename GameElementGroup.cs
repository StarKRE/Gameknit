using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a group of elements.</para>
    /// </summary>
    public interface IGameElementGroup : IEnumerable<IGameElement>
    {
        /// <summary>
        ///     <para>Returns elements in this group.</para>
        /// </summary>
        IEnumerable<IGameElement> GetElements();

        /// <summary>
        ///     <para>Returns element count in this group.</para>
        /// </summary>
        int GetElementCount();
    }

    public abstract class GameElementGroup : GameElement, IGameElementGroup
    {
        public abstract IEnumerator<IGameElement> GetEnumerator();

        public abstract IEnumerable<IGameElement> GetElements();

        public abstract int GetElementCount();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
        protected override void OnPrepareGame(object sender)
        {
            base.OnPrepareGame(sender);
            foreach (var element in this)
            {
                element.OnPrepareGame(sender);
            }
        }

        protected override void OnReadyGame(object sender)
        {
            base.OnReadyGame(sender);
            foreach (var element in this)
            {
                element.OnReadyGame(sender);
            }
        }

        protected override void OnStartGame(object sender)
        {
            base.OnStartGame(sender);
            foreach (var element in this)
            {
                element.OnStartGame(sender);
            }
        }

        protected override void OnPauseGame(object sender)
        {
            base.OnPauseGame(sender);
            foreach (var element in this)
            {
                element.OnPauseGame(sender);
            }
        }

        protected override void OnResumeGame(object sender)
        {
            base.OnResumeGame(sender);
            foreach (var element in this)
            {
                element.OnResumeGame(sender);
            }
        }

        protected override void OnFinishGame(object sender)
        {
            base.OnFinishGame(sender);
            foreach (var element in this)
            {
                element.OnFinishGame(sender);
            }
        }

        protected override void OnDestroyGame(object sender)
        {
            foreach (var element in this)
            {
                element.OnDestroyGame(sender);
            }

            base.OnDestroyGame(sender);
        }
    } 
}