using System;
using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    /// <summary>
    ///     <para>Represents an element as a dictionary of generic elements.</para>
    /// </summary>
    public interface IGameElementLayer : IGameElementGroup
    {
        /// <summary>
        ///     <para>Adds an element into the dictionary.</para>
        /// </summary>
        bool AddElement(IGameElement element);

        /// <summary>
        ///     <para>Removes an element from the dictionary.</para>
        /// </summary>
        bool RemoveElement(IGameElement element);

        /// <summary>
        ///     <para>Returns an element of "T".</para>
        /// </summary>
        T GetElement<T>() where T : IGameElement;

        /// <summary>
        ///     <para>Tries to get an element of "T".</para>
        /// </summary>
        bool TryGetElement<T>(out T element);
    }

    public class GameElementLayer : GameElementGroup, IGameElementLayer
    {
        private readonly Dictionary<Type, IGameElement> registeredElementMap;

        protected GameElementLayer()
        {
            this.registeredElementMap = new Dictionary<Type, IGameElement>();
        }

        public bool AddElement(IGameElement element)
        {
            var type = element.GetType();
            if (this.registeredElementMap.ContainsKey(type))
            {
                return false;
            }

            this.registeredElementMap.Add(type, element);
            element.OnRegistered(this, this.GameSystem);
            GameElementUtils.UpdateElementState(element, this);
            return true;
        }
        
        public bool RemoveElement(IGameElement element)
        {
            var type = element.GetType();
            if (!this.registeredElementMap.Remove(type))
            {
                return false;
            }

            element.OnUnregistered();
            return true;
        }

        public T GetElement<T>() where T : IGameElement
        {
            return GameElementUtils.Find<T, IGameElement>(this.registeredElementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            if (GameElementUtils.TryFind(this.registeredElementMap, typeof(T), out var result))
            {
                element = (T) result;
                return true;
            }

            element = default;
            return false;
        }

        public override IEnumerable<IGameElement> GetElements()
        {
            return this.registeredElementMap.Values;
        }

        public override int GetElementCount()
        {
            return this.registeredElementMap.Count;
        }

        public override IEnumerator<IGameElement> GetEnumerator()
        {
            foreach (var element in this.registeredElementMap.Values)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}