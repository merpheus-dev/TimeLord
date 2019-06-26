using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Subtegral.TimeLord.Recorders
{
    using Core;
    [RequireComponent(typeof(AudioSource))]
    public class AudioRecorder : Recorder<AudioSource, AudioClip>
    {

        public override void ClearRecordings()
        {
           // throw new System.NotImplementedException();
        }

        public override void Record()
        {
            Target.pitch = 1f;
           // throw new System.NotImplementedException();
        }

        public override void RegisterTarget()
        {
            Target = gameObject.GetComponent<AudioSource>();
        }

        public override void Rewind()
        {
            Target.pitch = -1;
        }

        protected override AudioClip RewindInternal()
        {
            return null;
            //throw new System.NotImplementedException();
        }
    }

}