using static OpenGL3DLearning.GameWindow;

namespace OpenGL3DLearning.Renderer; 

public class VertexArrayObject : IDisposable {
	public readonly uint Id;

	public VertexArrayObject() {
		this.Id = gl.CreateVertexArray();
	}

	public void Bind() {
		gl.BindVertexArray(this.Id);
	}

	public void Unbind() {
		UnbindGlobal();
	}
	
	public static void UnbindGlobal() {
		gl.BindVertexArray(0);
	}

	private bool _isDisposed;
	public void Dispose() {
		if (this._isDisposed)
			return;

		this._isDisposed = true;
		
		gl.DeleteVertexArray(this.Id);
	}
}
