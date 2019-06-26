using System;
using UnityEngine;
using Subtegral.TimeLord.Core;
public class RewindListener : MonoBehaviour
{
    private bool rewindstarted = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !rewindstarted)
        {
            rewindstarted = true;
            TimeLordTracker.Instance.StartRewind();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            rewindstarted = false;
            TimeLordTracker.Instance.StopRewind();
            TimeLordTracker.Instance.StartRecordingManually();
        }
        else if (Input.GetKey(KeyCode.P))
        {
            TimeLordTracker.Instance.PauseTime();
        }

    }
}
