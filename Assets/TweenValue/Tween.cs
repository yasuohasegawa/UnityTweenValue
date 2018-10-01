using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tweenvalue
{
    public enum AnimationState
    {
        None,
        Playing,
        Complete
    }

    public class Tween
    {
        #region Member Public Variables
        public System.Func<float, float, float, float> easing = null;

        public float m_time = 0f;
        public float m_delay = 0f;
        #endregion

        #region Member Private Variables
        private float m_startTime = 0f;
        private AnimationState m_state = AnimationState.None;

        private float m_startVal = 0f;
        private float m_endVal = 0f;

        private float m_changeVal = 0f;
        private float m_percentage = 0f;

        private bool m_useTimeScale = false;

        private System.Action<float> OnUpdate = null;
        private System.Action<float> OnComplete = null;
        #endregion

        // Use this for initialization
        public Tween()
        {
            TweenValue.m_tweens.Add(this);
        }

        public Tween Play(float _startVal, float _endVal, float _time = 0f, float _delay = 0f, bool _useTimeScale = false, System.Action<float> _OnUpdate = null, System.Action<float> _OnComplete = null, System.Func<float, float, float, float> _easing = null)
        {
            m_state = AnimationState.Playing;
            m_startVal = _startVal;
            m_endVal = _endVal;
            m_time = _time;
            m_useTimeScale = _useTimeScale;
            OnUpdate = _OnUpdate;
            OnComplete = _OnComplete;
            SetUpParams(_delay, _easing);
            return this;
        }

        private void SetUpParams(float _delay = 0f, System.Func<float, float, float, float> _easing = null)
        {
            m_delay = _delay;
            easing = _easing;
            m_startTime = Time.realtimeSinceStartup;
            m_changeVal = 0f;
            m_percentage = 0f;
            if (m_useTimeScale)
            {
                m_startTime = Time.time;
            }
            m_startTime += m_delay;
        }

        public void CancelAndStop()
        {
            m_state = AnimationState.None;
            TweenValue.Instance.Remove(this);
        }

        public void UpdateVal()
        {
            if (m_state == AnimationState.None || m_state == AnimationState.Complete)
            {
                return;
            }

            float end = Time.realtimeSinceStartup;
            if (m_useTimeScale)
            {
                end = Time.time;
            }

            if (end < m_startTime)
            {
                return;
            }

            float elapsed = end - m_startTime;
            if (elapsed >= m_time)
            {
                // complete
                m_state = AnimationState.Complete;
                if (OnUpdate != null)
                {
                    OnUpdate(m_endVal);
                }

                if (OnComplete != null)
                {
                    OnComplete(m_endVal);
                }

                CancelAndStop();
            }
            else
            {
                m_percentage = elapsed / m_time;
                
                // update value by easing equations
                if (easing != null)
                {
                    m_changeVal = easing(m_startVal, m_endVal, m_percentage);
                }
                else
                {
                    m_changeVal = Easing.linear(m_startVal, m_endVal, m_percentage);

                }
                
                if (OnUpdate != null)
                {
                    OnUpdate(m_changeVal);
                }
            }
        }

        public AnimationState GetState()
        {
            return m_state;
        }
    }
}
