using UnityEngine;
namespace Subtegral.TimeLord.Recorders
{
    using Subtegral.TimeLord.Core;
    public abstract class Recorder<T, K> : MonoBehaviour, IRecorder
    {
        [HideInInspector]
        public T Target;
        public abstract void Record();
        public abstract void ClearRecordings();
        public abstract void Rewind();
        public abstract void RegisterTarget();
        protected abstract K RewindInternal();

        private void Start()
        {
            RegisterTarget();
            RegisterRecorder();
        }

        private void OnDisable()
        {
            UnregisterRecorder();
        }

        public void RegisterRecorder()
        {
            TimeLordTracker.Instance.RegisterRecorder(this);
        }

        public void UnregisterRecorder()
        {
            TimeLordTracker.Instance.UnregisterRecorder(this);
        }
    }
}