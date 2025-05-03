using R3;
using Siberian25.Game.Characters;
using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
	public class DetatchParticlesOnDeath : MonoBehaviour
	{
		[SerializeField] private HealthComponent _health;
		[SerializeField] private Transform _detatchTo;
		[SerializeField] private ParticleSystem _ps;

		private CompositeDisposable _disposables;

		private void OnEnable()
		{
			if (_health != null && _health.IsAlive)
				_health.OnDeath.Subscribe(_ => Detatch()).AddTo(_disposables);

			Runtime.ObservePause().Subscribe(OnSetPaused).AddTo(_disposables);
		}
		private void OnSetPaused(bool isPaused)
		{
			if (isPaused)
				_ps.Pause(true);
			else
				_ps.Play(true);
		}

		private void OnDisable()
		{
			_disposables?.Dispose();
		}

		private void Detatch()
		{
			transform.SetParent(_detatchTo);
			_ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
	}
}