using System.Collections;

namespace Gullis
{
    public interface ISceneLoader
    {
        IEnumerator OnLoadScene(object sender, ISceneGameContext context);
    }
}