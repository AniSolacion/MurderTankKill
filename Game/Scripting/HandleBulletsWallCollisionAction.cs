using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleBulletsWallCollisionAction : genie.script.Action {
        
        // Member Variables
        RaylibPhysicsService physicsService;
        private List<Actor> bullets;
        private genie.cast.Actor? tank1;
        private genie.cast.Actor? tank2;
        private genie.cast.Actor? turret1;
        private genie.cast.Actor? turret2;
        // Constructor
        public HandleBulletsWallCollisionAction(int priority, RaylibPhysicsService physicsService, RaylibAudioService audioService) : base(priority) {
            this.physicsService = physicsService;
            this.bullets = new List<Actor>();
            this.tank1 = null;
            this.tank2 = null;
            this.turret1 = null;
            this.turret2 = null;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {

            // First, get a list of bullets out of the cast
            bullets = cast.GetActors("bullets");

            //Bullet Rotation
            // if(bullets.GetY() == wallMaxY.GetMaxY() || bullets.GetY() == wallMinY.GetMinY()){
            //     bullets.SetVy(-bullets.GetVy());
            // }
            // if(bullets.GetX() == wallMaxX.GetMaxX() || bullets.GetX() == wallMinX.GetMinX()){
            //     bullets.SetVx(-bullets.GetVy());
            // }
        }
    }
}