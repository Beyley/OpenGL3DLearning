using System.Diagnostics;
using Kettu;
using Silk.NET.OpenGL;
using static OpenGL3DLearning.GameWindow;

namespace OpenGL3DLearning.Renderer; 

public unsafe class VertexBuffer : IDisposable {
	public readonly uint Id;
	public readonly uint Size;

	public VertexBuffer(uint size, BufferUsageARB usage = BufferUsageARB.StaticDraw) {
		this.Id   = gl.CreateBuffer();
		this.Size = size;
		
		this.Bind();
		
		//Fill the buffer with null to allocate the size
		gl.BufferData(BufferTargetARB.ArrayBuffer, size, null, usage);
		
		this.Unbind();
	}

	internal static uint LastBound => (uint)gl.GetInteger((GLEnum)GetPName.ArrayBufferBinding);

	[Conditional("DEBUG")]
	private void CheckForDoubleBind() {
		if(LastBound == this.Id)
			Logger.Log("Double bind of VBO found!");
	}
	
	[Conditional("DEBUG")]
	private void CheckIfBound() {
		if (LastBound != this.Id)
			throw new InvalidOperationException("The buffer must be bound to run this function!");
	}

	[Conditional("DEBUG")]
	private static void CheckForRedundantUnbind() {
		if(LastBound == 0)
			Logger.Log("Double unbind of VBO found!");
	}
	
	public void SetData <T>(T[] arr) where T : unmanaged {
		this.CheckIfBound();
		
		if (sizeof(T) * arr.Length > this.Size)
			throw new InvalidOperationException("You cannot set the data of a buffer larger than its original size!");
		
		gl.BufferSubData<T>(BufferTargetARB.ArrayBuffer, 0, arr);
	}

	public void Bind() {
		this.CheckForDoubleBind();
		
		gl.BindBuffer(BufferTargetARB.ArrayBuffer, this.Id);
	}

	public void Unbind() {
		UnbindGlobal();
	}

	public static void UnbindGlobal() {
		CheckForRedundantUnbind();
		
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
