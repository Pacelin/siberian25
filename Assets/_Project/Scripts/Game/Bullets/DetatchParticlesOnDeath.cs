using System;
using R3;
using Siberian25.Game.Characters;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
	public class DetatchParticlesOnDeath : MonoBehaviour
	{
		[SerializeField] private HealthComponent _health;
		[SerializeField] private Transform _detatchTo;
		[SerializeField] private ParticleSystem _ps;

		private IDisposable _disposable;

		private void OnEnable()
		{
			if (_health != null && _health.IsAlive)
				_disposable = _health.OnDeath.Subscribe(_ => Detatch());
		}

		private void OnDisable()
		{
			_disposable?.Dispose();
		}

		private void Detatch()
		{
			transform.SetParent(_detatchTo);
			_ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
	}
}