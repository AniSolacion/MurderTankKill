using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleTankAsteroidsCollisionAction : genie.script.Action {
        
        // Member Variables
        RaylibPhysicsService physicsService;
        RaylibAudioService audioService;
        private genie.cast.Actor? tank;


        // Constructor
        public HandleTankAsteroidsCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.tank = null;
            this.physicsService = physicsService;
            this.audioService = audioService;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // Grab the tank from the cast
            this.tank = cast.GetFirstActor("tank");

            // Only worry about collision if the tank actually exists
            if (this.tank != null) {
                foreach (Actor actor in cast.GetActors("asteroids")) {
                    if (this.physicsService.CheckCollision(this.tank, actor)) {
                        cast.RemoveActor("tank", this.tank);
                        cast.RemoveActor("asteroids", actor);
                        this.audioService.PlaySound("asteroid/assets/sound/explosion-01.wav", (float) 0.1);
                        this.tank = null;
                        break;
                    }
                }
            }
        }
    }
}