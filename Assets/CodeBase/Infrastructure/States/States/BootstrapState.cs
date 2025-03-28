using System;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;

namespace CodeBase.Infrastructure.States.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _stateMachine = stateMachine  ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Scenes.Game, () => _stateMachine.Enter<GameState>());
        }

        public void Exit()
        {
            
        }
    }
}