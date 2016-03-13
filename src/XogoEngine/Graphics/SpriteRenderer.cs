using System;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Graphics
{
    internal class SpriteRenderer
    {
        public SpriteRenderer(
            IShaderProgram shaderProgram,
            ITexture texture,
            IVertexArrayObject vao,
            IVertexBuffer<VertexPositionColourTexture> vbo,
            IElementBuffer<ushort> ebo)
        {
            shaderProgram.ThrowIfNull(nameof(shaderProgram));
            texture.ThrowIfNull(nameof(texture));
            vao.ThrowIfNull(nameof(vao));
            vbo.ThrowIfNull(nameof(vbo));
            ebo.ThrowIfNull(nameof(ebo));
        }
    }
}
