using System;
using OpenTK.Audio.OpenAL;
using XogoEngine.Audio.Adapters;

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

        public void Dispose()
        {
            DisposeUnmanaged();
            GC.SuppressFinalize(this);
        }

        ~Sound()
        {
            DisposeUnmanaged();
        }

        private void DisposeUnmanaged()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteSource(sourceHandle);
            adapter.DeleteBuffer(bufferHandle);
            isDisposed = true;
        }
    }
}
