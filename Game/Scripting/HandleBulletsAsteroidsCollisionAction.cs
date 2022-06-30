using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleBulletsAsteroidsCollisionAction : genie.script.Action {
        
        // Member Variables
        RaylibPhysicsService physicsService;
        RaylibAudioService audioService;
        private PlayerScore? score;
        private List<Actor> bullets;


        // Constructor
        public HandleBulletsAsteroidsCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.score = null;
            this.physicsService = physicsService;
            this.audioService = audioService;
            this.bullets = new List<Actor>();
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // Grab the score from the cast
            this.score = (PlayerScore?) cast.GetFirstActor("score");

            // First, get a list of bullets out of the cast
            bullets = cast.GetActors("bullets");

            // Check if any bullet collides with any asteroid
            foreach (Asteroid asteroid in cast.GetActors("asteroids")) {
                Actor? collidedBullet = this.physicsService.CheckCollisionList(asteroid, bullets);
                if (collidedBullet != null) {
                    cast.RemoveActor("bullets", collidedBullet);
                    asteroid.TakeDamage(1);
                    this.audioService.PlaySound("asteroid/assets/sound/rock_cracking.wav", (float) 0.1);

                    // Destroy asteroid if its health is 0 and give player a score
                    if (asteroid.GetHP() <= 0) {
                        cast.RemoveActor("asteroids", asteroid);
                        if (this.score != null) {
                            this.score.AddScore(asteroid.GetPoints());
                        }
                        this.audioService.PlaySound("asteroid/assets/sound/explosion-01.wav", (float) 0.1);
                    }
                }
            }
        }
    }
}