namespace TeamBuilder.App
{
    using Core;

    public class Application
    {
        public static void Main()
        {
            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
