namespace Gullis
{
    public interface IDelegableState : IState 
    {
        void OnProvideParent(object parent);
    }
}