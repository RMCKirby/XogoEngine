using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.Graphics
{
    internal static class ShaderProgramFactory
    {
        public static IShaderProgram Get(string name)
        {
            name = name.ToLower();
            Debug.Assert(Programs.ContainsKey(name));
            return Programs[name]();
        }

        private static readonly Dictionary<string, Func<IShaderProgram>> Programs = new Dictionary<string, Func<IShaderProgram>>()
        {
            ["sprite"] = SpriteProgram
        };

        private static IShaderProgram SpriteProgram()
        {
            var vertexShader = new Shader(EngineCore.GlAdapter, ShaderType.VertexShader);
            var fragmentShader = new Shader(EngineCore.GlAdapter, ShaderType.FragmentShader);
            vertexShader.Load(Shaders.VertexShaders.Sprite);
            fragmentShader.Load(Shaders.FragmentShaders.Sprite);

            var program = new ShaderProgram(
                EngineCore.GlAdapter,
                vertexShader,
                fragmentShader
            );

            program.Link();
            program.DetachShaders();
            program.DeleteShaders();

            return program;
        }
    }
}
