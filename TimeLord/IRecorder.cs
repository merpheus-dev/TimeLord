using System;

public interface IRecorder
{
    void Record();
    void RegisterRecorder();
    void UnregisterRecorder();
    void ClearRecordings();
}
