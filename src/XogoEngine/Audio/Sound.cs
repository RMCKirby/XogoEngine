using System;
using OpenTK.Audio.OpenAL;
using XogoEngine.OpenAL.Adapters;

namespace XogoEngine.Audio
{
    public sealed class Sound : IDisposable
    {
        private int sourceHandle;
        private int bufferHandle;
        private readonly IOpenAlAdapter adapter;
        private bool isDisposed = false;

        internal Sound(IOpenAlAdapter adapter, int sourceHandle, int bufferHandle)
        {
            this.adapter = adapter;
            this.sourceHandle = sourceHandle;
            this.bufferHandle = bufferHandle;
        }

        internal int SourceHandle => sourceHandle;
        internal int BufferHandle => bufferHandle;

        public bool IsDisposed => isDisposed;

        public float Gain
        {
            get {
                ThrowIfDisposed();
                return adapter.GetGain(sourceHandle);
            }
            set {
                ThrowIfDisposed();
                adapter.Source(sourceHandle, ALSourcef.Gain, value);
            }
        }

        public bool Playing
        {
            get {
                ThrowIfDisposed();
                return adapter.GetSourceState(sourceHandle) == ALSourceState.Playing;
            }
        }

        public bool Paused
        {
            get {
                ThrowIfDisposed();
                return adapter.GetSourceState(sourceHandle) == ALSourceState.Paused;
            }
        }

        public bool Stopped
        {
            get {
                ThrowIfDisposed();
                return adapter.GetSourceState(sourceHandle) == ALSourceState.Stopped;
            }
        }

        public void Play()
        {
            ThrowIfDisposed();
            adapter.SourcePlay(sourceHandle);
        }

        public void Pause()
        {
            ThrowIfDisposed();
            adapter.SourcePause(sourceHandle);
        }

        public void Stop()
        {
            ThrowIfDisposed();
            adapter.SourceStop(sourceHandle);
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteSource(sourceHandle);
            adapter.DeleteBuffer(BufferHandle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
