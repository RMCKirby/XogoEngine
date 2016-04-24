using OpenTK.Audio.OpenAL;
using System.Diagnostics.CodeAnalysis;

namespace XogoEngine.Audio.Adapters
{
    [ExcludeFromCodeCoverage]
    internal sealed class OpenAlAdapter : IOpenAlAdapter
    {
        public int GenBuffer() => AL.GenBuffer();
        public int GenSource() => AL.GenSource();

        public void SourcePlay(int sourceHandle) => AL.SourcePlay(sourceHandle);
        public void SourcePause(int sourceHandle) => AL.SourcePause(sourceHandle);
        public void SourceStop(int sourceHandle) => AL.SourceStop(sourceHandle);

        public float GetGain(int sourceHandle)
        {
            float gain;
            AL.GetSource(sourceHandle, ALSourcef.Gain, out gain);
            return gain;
        }

        public ALSourceState GetSourceState(int sourceHandle) => AL.GetSourceState(sourceHandle);
        public void Source(int sourceHandle, ALSourcef param, float @value) => AL.Source(sourceHandle, param, @value);

        public void DeleteBuffer(int handle) => AL.DeleteBuffer(handle);
        public void DeleteSource(int handle) => AL.DeleteSource(handle);
    }
}
