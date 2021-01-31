using System.Collections;
using Gullis;

namespace Gullis
{
    public interface ISceneInitializer
    {
        IEnumerator OnInitializeScene(object sender, ISceneGameContext context);
    }
    
    public abstract class GameSceneInitializer : GameNode, ISceneInitializer
    {
        protected ISceneGameContext context { get; private set; }
        
        #region Lifecycle
        
        protected override void OnPrepareGame(object sender)
        {
            base.OnPrepareGame(sender);
            this.context = this.GetContext<ISceneGameContext>();
            this.context.RegisterInitializer(this);
        }
        
        protected override void OnDestroyGame(object sender)
        {
            base.OnDestroyGame(sender);
            this.context.UnregisterInitializer(this);
        }
        
        #endregion

        public abstract IEnumerator OnInitializeScene(object sender, ISceneGameContext context);
    }
}