using Raylib_cs;

using genie;
using genie.cast;
using genie.script;
using genie.test;
using genie.services;
using genie.services.raylib;

using asteroid.script;
using asteroid.cast;

namespace asteroid
{
    public static class Program
    {
        public static void Test() {
            // MouseMap mouseMap = new MouseMap();
            // mouseMap.getRaylibMouse(-1);

            // CastScriptTest castScriptTest = new CastScriptTest();
            // castScriptTest.testCast();
            // castScriptTest.testScript();

            ServicesTest servicesTest = new ServicesTest();
            servicesTest.TestScreenService();

            // Director director = new Director();
            // director.DirectScene();
        }

        public static void Main(string[] args)
        {
            // A few game constants
            (int, int) W_SIZE = (500, 700);
            (int, int) START_POSITION_PLAYER_1 = (200, 200); // Top left (ish)
            (int, int) START_POSITION_PLAYER_2 = (500, 700);
            int TANK_WIDTH = 40;
            int TANK_LENGTH = 50;
            string SCREEN_TITLE = "Murder Tank Kill";
            int FPS = 120;
            
            // Initiate all services
            RaylibKeyboardService keyboardService = new RaylibKeyboardService();
            RaylibPhysicsService physicsService = new RaylibPhysicsService();
            RaylibScreenService screenService = new RaylibScreenService(W_SIZE, SCREEN_TITLE, FPS);
            RaylibAudioService audioservice = new RaylibAudioService();
            RaylibMouseService mouseService = new RaylibMouseService();

            // Create the director
            Director director = new Director();

            // Create the cast
            Cast cast = new Cast();
            
            // Create the player
            //Ship ship = new Ship("./asteroid/assets/spaceship/spaceship_yellow.png", 70, 50, W_SIZE.Item1/2, mothership.GetTopLeft().Item2 - 40, 0, 0, 180);

            Tank tank1 = new Tank();
            Tank tank2 = new Tank();

            // Give actors to cast
            //cast.AddActor("ship", ship);
            cast.AddActor("tank1", tank1);
            cast.AddActor("tank2", tank2);


            // Create the script
            Script script = new Script();
            
            // Add actions that must be added to the script when the game starts:
            Dictionary<string, List<genie.script.Action>> startGameActions = new Dictionary<string, List<genie.script.Action>>();
            startGameActions["input"] = new List<genie.script.Action>();
            startGameActions["update"] = new List<genie.script.Action>();
            startGameActions["output"] = new List<genie.script.Action>();
            startGameActions["input"].Add(new HandleTankMovementAction(2, keyboardService));
            startGameActions["input"].Add(new HandleShootingAction(2, (float)0.15, (0, -10), keyboardService, audioservice));

            // Add all input actions
            script.AddAction("input", new HandleStartGameAction(2, mouseService, physicsService, startGameActions));
            //script.AddAction("input", new HandleQuitAction(1,screenService));

            // Add all update actions
            script.AddAction("update", new MoveActorsAction(1, physicsService));
            script.AddAction("update", new HandleTankAsteroidsCollisionAction(1, physicsService, audioservice));
            script.AddAction("update", new HandleBulletsAsteroidsCollisionAction(1, physicsService, audioservice));

            // Add all output actions
            script.AddAction("output", new DrawActorsAction(1, screenService));
            script.AddAction("output", new UpdateScreenAction(2, screenService));

            // Yo, director, do your thing!
            director.DirectScene(cast, script);
        }
    }
}