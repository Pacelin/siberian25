using System;
using R3;
using UnityEngine;

namespace Siberian25.Game.Characters
{
	public class AddSlowMotionOnDamage : MonoBehaviour
	{
		[SerializeField, Range(0f, 1f)]
		private float _scale = .1f;
		[SerializeField, Min(0f)]
		private float _duration = .2f;
		[SerializeField, Min(0f)]
		private float _fadeTime = .1f;

		[SerializeField] private HealthComponent _health;

		private IDisposable _disposable;

		private void OnEnable()
		{
			_disposable = _health.OnDamage.Subscribe(OnDamage);
		}

		private void OnDisable()
		{
			_disposable?.Dispose();
		}

		private void OnDamage(int damage)
		{
			GameSlowMotionService.StartSlowMotion(_scale, _duration, _fadeTime);
		}
	}
}