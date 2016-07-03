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

        private HashSet<Line> submittedLines;

        public LineRenderer(IDrawAdapter adapter, IVertexArrayObject vao, IVertexBuffer<VertexPositionColour> vbo)
        {
            adapter.ThrowIfNull(nameof(adapter));
            vao.ThrowIfNull(nameof(vao));
            vbo.ThrowIfNull(nameof(vbo));
        }

        public void Draw(Line line)
        {

        }
    }
}
