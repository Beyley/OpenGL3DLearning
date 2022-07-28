using OpenGL3DLearning.Renderer;
using Silk.NET.OpenGL;

namespace OpenGL3DLearning; 

public static partial class GameWindow {
	private static VertexArrayObject  VAO;
	private static VertexBufferObject VertexBuffer;

	private static float[] vertices = {
		-0.5f, -0.5f, 0.0f,
		0.5f, -0.5f, 0.0f,
		0.0f,  0.5f, 0.0f
	};  
	
	private static void WindowOnLoad() {
		gl = Window.CreateOpenGL();

		VAO          = new VertexArrayObject();
		VertexBuffer = new VertexBufferObject((uint)(sizeof(float) * vertices.Length));
		
		VertexBuffer.SetData(vertices);
	}
	
	private static void WindowOnClosing() {
		
	}
	
	private static void WindowOnUpdate(double obj) {
		
	}
	
	private static void WindowOnDraw(double obj) {
		VAO.Bind();
		
	}
}
