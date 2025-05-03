using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using TSS.Core;
using UnityEngine;

namespace Siberian25.Game
{
	public static class GameSlowMotionService
	{
		private static CancellationTokenSource _cts;

		public static void StartSlowMotion(float scale, float duration, float fadeTime)
		{
			_cts?.Cancel();
			_cts?.Dispose();
			_cts = new CancellationTokenSource();

			StartSlowMotionAsync(scale, duration, fadeTime).Forget();
		}

		private static void StopSlowMotionImmediate()
		{
			_cts?.Cancel();
			_cts?.Dispose();
			_cts = null;

			Time.timeScale = 1f;
		}

		private static void StopSlowMotionOnPause(bool isPaused)
		{
			if (isPaused)
				StopSlowMotionImmediate();
		}

		private static async UniTaskVoid StartSlowMotionAsync(float scale, float duration, float fadeTime)
		{
			IDisposable pauseObserver = Runtime.ObservePause().Subscribe(StopSlowMotionOnPause);

			Sequence sequence = DOTween.Sequence()
				.Append(DOVirtual.Float(Time.timeScale, scale, fadeTime, SetTimeScale))
				.AppendInterval(duration)
				.Append(DOVirtual.Float(scale, 1f, fadeTime, SetTimeScale))
				.SetUpdate(true)
				.Play();

			try
			{
				await sequence.ToUniTask(TweenCancelBehaviour.CompleteAndCancelAwait, _cts.Token).SuppressCancellationThrow();
			}
			finally
			{
				pauseObserver.Dispose();
			}
		}

		private static void SetTimeScale(float value)
		{
			Time.timeScale = value;
		}
	}
}
