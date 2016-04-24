using OpenTK.Audio.OpenAL;

namespace XogoEngine.Audio.Adapters
{
    internal interface IOpenAlAdapter
    {
        int GenBuffer();
        int GenSource();

        void SourcePlay(int sourceHandle);
        void SourcePause(int sourceHandle);
        void SourceStop(int sourceHandle);
        float GetGain(int sourceHandle);

        ALSourceState GetSourceState(int sourceHandle);

        void Source(int sourceHandle, ALSourcef param, float @value);

        void DeleteBuffer(int handle);
        void DeleteSource(int handle);
    }
}
