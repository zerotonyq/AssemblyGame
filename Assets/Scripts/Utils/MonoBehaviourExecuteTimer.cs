using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class MonoBehaviourExecuteTimer
    {
        private static float _remainingTime;
        private static float _deltaTime;
        public static void StartExecuteTimer(
            MonoBehaviour executable, 
            float remainingTime,
            float deltaTime,
            Action action,
            Action doneAction)
        {
            _remainingTime = remainingTime;
            _deltaTime = deltaTime;
            executable.StartCoroutine(TimerCoroutine(action, doneAction));
        }

        private static IEnumerator TimerCoroutine(Action action, Action doneAction)
        {
            while (_remainingTime > 0f)
            {
                _remainingTime -= _deltaTime;
                action.Invoke();
                yield return new WaitForSeconds(_deltaTime);
            }
            doneAction.Invoke();
        }
    }
}