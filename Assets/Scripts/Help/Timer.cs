using System;
using System.Collections;
using UnityEngine;

namespace Help
{
    public class Timer : MonoBehaviour
    {
        private Action _onFinish;
        private Action<int> _timerChanged;
        private Coroutine _timerCoroutine;

        public void InitTimer(int timeLeft, Action<int> timerChanged, Action onFinish)
        {
            this._onFinish = onFinish;
            this._timerChanged = timerChanged;

            if (this._timerCoroutine != null)
            {
                StopCoroutine(this._timerCoroutine);
            }

            this._timerCoroutine = StartCoroutine(TimerCoroutine(timeLeft));
        }

        public void Stop()
        {
            if (this._timerCoroutine != null)
            {
                StopCoroutine(this._timerCoroutine);
            }
        }

        private IEnumerator TimerCoroutine(int time)
        {
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                this._timerChanged.Invoke(time);
            }

            this._onFinish.Invoke();
        }
    }
}