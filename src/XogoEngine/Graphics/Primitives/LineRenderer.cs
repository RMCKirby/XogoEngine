using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Primitives;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Graphics.Primitives
{
    internal sealed class LineRenderer : IDisposable
    {
        private readonly IDrawAdapter adapter;
        private readonly IVertexArrayObject vao;
        private readonly IVertexBuffer<VertexPositionColour> vbo;

        private HashSet<Line> submittedLines = new HashSet<Line>();
        private bool isDisposed = false;

        public LineRenderer(IDrawAdapter adapter, IVertexArrayObject vao, IVertexBuffer<VertexPositionColour> vbo)
        {
            adapter.ThrowIfNull(nameof(adapter));
            vao.ThrowIfNull(nameof(vao));
            vbo.ThrowIfNull(nameof(vbo));
            this.adapter = adapter;
            this.vao = vao;
            this.vbo = vbo;
        }

        public int SubmittedLineCount => submittedLines.Count;
        public bool IsDisposed => isDisposed;

        public void Draw(Line line)
        {
            if (submittedLines.Contains(line))
            {
                return;
            }
            submittedLines.Add(line);
        }

        public void Draw()
        {
            vao.Bind();
            adapter.DrawArrays(PrimitiveType.Lines, 0, SubmittedLineCount);
        }

		public void Flush()
		{
			
		}

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            vao?.Dispose();
            vbo?.Dispose();
            isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
    }
}
