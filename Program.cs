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
            (int, int) START_POSITION = (500, 700);
            int SHIP_WIDTH = 40;
            int SHIP_LENGTH = 50;
            string SCREEN_TITLE = "Asteroids";
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

            // Create the mothership
            Mothership mothership = new Mothership("./asteroid/assets/mother_ship.png", W_SIZE.Item1, (int) (W_SIZE.Item1/ 5.7), //path, width, height
                                                    W_SIZE.Item1/2, W_SIZE.Item2 - (int)(W_SIZE.Item1/ 5.7)/2,   // x and y
                                                    0, 0,  // vx and vy
                                                    0, 0,  // rotation and rotational velocity
                                                    (int)(W_SIZE.Item1 / 5.7)/2 - 10, 20,  //healthBarYOffset and healthBarHeight
                                                    50, true); // maxHp and showTextHealth
            
            // Create the player
            Ship ship = new Ship("./asteroid/assets/spaceship/spaceship_yellow.png", 70, 50, W_SIZE.Item1/2, mothership.GetTopLeft().Item2 - 40, 0, 0, 180);

            // Scale the background to have the same dimensions as the Window,
            // then position it at the center of the screen
            Background backgroundImage = new Background("./asteroid/assets/space.png", W_SIZE.Item1, W_SIZE.Item2, W_SIZE.Item1/2, W_SIZE.Item2/2);

            // Create the Player Score
            PlayerScore score = new PlayerScore(path:"", score:0);

            // Create the Start Button
            StartGameButton startGameButton = new StartGameButton("./asteroid/assets/others/start_button.png", 305, 113, W_SIZE.Item1/2, W_SIZE.Item2/2);

            // Give actors to cast
            cast.AddActor("background_image", backgroundImage);
            cast.AddActor("ship", ship);
            cast.AddActor("start_button", startGameButton);
            cast.AddActor("mothership", mothership);
            cast.AddActor("score", score);

            // Create the script
            Script script = new Script();

            // Add all input actions
            script.AddAction("input", new HandleQuitAction(1,screenService));
            
            // Add actions that must be added to the script when the game starts:
            Dictionary<string, List<genie.script.Action>> startGameActions = new Dictionary<string, List<genie.script.Action>>();
            startGameActions["input"] = new List<genie.script.Action>();
            startGameActions["update"] = new List<genie.script.Action>();
            startGameActions["output"] = new List<genie.script.Action>();
            startGameActions["input"].Add(new HandleShipMovementAction(2, keyboardService));
            startGameActions["input"].Add(new HandleShootingAction(2, (float)0.15, (0, -10), keyboardService, audioservice));
            startGameActions["update"].Add(new SpawnAsteroidsAction(1, W_SIZE, (float)1.5));

            // Add all input actions
            script.AddAction("input", new HandleStartGameAction(2, mouseService, physicsService, startGameActions));

            // Add all update actions
            script.AddAction("update", new MoveActorsAction(1, physicsService));
            script.AddAction("update", new HandleOffscreenAction(1, W_SIZE));
            script.AddAction("update", new HandleShipAboveMotherShipAction(1, W_SIZE));
            script.AddAction("update", new HandleShipAsteroidsCollisionAction(1, physicsService, audioservice));
            script.AddAction("update", new HandleMothershipAsteroidsCollisionAction(1, physicsService, audioservice));
            script.AddAction("update", new HandleBulletsAsteroidsCollisionAction(1, physicsService, audioservice));

            // Add all output actions
            script.AddAction("output", new PlayBackgroundMusicAction(1, "asteroid/assets/sound/background_music.wav", audioservice));
            script.AddAction("output", new DrawActorsAction(1, screenService));
            script.AddAction("output", new DrawHealthBarAction(1, screenService));
            script.AddAction("output", new DrawScoreAction(1, screenService));
            script.AddAction("output", new UpdateScreenAction(2, screenService));

            // Yo, director, do your thing!
            director.DirectScene(cast, script);
        }
    }
}