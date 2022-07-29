using System.Diagnostics;
using Kettu;
using Silk.NET.OpenGL;
using static OpenGL3DLearning.GameWindow;

namespace OpenGL3DLearning.Renderer; 

public class VertexArray : IDisposable {
	public readonly uint Id;

	public VertexArray() {
		this.Id = gl.CreateVertexArray();
	}

	internal static uint LastBound => (uint)gl.GetInteger((GLEnum)GetPName.VertexArrayBinding);

	[Conditional("DEBUG")]
	private void CheckForDoubleBind() {
		if(LastBound == this.Id)
			Logger.Log("Double bind of VAO found!");
	}
	
	[Conditional("DEBUG")]
	private void CheckIfBound() {
		if (LastBound != this.Id)
			throw new InvalidOperationException("The object must be bound to be able to call!");
	}

	[Conditional("DEBUG")]
	private static void CheckForRedundantUnbind() {
		if(LastBound == 0)
			Logger.Log("Double unbind of VAO found!");
	}
	
	public void Bind() {
		this.CheckForDoubleBind();
		
		gl.BindVertexArray(this.Id);
	}

	public void Unbind() {
		UnbindGlobal();
	}
	
	public static void UnbindGlobal() {
		CheckForRedundantUnbind();
		
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
