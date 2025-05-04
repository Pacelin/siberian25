using DG.Tweening;
using TSS.Tweening;
using UnityEngine;

public class DissolvePortal : ScriptableTweenEventHandler
{
	private static readonly int DISSOLVE_KEY = Shader.PropertyToID("_Dissolve_Factor");

	[SerializeField] private float _dissolveTime;
	[SerializeField] private Material _material;

	private Tween _dissolveTween;

	private bool _dissolved = true;

	private void Awake()
	{
		_material.SetFloat(DISSOLVE_KEY, 1f);
	}

	public override void OnTrigger()
	{
		Dissolve(_dissolved ? 0f : 1f);
		_dissolved = !_dissolved;
	}

	private void Dissolve(float to)
	{
		_dissolveTween?.Kill(false);
		_dissolveTween = _material.DOFloat(to, DISSOLVE_KEY, _dissolveTime).Play();
	}
}
