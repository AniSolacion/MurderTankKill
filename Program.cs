// See https://aka.ms/new-console-template for more information
using Murdertankkill.Game.Casting;
using Murdertankkill.Game.Directing;
using Murdertankkill.Game.Scripting;
using Murdertankkill.Game.Services;
using Murdertankkill.Game;


namespace Murdertankkill
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();
            cast.AddActor("tank", new Bike(Constants.TANKSTART, Constants.RED));
            cast.AddActor("tank2", new Bike(Constants.TANK2START, Constants.YELLOW));
            cast.AddActor("score", new Score());

            // create the services
            KeyboardService keyboardService = new KeyboardService();
            VideoService videoService = new VideoService(false);
           
            // create the script
            Script script = new Script();
            script.AddAction("input", new ControlActorsAction(keyboardService));
            script.AddAction("update", new MoveActorsAction());
            script.AddAction("update", new HandleCollisionsAction());
            script.AddAction("output", new DrawActorsAction(videoService));

            // start the game
            Director director = new Director(videoService);
            director.StartGame(cast, script);
        }
    }
}

