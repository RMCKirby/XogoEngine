using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Primitives;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Graphics.Primitives
{
    internal sealed class LineRenderer
    {
        private readonly IDrawAdapter adapter;
        private readonly IVertexArrayObject vao;
        private readonly IVertexBuffer<VertexPositionColour> vbo;

        private HashSet<Line> submittedLines = new HashSet<Line>();

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
    }
}
