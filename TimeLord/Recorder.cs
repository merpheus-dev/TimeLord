using UnityEngine;
namespace Subtegral.TimeLord.Recorders
{
    using Subtegral.TimeLord.Core;
    public abstract class Recorder<T, K> : MonoBehaviour, IRecorder
    {
        public T Target;
        public abstract K Rewind();
        public abstract void Record();

        public abstract void ClearRecordings();

        private void OnEnable()
        {
            RegisterRecorder();
        }

        private void OnDisable()
        {
            UnregisterRecorder();
        }

        public void RegisterRecorder()
        {
            TimeLord.Instance.RegisterRecorder(this);
        }

        public void UnregisterRecorder()
        {
            TimeLord.Instance.UnregisterRecorder(this);
        }
    }
}