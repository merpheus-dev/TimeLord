using System;

public interface IRecorder
{
    void Record();
    void Rewind();
    void Pause();
    void UnPause();
    void RegisterRecorder();
    void UnregisterRecorder();
    void ClearRecordings();
}
