using Kettu;
using OpenGL3DLearning.Renderer;
using Silk.NET.Core.Native;
using Silk.NET.OpenGL;
using VertexArray = OpenGL3DLearning.Renderer.VertexArray;

namespace OpenGL3DLearning;

public static unsafe partial class GameWindow {
	private static VertexArray   VAO;
	private static VertexBuffer  VertexBuffer;
	private static ShaderProgram Program;

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
		-0.5f, -0.5f, 0.0f,
		0.5f, -0.5f, 0.0f,
		0.0f,  0.5f, 0.0f
	};  
	
	private static void WindowOnLoad() {
		gl = Window.CreateOpenGL();
		
		gl.Enable(EnableCap.DebugOutput);
		gl.Enable(EnableCap.DebugOutputSynchronous);
		gl.DebugMessageCallback(DebugCallback, null);

		VAO          = new VertexArray();
		VertexBuffer = new VertexBuffer((uint)(sizeof(float) * vertices.Length));
		Program      = new ShaderProgram(vertSource, fragSource);
		
		VAO.Bind();
		VertexBuffer.Bind();
		VertexBuffer.SetData(vertices);
		
		gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, (void*)0);
		gl.EnableVertexAttribArray(0);
		
		VertexBuffer.Unbind();
		VAO.Unbind();
	}
	
	private static void DebugCallback(GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userparam) {
		string messageStr = SilkMarshal.PtrToString(message);
		
		Logger.Log(messageStr);
	}

	private static void WindowOnClosing() {
		
	}
	
	private static void WindowOnUpdate(double obj) {
		
	}
	
	private static void WindowOnDraw(double obj) {
		gl.ClearColor(0.5f, 0, 0, 0f);
		gl.Clear(ClearBufferMask.ColorBufferBit);
		
		VAO.Bind();
		Program.Bind();
		VertexBuffer.Bind();
		
		gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
		
		VertexBuffer.Unbind();
		Program.Unbind();
		VAO.Unbind();
	}
}
