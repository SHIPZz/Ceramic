using CodeBase.Gameplay.Common;
using CodeBase.Gameplay.Services;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.Providers;
using CodeBase.Infrastructure.States.Factory;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {

        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindCommonServices();
            BindAppServices();
            BindStates();
            BindFileService();
            BindStateMachine();
            BindMatrixOffsetService();
        }

        private void BindMatrixOffsetService()
        {
            Container.Bind<IMatrixOffsetService>().To<MatrixOffsetService>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
        }

        private void BindFileService()
        {
            Container.Bind<IFileService>().To<FileService>().AsSingle();
        }

        private void BindStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
        }

        private void BindAppServices()
        {
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ILevelProvider>().To<LevelProvider>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IStateMachine>().Enter<BootstrapState>();
        }
    }
}