using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleMothershipAsteroidsCollisionAction : genie.script.Action {
        
        // Member Variables
        RaylibPhysicsService physicsService;
        RaylibAudioService audioService;
        private Mothership? mothership;


        // Constructor
        public HandleMothershipAsteroidsCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.mothership = null;
            this.physicsService = physicsService;
            this.audioService = audioService;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // Grab the mothership from the cast
            this.mothership = (Mothership?) cast.GetFirstActor("mothership");

            // Only worry about collision if mothership actually exists
            if (this.mothership != null) {
                // Look through all the astroids, see if any collides with ship
                foreach (Asteroid asteroid in cast.GetActors("asteroids")) {
                    if (this.physicsService.CheckCollision(this.mothership, asteroid)) {
                        // Damage the mothership
                        mothership.TakeDamage(asteroid.GetHP());

                        // Explode the asteroid
                        cast.RemoveActor("asteroids", asteroid);
                        this.audioService.PlaySound("asteroid/assets/sound/explosion-01.wav", (float) 0.1);

                        // Check if mothership lost all health
                        if (this.mothership.GetHP() <= 0) {
                            cast.RemoveActor("mothership", this.mothership);
                            this.mothership = null;
                        }

                        break;
                    }
                }
            }
        }
    }
}