using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class SpawnAsteroidsAction : genie.script.Action {
        
        private (int x, int y) windowSize;
        private bool timerStarted;
        private bool asteroidSpawn;
        private DateTime lastSpawn;
        private float spawnInterval_ms;
        private Random randomGenerator;

        public SpawnAsteroidsAction(int priority, (int, int) windowSize, float spawnInterval) : base(priority) {
            this.windowSize = windowSize;
            this.timerStarted = false;
            this.asteroidSpawn = false;
            this.lastSpawn = new DateTime();
            this.spawnInterval_ms = spawnInterval * 1000;
            this.randomGenerator = new Random();
        }

        private Asteroid CreateAsteroid(int type, int x, int y) {
            if (type == 1) {
                int velX = x > this.windowSize.x/2 ? -1 : 1;
                return new Asteroid("./asteroid/assets/asteroids/asteroid_large.png",
                                    175, 175,        // Width and height of asteroid
                                    x, y,            // X and Y of asteroid
                                    velX, 3,         // vX and vY of asteroid
                                    0, 1,            // rotation and rotational velocity
                                    93, 5,           // health bar y-offset and heath bar height
                                    5, true,         // maxHP and whether to show health text
                                    5);              // how many points is this asteroid worth?
            }
            else if (type == 2)
            {
                int velX = x > this.windowSize.x / 2 ? -2 : 2;
                return new Asteroid("./asteroid/assets/asteroids/asteroid_med.png",
                                    100, 100,        // Width and height of asteroid
                                    x, y,            // X and Y of asteroid
                                    velX, 6,         // vX and vY of asteroid
                                    0, 1,            // rotation and rotational velocity
                                    55, 5,           // health bar y-offset and heath bar height
                                    3, true,         // maxHP and whether to show health text
                                    3);              // how many points is this asteroid worth?
            }
            else {
                int velX = x > this.windowSize.x / 2 ? -3 : 3;
                return new Asteroid("./asteroid/assets/asteroids/asteroid_small.png",
                                    40, 40,        // Width and height of asteroid
                                    x, y,            // X and Y of asteroid
                                    velX, 8,         // vX and vY of asteroid
                                    0, 1,            // rotation and rotational velocity
                                    25, 5,           // health bar y-offset and heath bar height
                                    1, true,         // maxHP and whether to show health text
                                    1);              // how many points is this asteroid worth?
            }
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            if (!this.timerStarted) {
                this.timerStarted = true;
                this.lastSpawn = DateTime.Now;
            }

            // If the spawn interval has passed, spawn a new asteroid
            if ((DateTime.Now - this.lastSpawn).TotalMilliseconds >= this.spawnInterval_ms) {

                // Randomly select a type from 1 to 3 (excluding the 4)
                // 1 is large. 2 is medium. 3 is small
                int asteroidType = this.randomGenerator.Next(1, 4);

                // The x-range within which the asteroid should spawn
                int lowerXBound = (int) (this.windowSize.x /8);
                int upperXBound = (int) (this.windowSize.x - lowerXBound);

                // Start within the x-range on top of the screen
                int startPosX = this.randomGenerator.Next(lowerXBound, upperXBound);
                int startPosY = 0;

                // Create a new asteroid with the type and position
                Asteroid asteroid = this.CreateAsteroid(asteroidType, startPosX, startPosY);

                // Add the newly created asteroid to the cast
                cast.AddActor("asteroids", asteroid);

                // lastSpawn is right now.
                this.lastSpawn = DateTime.Now;
            }
        }
    }
}