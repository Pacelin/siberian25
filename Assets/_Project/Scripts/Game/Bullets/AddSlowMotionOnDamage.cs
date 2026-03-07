using System;
using R3;
using UnityEngine;

namespace Siberian25.Game.Characters
{
	public class AddSlowMotionOnDamage : MonoBehaviour
	{
		[SerializeField, Min(0f)]
		private float _duration = .2f;

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

		private void OnDamage(int damage) => GameContext.SlowMo.PlaySlowMotion(_duration);
	}
}