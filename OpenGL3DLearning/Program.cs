using Kettu;

namespace OpenGL3DLearning;

public static class Program {
	public static void Main(string[] args) {
		Logger.StartLogging();
		Logger.AddLogger(new ConsoleLogger());

		GameWindow.Initialize();

		GameWindow.Run();
	}
}