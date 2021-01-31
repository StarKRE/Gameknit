using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gullis
{
    public interface ISceneGameContext : IGameContext
    {
        GameObject myObject { get; }

        Transform myTransform { get; }

        void RegisterLoader(ISceneLoader loader);

        void UnregisterLoader(ISceneLoader loader);

        void RegisterInitializer(ISceneInitializer initializer);

        void UnregisterInitializer(ISceneInitializer initializer);
    }

    public class SceneGameContext : GameNodeContext, ISceneGameContext
    {
        #region Events

        public override event Action<object> OnGamePreparedEvent;

        public override event Action<object> OnGameReadyEvent;

        public override event Action<object> OnGameStartedEvent;

        public override event Action<object> OnGamePausedEvent;

        public override event Action<object> OnGameResumedEvent;

        public override event Action<object> OnGameFinishedEvent;

        #endregion

        public GameObject myObject { get; private set; }

        public Transform myTransform { get; private set; }

        protected readonly HashSet<ISceneLoader> sceneLoaders;

        protected readonly HashSet<ISceneInitializer> sceneInitializers;

        [SerializeField]
        private GameNode[] rootNodes;

        #region Lifecycle

        public SceneGameContext()
        {
            this.sceneLoaders = new HashSet<ISceneLoader>();
            this.sceneInitializers = new HashSet<ISceneInitializer>();
        }

        protected virtual void Awake()
        {
            this.myObject = this.gameObject;
            this.myTransform = this.transform;
            foreach (var node in this.rootNodes)
            {
                this.RegisterNode(node);
            }
        }

        public sealed override void PrepareGame(object sender)
        {
            base.PrepareGame(sender);
            this.StartCoroutine(this.PrepareGameRoutine(sender));
        }

        private IEnumerator PrepareGameRoutine(object sender)
        {
            foreach (var loader in this.sceneLoaders)
            {
                yield return loader.OnLoadScene(sender, this);
            }

            foreach (var initializer in this.sceneInitializers)
            {
                yield return initializer.OnInitializeScene(sender, this);
            }

            this.NotifyAboutGamePrepared(sender);
        }

        protected virtual void NotifyAboutGamePrepared(object sender)
        {
            this.OnGamePreparedEvent?.Invoke(sender);
        }

        public sealed override void ReadyGame(object sender)
        {
            base.ReadyGame(sender);
            this.NotifyAboutGameReady(sender);
        }

        protected virtual void NotifyAboutGameReady(object sender)
        {
            this.OnGameReadyEvent?.Invoke(sender);
        }

        public sealed override void StartGame(object sender)
        {
            base.StartGame(sender);
            this.NotifyAboutGameStarted(sender);
        }

        protected virtual void NotifyAboutGameStarted(object sender)
        {
            this.OnGameStartedEvent?.Invoke(sender);
        }

        public sealed override void PauseGame(object sender)
        {
            base.PauseGame(sender);
            this.NotifyAboutGamePaused(sender);
        }

        protected virtual void NotifyAboutGamePaused(object sender)
        {
            this.OnGamePausedEvent?.Invoke(sender);
        }

        public sealed override void ResumeGame(object sender)
        {
            base.ResumeGame(sender);
            this.NotifyAboutGameResumed(sender);
        }

        protected virtual void NotifyAboutGameResumed(object sender)
        {
            this.OnGameResumedEvent?.Invoke(sender);
        }

        public sealed override void FinishGame(object sender)
        {
            base.FinishGame(sender);
            this.NotifyAboutGameFinished(sender);
        }

        protected virtual void NotifyAboutGameFinished(object sender)
        {
            this.OnGameFinishedEvent?.Invoke(sender);
        }

        #endregion

        public void RegisterLoader(ISceneLoader loader)
        {
            this.sceneLoaders.Add(loader);
        }

        public void UnregisterLoader(ISceneLoader loader)
        {
            this.sceneLoaders.Remove(loader);
        }

        public void RegisterInitializer(ISceneInitializer initializer)
        {
            this.sceneInitializers.Add(initializer);
        }

        public void UnregisterInitializer(ISceneInitializer initializer)
        {
            this.sceneInitializers.Remove(initializer);
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            var rootNodes = new List<GameNode>();
            foreach (Transform childTransform in this.transform)
            {
                if (childTransform.TryGetComponent(out GameNode node))
                {
                    rootNodes.Add(node);
                }
            }

            this.rootNodes = rootNodes.ToArray();
        }
#endif
    }
}