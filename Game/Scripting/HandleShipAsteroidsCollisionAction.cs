using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleShipAsteroidsCollisionAction : genie.script.Action {
        
        // Member Variables
        RaylibPhysicsService physicsService;
        RaylibAudioService audioService;
        private genie.cast.Actor? ship;


        // Constructor
        public HandleShipAsteroidsCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.ship = null;
            this.physicsService = physicsService;
            this.audioService = audioService;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // Grab the ship from the cast
            this.ship = cast.GetFirstActor("ship");

            // Only worry about collision if the ship actually exists
            if (this.ship != null) {
                foreach (Actor actor in cast.GetActors("asteroids")) {
                    if (this.physicsService.CheckCollision(this.ship, actor)) {
                        cast.RemoveActor("ship", this.ship);
                        cast.RemoveActor("asteroids", actor);
                        this.audioService.PlaySound("asteroid/assets/sound/explosion-01.wav", (float) 0.1);
                        this.ship = null;
                        break;
                    }
                }
            }
        }
    }
}