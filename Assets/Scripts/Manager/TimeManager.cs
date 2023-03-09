using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Utility;

namespace CCB.Gameplay
{
    public enum TimeState
    {
        Normal,
        Accelerate,
        Slow,
        Stop,
    }

    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] TimeState currentTimeState;
        float time = 0;

        protected override void InitAfterAwake()
        {

        }

        void Start()
        {
            OnChangeTimeState(TimeState.Normal);
        }

        public void OnChangeTimeState(TimeState state)
        {
            currentTimeState = state;

            switch(currentTimeState)
            {
                case TimeState.Normal:
                    time = NormalTime();
                    break;
                case TimeState.Accelerate:
                    time = AccelerateTime();
                    break;
                case TimeState.Slow:
                    time = SlowTime();
                    break;
                case TimeState.Stop:
                    time = StopTime();
                    break;
            }
        }

        float NormalTime()
        {
            return 1;
        }

        float AccelerateTime()
        {
            return NormalTime() * 2;
        }

        float SlowTime()
        {
            return NormalTime() * 0.5f;
        }

        float StopTime()
        {
            return 0;
        }

        public float GetTime()
        {
            return time;
        }

        public TimeState GetTimeState()
        {
            return currentTimeState;
        }
    }
}
