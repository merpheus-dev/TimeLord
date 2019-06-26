using System;
namespace Subtegral.TimeLord.Core
{

    public class TimeLord
    {
        private static float internalDeltaTime = 0.02f;
#pragma warning disable //For the sake of fitting Unity's convention
        public static float deltaTime
        {
            get => internalDeltaTime;
            internal set => internalDeltaTime = value;
        }
    }

}