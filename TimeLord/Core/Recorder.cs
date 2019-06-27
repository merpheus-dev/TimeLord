using UnityEngine;
namespace Subtegral.TimeLord.Recorders
{
    using Subtegral.TimeLord.Core;
    public abstract class Recorder<TComponent, TContainer> : MonoBehaviour, IRecorder
    {
        [HideInInspector]
        public TComponent Target;
        public abstract void Record();
        public abstract void ClearRecordings();
        public abstract void Rewind();
        public abstract void RegisterTarget();
        protected abstract TContainer RewindInternal();
        public virtual void Pause() { }
        public virtual void UnPause() { }

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