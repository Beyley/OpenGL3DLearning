using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace OpenGL3DLearning; 

public static partial class GameWindow {
	public static IWindow Window;
	public static GL      gl;

	public static void Initialize() {
		WindowOptions options = WindowOptions.Default;

		ContextFlags flags = ContextFlags.Default;
		
#if DEBUG
		flags = ContextFlags.Debug;
#endif
		
		options.API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, flags, new APIVersion(3, 3));

		Window = Silk.NET.Windowing.Window.Create(options);

		Window.Load    += WindowOnLoad;
		Window.Update  += WindowOnUpdate;
		Window.Render  += WindowOnDraw;
		Window.Closing += WindowOnClosing;
	}

	public static void Close() {
		Window.Close();
	}
	
	public static void Run() {
		Window.Run();
	}
}
