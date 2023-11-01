using GameSoft.Tools;
using GameSoft.Tools.ZenjectExtensions;
using Zenject;

namespace GameSoft
{
    public class ToolInstaller : MonoInstaller<ToolInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InstantiateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityMainThreadDispatcher>().AsSingle().NonLazy();
        }
    }
}