using System;

public interface IRecorder
{
    void Record();
    void Rewind();
    void RegisterRecorder();
    void UnregisterRecorder();
    void ClearRecordings();
}
