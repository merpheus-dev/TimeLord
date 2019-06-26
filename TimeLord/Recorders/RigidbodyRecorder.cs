using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subtegral.TimeLord.Recorders
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyRecorder : Recorder<Rigidbody, RigidbodyContainer>
    {
        public Stack<RigidbodyContainer> RigidbodySnapshot = new Stack<RigidbodyContainer>();
        private RigidbodyContainer containerCache;
        public override void RegisterTarget()
        {
            Target = gameObject.GetComponent<Rigidbody>();
        }


        public override void Record()
        {
            RigidbodySnapshot.Push(new RigidbodyContainer(Target));
        }


        public override void Rewind()
        {
            containerCache = RewindInternal();
            Target.position = containerCache.Pose.position;
            Target.rotation = containerCache.Pose.rotation;
            Target.velocity = containerCache.Velocity;
        }

        protected override RigidbodyContainer RewindInternal()
        {
            return RigidbodySnapshot.Pop();
        }
        public override void ClearRecordings()
        {
            RigidbodySnapshot.Clear();
        }
    }

    public struct RigidbodyContainer
    {
        public Pose Pose;
        public Vector3 Velocity;
        public RigidbodyContainer(Rigidbody rigidbody)
        {
            Pose.position = rigidbody.position;
            Pose.rotation = rigidbody.rotation;
            Velocity = rigidbody.velocity;
        }
    }
}