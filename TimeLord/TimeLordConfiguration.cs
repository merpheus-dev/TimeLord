using System;
using UnityEngine;
[CreateAssetMenu]
public class TimeLordConfiguration : ScriptableObject
{
    public bool AutoRecord;
    public float FrameLimit = -1; //Infinite
}
