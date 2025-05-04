using System;
using Siberian25.Game;
using UnityEngine;

[ExecuteAlways]
public class CameraTargetDamper : MonoBehaviour
{
    [SerializeField]
    private Transform _follow;
    [SerializeField]
    private Transform _center;
    [SerializeField]
    private Transform _target;

    [SerializeField, Range(0f, 1f)]
    private float _dampFactor;

    private void OnEnable()
    {
        if (GameContext.Player)
            _follow = GameContext.Player.transform;
    }

    private void OnDisable()
    {
        _target.position = Vector3.zero;
    }

    [ExecuteAlways]
    private void Update()
    {
        if (_follow == null || _center == null || _target == null) return;

        Vector3 delta = _follow.position - _center.position;
        delta *= _dampFactor;
        _target.position = _center.position + delta;
    }
}
