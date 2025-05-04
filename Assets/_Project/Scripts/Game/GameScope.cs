using Siberian25.Game.World;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Siberian25.Game
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private float _slowMoTimeScale;
        [SerializeField] private float _slowMoVelocity;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>();
            builder.RegisterInstance<WorldSlowMotion>(new WorldSlowMotion(_slowMoTimeScale, _slowMoVelocity));
        }
    }
}