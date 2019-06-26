using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Subtegral.TimeLord.Recorders
{
    public class TransformRecorder : Recorder<Transform, Pose>
    {
        public Stack<Pose> PoseData = new Stack<Pose>();

        private Pose poseCache;

        public override void RegisterTarget()
        {
            Target = gameObject.transform;
        }
        public override void Record()
        {
            PoseData.Push(new Pose(Target.position, Target.rotation));
        }

        public override void Rewind()
        {
            poseCache = RewindInternal();
            transform.position = poseCache.position;
            transform.rotation = poseCache.rotation;
        }

        protected override Pose RewindInternal()
        {
            return PoseData.Pop();
        }
        public override void ClearRecordings()
        {
            PoseData.Clear();
        }
    }
}