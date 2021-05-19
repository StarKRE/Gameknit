using System;
using System.Collections.Generic;

namespace GameElements
{
    public static class GameElementUtils
    {
        public static void UpdateElementState(IGameElement element, IGameStateable parent)
        {
            var parentState = parent.State;
            if (parentState >= GameState.FINISH)
            {
                return;
            }
            
            if (parentState < GameState.PREPARE)
            {
                return;
            }

            element.OnPrepareGame(parent);

            if (parentState >= GameState.READY)
            {
                element.OnReadyGame(parent);
            }

            if (parentState >= GameState.PLAY)
            {
                element.OnStartGame(parent);
            }

            if (parentState == GameState.PAUSE)
            {
                element.OnPauseGame(parent);
            }
        }
        
        public static R Find<R, T>(Dictionary<Type, T> map) where R : T
        {
            return (R) Find(map, typeof(R));
        }

        public static T Find<T>(Dictionary<Type, T> map, Type requiredType)
        {
            if (map.ContainsKey(requiredType))
            {
                return map[requiredType];
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return map[key];
                }
            }

            throw new Exception("Value is not found!");
        }

        public static bool TryFind<T>(Dictionary<Type, T> map, Type requiredType, out T item)
        {
            if (map.ContainsKey(requiredType))
            {
                item = map[requiredType];
                return true;
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    item = map[key];
                    return true;
                }
            }

            item = default;
            return false;
        }
    }
}