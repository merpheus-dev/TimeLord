using System;
using UnityEngine;
using Subtegral.TimeLord.Recorders;
using UnityEngine.Events;
namespace Subtegral.TimeLord.Core
{
    public class TimeLord : MonoBehaviour
    {
        #region Singleton
        public static TimeLord Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                throw new MultipleExistanceException("Multiple timelords detected, self-destructing.");
            }
        }
        #endregion
        private Action OnRecord;

        private bool isRecording = true;

        public void Update()
        {
            if (isRecording)
            {
                OnRecord?.Invoke();
            }
        }

        #region Exposed Functions
        public void RegisterRecorder(IRecorder recorder)
        {
            OnRecord += recorder.Record;
        }

        public void UnregisterRecorder(IRecorder recorder)
        {
            OnRecord -= recorder.Record;
        }
        #endregion
    }
}