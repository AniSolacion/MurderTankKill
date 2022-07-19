using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleBulletsWallCollisionAction : genie.script.Action {
        
        // Member Variables
        private genie.cast.Actor? bullets;
        private genie.cast.Actor? MAX_X;
        private genie.cast.Actor? MAX_Y;
        private genie.cast.Actor? MIN_X;
        private genie.cast.Actor? MIN_Y;
        

        public HandleBulletsWallCollisionAction(int priority) : base(priority) {
            this.bullets = null;
            MAX_X = null;
            MIN_X = null;
            MAX_Y = null;
            MIN_Y = null;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            this.MAX_X = cast.GetFirstActor("maxX");
            this.MAX_Y = cast.GetFirstActor("maxY");
            this.MIN_X = cast.GetFirstActor("minX");
            this.MIN_Y = cast.GetFirstActor("minY");
            this.bullets = cast.GetFirstActor("bullets");

            if (this.bullets != null) {
                //Counts for each bullet
                foreach (Actor bullet in cast.GetActors("bullets")) {
                    //Checks if a bullet hits any wall and counts it
                    if (bullet.GetX() - bullet.GetWidth() < MIN_X.GetX()) {
                        bullet.SetVx(-bullet.GetVx());
                        bullet.setBulletCounter(bullet.getBulletCounter() + 1);
                    }
                    if (bullet.GetX() + bullet.GetWidth() > MAX_X.GetX()) {
                        bullet.SetVx(-bullet.GetVx());
                        bullet.setBulletCounter(bullet.getBulletCounter() + 1);
                    }
                    if (bullet.GetY() - bullet.GetHeight() < MIN_Y.GetY()) {
                        bullet.SetVy(-bullet.GetVy());
                        bullet.setBulletCounter(bullet.getBulletCounter() + 1);
                    }
                    if (bullet.GetY() + bullet.GetHeight() > MAX_Y.GetY()) {
                        bullet.SetVy(-bullet.GetVy());
                        bullet.setBulletCounter(bullet.getBulletCounter() + 1);
                    }
                    //Deletes bullet if it hits the wall 3 times
                    if(bullet.getBulletCounter() == 3){
                        cast.RemoveActor("bullets", bullet);
                    }
                }
            }
        }
    }
}