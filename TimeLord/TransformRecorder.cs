using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subtegral.TimeLord.Recorders
{
    public class TransformRecorder : Recorder<Transform, Pose>
    {
        public Stack<Pose> PoseData = new Stack<Pose>();

        public override void Record()
        {
            PoseData.Push(new Pose(Target.position, Target.rotation));
        }

        public override Pose Rewind()
        {
            return PoseData.Pop();
        }
        public override void ClearRecordings()
        {
            PoseData.Clear();
        }
    }

}