using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class Shader:IDisposable
    {
        int Handle;
        int VertexShader, FragmentShader;
        public Shader(string vertPath, string fragPath)
        {
            string VertexShaderSource;
            using (StreamReader reader = new StreamReader(vertPath, Encoding.UTF8))
                VertexShaderSource = reader.ReadToEnd();

            string FragmentShaderSource;
            using (StreamReader reader = new StreamReader(fragPath, Encoding.UTF8))
                FragmentShaderSource = reader.ReadToEnd();
            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);
            GL.CompileShader(VertexShader);
            var infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);
            
            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);
            GL.CompileShader(FragmentShader);
            var infoLogVFram = GL.GetShaderInfoLog(FragmentShader);
            if (infoLogVFram != System.String.Empty)
                System.Console.WriteLine(infoLogVFram);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);
            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

        }
        public void Use()
        {
            GL.UseProgram(Handle);
        }
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
