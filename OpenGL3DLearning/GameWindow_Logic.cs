using Kettu;
using OpenGL3DLearning.Renderer;
using Silk.NET.Core.Native;
using Silk.NET.OpenGL;
using VertexArray = OpenGL3DLearning.Renderer.VertexArray;

namespace OpenGL3DLearning;

public static unsafe partial class GameWindow {
	private static VertexArray        VAO;
	private static VertexBuffer       VertexBuffer;
	private static ElementArrayBuffer ElementBuffer;
	private static ShaderProgram      Program;

	private static string vertSource = @"#version 330 core
layout (location = 0) in vec3 aPos;

void main()
{
    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
}";

	private static string fragSource = @"#version 330 core
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}";

	private static float[] vertices = {
		0.5f, 0.5f, 0.0f,   // top right
		0.5f, -0.5f, 0.0f,  // bottom right
		-0.5f, -0.5f, 0.0f, // bottom left
		-0.5f, 0.5f, 0.0f   // top left 
	};
	private static ushort[] indices = { // note that we start from 0!
		0, 1, 3,                      // first triangle
		1, 2, 3                       // second triangle
	};

	private static void WindowOnLoad() {
		gl = Window.CreateOpenGL();
		
		gl.Enable(EnableCap.DebugOutput);
		gl.Enable(EnableCap.DebugOutputSynchronous);
		gl.DebugMessageCallback(DebugCallback, null);

		Program       = new ShaderProgram(vertSource, fragSource);
		VAO           = new VertexArray();
		VertexBuffer  = new VertexBuffer((uint)(sizeof(float)        * vertices.Length));
		ElementBuffer = new ElementArrayBuffer((uint)(sizeof(ushort) * indices.Length));
		
		VAO.Bind();
		VertexBuffer.Bind();
		VertexBuffer.SetData(vertices);
		Console.WriteLine(gl.GetError());
		
		ElementBuffer.Bind();
		ElementBuffer.SetData(indices);
		Console.WriteLine(gl.GetError());

		gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, (void*)0);
		gl.EnableVertexAttribArray(0);
		Console.WriteLine(gl.GetError());
	}
	
	private static void DebugCallback(GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userparam) {
		string messageStr = SilkMarshal.PtrToString(message);
		
		Logger.Log($"DebugCallbackMessage: {messageStr}");
		Logger.Update().Wait();
	}

	private static void WindowOnClosing() {
		
	}
	
	private static void WindowOnUpdate(double obj) {
		
	}
	
	private static void WindowOnDraw(double obj) {
		gl.ClearColor(0.5f, 0, 0, 0f);
		gl.Clear(ClearBufferMask.ColorBufferBit);
		Console.WriteLine(gl.GetError());

		Program.Bind();
		VAO.Bind();
		Console.WriteLine(gl.GetError());
		
		gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedShort, 0);
		Console.WriteLine(gl.GetError());
		
		VAO.Unbind();
	}
}
