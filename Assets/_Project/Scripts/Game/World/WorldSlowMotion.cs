using System;
using TSS.Audio;
using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class WorldSlowMotion
    {
        private float _slowMotionEstimation;
        private float _targetTimeScale;
        
        private readonly float _slowMoScale;
        private readonly float _timeVelocity;

        public WorldSlowMotion(float slowMoScale, float timeVelocity)
        {
            _slowMoScale = slowMoScale;
            _timeVelocity = timeVelocity;
        }
        
        public void PlaySlowMotion(float time, float scale = 0)
        {
            if (scale == 0)
                _targetTimeScale = _slowMoScale;
            else
                _targetTimeScale = scale;
            _slowMotionEstimation = Time.time + time;
        }

        public void Update()
        {
            if (Runtime.IsPaused)
            {
                Time.timeScale = 1;
                AudioSystem.Global.SetSlowmo(0);
            }
            else
            {
                AudioSystem.Global.SetSlowmo((1 - Time.timeScale) / (1 - _slowMoScale) * 0.03f);
                Time.timeScale = Mathf.MoveTowards(Time.timeScale, _targetTimeScale, _timeVelocity * Time.unscaledDeltaTime);
                if (Math.Abs(_targetTimeScale - 1) > 0.01f && Time.time >= _slowMotionEstimation)
                    _targetTimeScale = 1;
            }
        }
    }
}