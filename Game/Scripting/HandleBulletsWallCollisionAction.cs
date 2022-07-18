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
                if (this.bullets.GetX() - this.bullets.GetWidth() < MIN_X.GetX()) {
                    this.bullets.SetVx(-this.bullets.GetVx());
                }
                if (this.bullets.GetX() + this.bullets.GetWidth() > MAX_X.GetX()) {
                    this.bullets.SetVx(-this.bullets.GetVx());
                }
                if (this.bullets.GetY() - this.bullets.GetHeight() < MIN_Y.GetY()) {
                    this.bullets.SetVy(-this.bullets.GetVy());
                }
                if (this.bullets.GetY() + this.bullets.GetHeight() > MAX_Y.GetY()) {
                    this.bullets.SetVy(-this.bullets.GetVy());
                }
            }
        }
    }
}