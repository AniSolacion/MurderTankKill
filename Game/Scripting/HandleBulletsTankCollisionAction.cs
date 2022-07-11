using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleBulletsTankCollisionAction : genie.script.Action {
        
        // Member Variables
        RaylibPhysicsService physicsService;
        private List<Actor> bullets;
        private genie.cast.Actor tank1;
        private genie.cast.Actor tank2;
        // Constructor
        public HandleBulletsTankCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.physicsService = physicsService;
            this.bullets = new List<Actor>();
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {

            // First, get a list of bullets out of the cast
            bullets = cast.GetActors("bullets");

            // Check if any bullet collides with any asteroid
            foreach (Bullet bullet in cast.GetActors("bullets")) {
                Actor? collidedBullet = this.physicsService.CheckCollisionList(tank1, bullets);
                if (collidedBullet != null) {
                    cast.RemoveActor("bullets", collidedBullet);
                    cast.RemoveActor("tank1", tank1);

                    // cast.RemoveActor("bullets", collidedBullet);
                    // asteroid.TakeDamage(1);

                    // // Destroy asteroid if its health is 0
                    // if (asteroid.GetHP() <= 0) {
                    //     cast.RemoveActor("asteroids", asteroid);
                    // }
                }
            }

            foreach (Tank tank2 in cast.GetActors("tank2")) {
                Actor? collidedBullet = this.physicsService.CheckCollisionList(tank2, bullets);
                if (collidedBullet != null) {
                    cast.RemoveActor("bullets", collidedBullet);
                    cast.RemoveActor("tank2", tank2);

                    // cast.RemoveActor("bullets", collidedBullet);
                    // asteroid.TakeDamage(1);

                    // // Destroy asteroid if its health is 0
                    // if (asteroid.GetHP() <= 0) {
                    //     cast.RemoveActor("asteroids", asteroid);
                    // }
                }
            }
        }
    }
}