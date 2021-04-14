using OpenTK.Graphics.ES30;
using OpenTK.Graphics.GL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class Game : GameWindow
    {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
             0.0f,  0.5f, 0.0f  //Top vertex
        };
        Shader shader;
        int VertexBufferObject;
        int VertexArrayObjext;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            shader = new Shader("shader.vert", "shader.frag");
            GL.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);
            VertexBufferObject = GL.GenBuffer();
            VertexArrayObjext = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObjext);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,false, 3 * sizeof(float),0);            
            GL.EnableVertexAttribArray(0) ;
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            KeyboardState input = KeyboardState.GetSnapshot();
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObjext);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);


            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(VertexArrayObjext);
            shader.Dispose();
            base.OnUnload();
        }
    }
}
