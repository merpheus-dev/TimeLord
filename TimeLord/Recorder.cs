using UnityEngine;
namespace Subtegral.TimeLord.Recorders
{
    using Subtegral.TimeLord.Core;
    public abstract class Recorder<T, K> : MonoBehaviour, IRecorder
    {
        public T Target;
        public abstract K Rewind();
        public abstract void Record();

        private void Awake()
        {
            RegisterRecorder();
        }

        public void RegisterRecorder()
        {
            TimeLord.Instance.RegisterRecorder(this);
        }
    }
}