namespace XogoEngine.OpenGL.Shaders
{
    public sealed class Shader
    {
        private int handle;

        public Shader()
        {
            this.handle = 1;
        }

        public int Handle { get { return handle; } }
    }
}
