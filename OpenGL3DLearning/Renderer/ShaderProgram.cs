using System.Diagnostics;
using Kettu;
using Silk.NET.OpenGL;
using static OpenGL3DLearning.GameWindow;

namespace OpenGL3DLearning.Renderer; 

public class ShaderProgram {
	public readonly uint Id;

	public ShaderProgram(string vertexSource, string fragmentSource) {
		this.Id = gl.CreateProgram();

		//Create shaders
		uint vertId = gl.CreateShader(ShaderType.VertexShader);
		uint fragId = gl.CreateShader(ShaderType.FragmentShader);
		
		//Set the source code of the shaders
		gl.ShaderSource(vertId, vertexSource);
		gl.ShaderSource(fragId, fragmentSource);
		
		//Compile the shaders
		gl.CompileShader(vertId);
		gl.CompileShader(fragId);
		
		CheckShaderCompileStatus(vertId);
		CheckShaderCompileStatus(fragId);
		
		//Attach to program and link
		gl.AttachShader(this.Id, vertId);
		gl.AttachShader(this.Id, fragId);
		gl.LinkProgram(this.Id);
		
		gl.GetProgram(this.Id, ProgramPropertyARB.LinkStatus, out int linkStatus);
		if (linkStatus == 0)
			throw new Exception($"Failed to link shader! log:{gl.GetProgramInfoLog(this.Id)}");
		
		//Delete shaders, as they are now part of the program
		gl.DeleteShader(vertId);
		gl.DeleteShader(fragId);
	}

	private static void CheckShaderCompileStatus(uint shader) {
		gl.GetShader(shader, ShaderParameterName.CompileStatus, out int compileStatus);

		if (compileStatus == 0) {
			throw new Exception($"Failed to compile shader! log:{gl.GetShaderInfoLog(shader)}");
		}
	}
	
	internal static uint LastBound => (uint)gl.GetInteger((GLEnum)GetPName.CurrentProgram);

	[Conditional("DEBUG")]
	private void CheckForDoubleBind() {
		if(LastBound == this.Id)
			Logger.Log("Double bind of Shader Program found!");
	}
	
	[Conditional("DEBUG")]
	private void CheckIfBound() {
		if (LastBound != this.Id)
			throw new InvalidOperationException("The object must be bound to be able to call!");
	}

	[Conditional("DEBUG")]
	private static void CheckForRedundantUnbind() {
		if(LastBound == 0)
			Logger.Log("Double unbind of Shader Program found!");
	}

	public void Bind() {
		this.CheckForDoubleBind();
		
		gl.UseProgram(this.Id);
	}

	public void Unbind() {
		UnbindGlobal();
	}
	
	public static void UnbindGlobal() {
		CheckForRedundantUnbind();
		
		gl.UseProgram(0);
	}
}
