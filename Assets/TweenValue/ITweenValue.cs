using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tweenvalue
{
    public interface ITweenValue
    {
        Tween AddTween(float _startVal, float _endVal, float _duration = 0f, float _delay = 0f, bool _useTimeScale = false, System.Action<float> _OnUpdate = null, System.Action<float> _OnComplete = null, System.Func<float, float, float, float> _easing = null);
        void Remove(Tween tw);
        void RemoveAll();
        void Dispose();
    }
}
