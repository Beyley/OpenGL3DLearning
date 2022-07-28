using Silk.NET.OpenGL;
using static OpenGL3DLearning.GameWindow;

namespace OpenGL3DLearning.Renderer; 

public unsafe class VertexBufferObject : IDisposable {
	public readonly uint Id;
	public readonly uint Size;

	public VertexBufferObject(uint size, BufferUsageARB usage = BufferUsageARB.StaticDraw) {
		this.Id   = gl.CreateBuffer();
		this.Size = size;
		
		this.Bind();
		
		//Fill the buffer with null to allocate the size
		gl.BufferData(BufferTargetARB.ArrayBuffer, size, null, usage);
		
		this.Unbind();
	}

	public void SetData <T>(T[] arr) where T : unmanaged {
		if (sizeof(T) * arr.Length > this.Size)
			throw new InvalidOperationException("You cannot set the data of a buffer larger than its original size!");
		
		gl.BufferSubData<T>(BufferTargetARB.ArrayBuffer, 0, arr);
	}

	public void Bind() {
		gl.BindBuffer(BufferTargetARB.ArrayBuffer, this.Id);
	}

	public void Unbind() {
		UnbindGlobal();
	}

	public static void UnbindGlobal() {
		gl.BindBuffer(GLEnum.ArrayBuffer, 0);
	}

	private bool _isDisposed;
	public void Dispose() {
		if (this._isDisposed)
			return;

		this._isDisposed = true;
		
		gl.DeleteBuffer(this.Id);
	}
}
