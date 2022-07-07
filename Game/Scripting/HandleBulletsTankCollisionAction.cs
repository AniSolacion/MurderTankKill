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
        private List<Actor> bullets;


        // Constructor
        public HandleBulletsAsteroidsCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.physicsService = physicsService;
            this.bullets = new List<Actor>();
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {

            // First, get a list of bullets out of the cast
            bullets = cast.GetActors("bullets");

            // Check if any bullet collides with any asteroid
            foreach (Asteroid asteroid in cast.GetActors("asteroids")) {
                Actor? collidedBullet = this.physicsService.CheckCollisionList(asteroid, bullets);
                if (collidedBullet != null) {
                    cast.RemoveActor("bullets", collidedBullet);
                    asteroid.TakeDamage(1);

                    // Destroy asteroid if its health is 0
                    if (asteroid.GetHP() <= 0) {
                        cast.RemoveActor("asteroids", asteroid);
                    }
                }
            }

            // Instant Kill Astroid&Ship Collision
            // public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // // Grab the ship from the cast
            // this.ship = cast.GetFirstActor("ship");

            // // Only worry about collision if the ship actually exists
            // if (this.ship != null) {
            //     foreach (Actor actor in cast.GetActors("asteroids")) {
            //         if (this.physicsService.CheckCollision(this.ship, actor)) {
            //             cast.RemoveActor("ship", this.ship);
            //             cast.RemoveActor("asteroids", actor);
            //             this.audioService.PlaySound("asteroid/assets/sound/explosion-01.wav", (float) 0.1);
            //             this.ship = null;
            //             break;
            //         }
            //     }
            // }
        
        }
    }
}