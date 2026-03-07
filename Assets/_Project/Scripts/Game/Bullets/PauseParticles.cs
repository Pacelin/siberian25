using System;
using R3;
using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
	public class PauseParticles : MonoBehaviour
	{
		[SerializeField] private ParticleSystem[] _particleSystems;

		private IDisposable _disposable;

		private void OnEnable()
		{
			_disposable = Runtime.ObservePause().Subscribe(OnSetPaused);
		}

		private void OnDisable()
		{
			_disposable?.Dispose();
		}

		private void OnSetPaused(bool isPaused)
		{
			if (isPaused)
				foreach (ParticleSystem ps in _particleSystems)
					ps.Pause(false);
			else
				foreach (ParticleSystem ps in _particleSystems)
					ps.Play(false);
		}
	}
}