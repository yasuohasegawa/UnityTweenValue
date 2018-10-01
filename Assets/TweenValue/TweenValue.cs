using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tweenvalue
{
    public class TweenValue : MonoBehaviour, ITweenValue
    {
        #region Singleton Constructors
        static TweenValue()
        {
        }

        TweenValue()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static TweenValue Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameObject("TweenValue").AddComponent<TweenValue>();
                    Init();
                }
                return m_instance;
            }
        }
        #endregion

        #region Member Public Variables
        public static List<Tween> m_tweens;
        #endregion

        #region Member Private Variables
        private static TweenValue m_instance = null;
        #endregion


        /// <summary>
        /// Init this instance.
        /// </summary>
        private static void Init()
        {
            m_tweens = new List<Tween>();
        }

        void Update()
        {
            for(int i = 0; i< m_tweens.Count; i++)
            {
                Tween tween = m_tweens[i];
                if (tween != null)
                {
                    tween.UpdateVal();
                }
            }
        }

        /// <summary>
        /// play tween animation
        /// </summary>
        /// <param name="_startVal">start float value</param>
        /// <param name="_endVal">end float value</param>
        /// <param name="_OnUpdate">animation update callback</param>
        /// <param name="_OnComplete">animation complete callback</param>
        /// <param name="_delay">animation delay</param>
        /// <param name="_useTimeScale">if it's true:use timescale</param>
        /// <param name="_easing">easing function</param>
        /// <returns></returns>
        public Tween AddTween(float _startVal, float _endVal, float _duration = 0f, float _delay = 0f, bool _useTimeScale = false, System.Action<float> _OnUpdate = null, System.Action<float> _OnComplete = null, System.Func<float, float, float, float> _easing = null)
        {
            return new Tween().Play(_startVal, _endVal, _duration, _delay, _useTimeScale, _OnUpdate, _OnComplete, _easing);
        }

        /// <summary>
        /// Remove tween
        /// </summary>
        /// <param name="tw"></param>
        public void Remove(Tween tw)
        {
            m_tweens.Remove(tw);
        }

        /// <summary>
        /// Remove all tweens
        /// </summary>
        public void RemoveAll()
        {
            for (int i = 0; i< m_tweens.Count; i++)
            {
                Tween tween = m_tweens[i];
                if (tween != null)
                {
                    tween.CancelAndStop();
                    tween = null;
                }
            }
            m_tweens.Clear();
        }

        /// <summary>
        /// dispose tweens
        /// </summary>
        public void Dispose()
        {
            RemoveAll();
            m_tweens = null;
        }

    }
}