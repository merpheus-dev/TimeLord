using System;
using UnityEngine;
using Subtegral.TimeLord.Recorders;
using System.Collections.Generic;
namespace Subtegral.TimeLord.Core
{
    public class TimeLordTracker : MonoBehaviour
    {
        #region Singleton
        public static TimeLordTracker Instance;
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

        private TimeLordConfiguration configuration;

        private HashSet<IRecorder> recorderCache = new HashSet<IRecorder>();

        private Action OnRecord;
        private Action OnRewind;
        private Action OnPause;

        private float recordedFrameCount = 0;
        private bool isRecordingRequested = false;
        private TimeMode timeMode;


        private void Start()
        {
            Configure();
            RegisterFrameCounter();
        }

        private void Configure()
        {
            configuration = Resources.Load<TimeLordConfiguration>("TimeLord");

            if (configuration == null)
            {
                configuration = ScriptableObject.CreateInstance<TimeLordConfiguration>();
            }

            timeMode = configuration.AutoRecord ? TimeMode.Record : TimeMode.Play;
        }

        private void RegisterFrameCounter()
        {
            OnRecord += IncreaseFrameCount;
            OnRewind += DecreaseFrameCount;
            OnRecord += PlugDeltaTime;
            OnRewind += StopDeltaTime;
            OnPause += StopDeltaTime;
        }

        private void IncreaseFrameCount()
        {
            recordedFrameCount++;
        }

        private void DecreaseFrameCount()
        {
            recordedFrameCount--;
        }

        private void StopDeltaTime()
        {
            TimeLord.deltaTime = 0f;
        }

        private void PlugDeltaTime()
        {
            TimeLord.deltaTime = Time.unscaledDeltaTime;
        }

        private void Update()
        {
            if (timeMode == TimeMode.Pause)
            {
                PauseMode();
                return;
            }
            switch (timeMode)
            {
                case TimeMode.Record:
                    RecordMode();
                    break;
                case TimeMode.Rewind:
                    RewindMode();
                    break;
                case TimeMode.Play:
                    PlugDeltaTime();
                    break;
            }
        }

        #region Record Mode
        private void RecordMode()
        {
            if (configuration.AutoRecord || isRecordingRequested)
            {
                if (IsFrameLimited() && IsFrameLimitExceeded())
                    CleanUpRecorders();

                OnRecord?.Invoke();
                Debug.Log("Recording...");
            }
        }

        private bool IsFrameLimited()
        {
            return configuration.FrameLimit != -1;
        }

        private bool IsFrameLimitExceeded()
        {
            return configuration.FrameLimit > recordedFrameCount;
        }

        private void CleanUpRecorders()
        {
            foreach (var recorder in recorderCache)
            {
                recorder.ClearRecordings();
            }
            recordedFrameCount = 0;
        }
        #endregion

        #region Rewind Mode
        private void RewindMode()
        {
            if (recordedFrameCount == 0)
            {
                CleanUpRecorders();
                return;
            }

            foreach (var recorder in recorderCache)
            {
                recorder.Rewind();
            }
            OnRewind?.Invoke();
        }
        #endregion 

        private void PauseMode()
        {
            OnPause?.Invoke();
        }

        #region Exposed Functions

        public void StartRecordingManually()
        {
            // CleanUpRecorders();
            isRecordingRequested = true;
            timeMode = TimeMode.Record;
        }

        public void StopRecording()
        {
            isRecordingRequested = false;
            timeMode = TimeMode.Play;
            CleanUpRecorders();
        }

        public void StartRewind()
        {
            timeMode = TimeMode.Rewind;
        }

        public void StopRewind()
        {
            timeMode = TimeMode.Play;
        }

        public void PauseTime()
        {
            timeMode = TimeMode.Pause;
        }

        public void RegisterRecorder(IRecorder recorder)
        {
            OnRecord += recorder.Record;
            recorderCache.Add(recorder);
        }

        public void UnregisterRecorder(IRecorder recorder)
        {
            OnRecord -= recorder.Record;
            recorderCache.Remove(recorder);
        }
        #endregion
    }
}
