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
            (int, int) W_SIZE = (900, 700);
            int TANK_WIDTH = 40;
            int TANK_LENGTH = 50;
            int TURRET_WIDTH = 10;
            int TURRET_LENGTH = 40;
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

            //"" - Needs to point to an image to rotate something.
            Tank tank1 = new Tank("./Game/Asset/Tank.png", TANK_WIDTH, TANK_LENGTH, 100, 100, 0, 0, 0, 0);
            Tank tank2 = new Tank("./Game/Asset/Tank.png", TANK_WIDTH, TANK_LENGTH, 800, 600, 0, 0, 0, 0);
            Turret turret1 = new Turret("./Game/Asset/Turret1.png", TURRET_WIDTH, TURRET_LENGTH, 100, 100, 0, 0, 0, 5);
            Turret turret2 = new Turret("./Game/Asset/Turret1.png", TURRET_WIDTH, TURRET_LENGTH, 800, 600, 0, 0, 0, 5);
            // Orginal Turret Locations  - (## + (TANK_LENGTH / 2))

            //Start Button
            StartGameButton startGameButton = new StartGameButton("./asteroid/assets/others/start_button.png", 305, 113, W_SIZE.Item1/2, W_SIZE.Item2/2);
            cast.AddActor("start_button", startGameButton);

            // Give actors to cast
            //cast.AddActor("ship", ship);
            cast.AddActor("tank1", tank1);
            cast.AddActor("tank2", tank2);
            cast.AddActor("turret1", turret1);
            cast.AddActor("turret2", turret2);

            // Create the script
            Script script = new Script();
            
            // Add actions that must be added to the script when the game starts:
            Dictionary<string, List<genie.script.Action>> startGameActions = new Dictionary<string, List<genie.script.Action>>();
            startGameActions["input"] = new List<genie.script.Action>();
            startGameActions["update"] = new List<genie.script.Action>();
            startGameActions["output"] = new List<genie.script.Action>();

            startGameActions["input"].Add(new HandleTankMovementAction(2, keyboardService));
            //Change (0, -10) to the set direction of the rotated turrets---------------------*************
            //They are currently only set to go up(-10).
            //x = -cos(Math.pi / 180.00 * angle), y = sin(Math.pi / 180.00 * angle)
            startGameActions["input"].Add(new HandleShootingAction(2, (float)0.15, (0, -10), keyboardService, audioservice));

            // Add all input actions
            script.AddAction("input", new HandleStartGameAction(2, mouseService, physicsService, startGameActions));
            script.AddAction("input", new HandleQuitAction(1,screenService));

            // Add all update actions
            script.AddAction("update", new MoveActorsAction(1, physicsService));
            //script.AddAction("update", new HandleTankAsteroidsCollisionAction(1, physicsService, audioservice));
            //script.AddAction("update", new HandleBulletsAsteroidsCollisionAction(1, physicsService, audioservice));

            // Add all output actions
            script.AddAction("output", new DrawActorsAction(1, screenService));
            script.AddAction("output", new UpdateScreenAction(2, screenService));

            // Yo, director, do your thing!
            director.DirectScene(cast, script);
        }
    }
}