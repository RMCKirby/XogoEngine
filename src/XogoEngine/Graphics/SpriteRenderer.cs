using System;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Graphics
{
    internal class SpriteRenderer
    {
        private readonly IShaderProgram shaderProgram;
        private readonly ITexture texture;
        private readonly IVertexArrayObject vao;
        private readonly IVertexBuffer<VertexPositionColourTexture> vbo;
        private readonly IElementBuffer<ushort> ebo;
        private readonly IDrawAdapter adapter;

        public SpriteRenderer(
            IShaderProgram shaderProgram,
            ITexture texture,
            IVertexArrayObject vao,
            IVertexBuffer<VertexPositionColourTexture> vbo,
            IElementBuffer<ushort> ebo,
            IDrawAdapter adapter)
        {
            shaderProgram.ThrowIfNull(nameof(shaderProgram));
            texture.ThrowIfNull(nameof(texture));
            vao.ThrowIfNull(nameof(vao));
            vbo.ThrowIfNull(nameof(vbo));
            ebo.ThrowIfNull(nameof(ebo));
            adapter.ThrowIfNull(nameof(adapter));

            this.shaderProgram = shaderProgram;
            this.texture = texture;
            this.vao = vao;
            this.vbo = vbo;
            this.ebo = ebo;
            this.adapter = adapter;
        }

        public IShaderProgram ShaderProgram => shaderProgram;
        public ITexture Texture => texture;
        public IVertexArrayObject Vao => vao;
        public IVertexBuffer<VertexPositionColourTexture> Vbo => vbo;
        public IElementBuffer<ushort> Ebo => ebo;
        public IDrawAdapter Adapter => adapter;

        public void Render(int indiceCount)
        {
            shaderProgram.Use();
            texture.Bind();
            vao.Bind();
            adapter.DrawElements(BeginMode.Triangles, indiceCount, DrawElementsType.UnsignedShort, 0);
        }
    }
}
