using System;
using R3;
using Siberian25.Game.Characters;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
	public class DetachParticlesOnDeath : MonoBehaviour
	{
		[SerializeField] private HealthComponent _health;
		[SerializeField] private Transform _detatchTo;

		[SerializeField] private ParticleSystem[] _particleSystems;
		[SerializeField] private GameObject[] _gameObjects;

		[SerializeField] private bool _enableGameObjectsOnDetach = false;
		[SerializeField] private bool _playOnDetach = false;
		[SerializeField] private bool _stopOnDetach = true;

		private IDisposable _disposable;

		private void OnEnable()
		{
			if (_health != null && _health.IsAlive)
				_disposable = _health.OnDeath.Subscribe(_ => Detach());
		}
		private void OnDisable()
		{
			_disposable?.Dispose();
		}

		private void Detach()
		{
			transform.SetParent(_detatchTo);

			if (_enableGameObjectsOnDetach)
				foreach (GameObject go in _gameObjects)
					go.SetActive(true);
			if (_playOnDetach)
				foreach (ParticleSystem ps in _particleSystems)
					ps.Play(true);
			if (_stopOnDetach)
				foreach (ParticleSystem ps in _particleSystems)
					ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
	}
}