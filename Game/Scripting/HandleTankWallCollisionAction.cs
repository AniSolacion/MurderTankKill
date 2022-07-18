using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleTankWallCollisionAction : genie.script.Action {
        
        // Member Variables
        //RaylibPhysicsService physicsService;
        private genie.cast.Actor? tank1;
        private genie.cast.Actor? tank2;
        private genie.cast.Actor? turret1;
        private genie.cast.Actor? turret2;
        private genie.cast.Actor? MAX_X;
        private genie.cast.Actor? MAX_Y;
        private genie.cast.Actor? MIN_X;
        private genie.cast.Actor? MIN_Y;
        

        public HandleTankWallCollisionAction(int priority) : base(priority) {
            this.tank1 = null;
            this.tank2 = null;
            this.turret1 = null;
            this.turret2 = null;
            
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
            this.tank1 = cast.GetFirstActor("tank1");
            this.turret1 = cast.GetFirstActor("turret1");

            if (this.tank1 != null) {
                if (this.tank1.GetX() - this.tank1.GetWidth() < MIN_X.GetX()) {
                    this.tank1.SetX(MIN_X.GetX() + this.tank1.GetWidth());
                    this.turret1.SetX(this.turret1.GetPreviousX());
                }
                if (this.tank1.GetX() + this.tank1.GetWidth() > MAX_X.GetX()) {
                    this.tank1.SetX(MAX_X.GetX() - this.tank1.GetWidth());
                    this.turret1.SetX(this.turret1.GetPreviousX());
                }
                if (this.tank1.GetY() - this.tank1.GetHeight() < MIN_Y.GetY()) {
                    this.tank1.SetY(MIN_Y.GetY() + this.tank1.GetHeight());
                    this.turret1.SetY(this.turret1.GetPreviousY());
                }
                if (this.tank1.GetY() + this.tank1.GetHeight() > MAX_Y.GetY()) {
                    this.tank1.SetY(MAX_Y.GetY() - this.tank1.GetHeight());
                    this.turret1.SetY(this.turret1.GetPreviousY());
                }
            }

            this.tank2 = cast.GetFirstActor("tank2");
            this.turret2 = cast.GetFirstActor("turret2");

            if (this.tank2 != null) {
                if (this.tank2.GetX() - this.tank2.GetWidth() < MIN_X.GetX()) {
                    this.tank2.SetX(MIN_X.GetX() + this.tank2.GetWidth());
                    this.turret2.SetX(this.turret2.GetPreviousX());
                }
                if (this.tank2.GetX() + this.tank2.GetWidth() > MAX_X.GetX()) {
                    this.tank2.SetX(MAX_X.GetX() - this.tank2.GetWidth());
                    this.turret2.SetX(this.turret2.GetPreviousX());
                }
                if (this.tank2.GetY() - this.tank2.GetHeight() < MIN_Y.GetY()) {
                    this.tank2.SetY(MIN_Y.GetY() + this.tank2.GetHeight());
                    this.turret2.SetY(this.turret2.GetPreviousY());
                }
                if (this.tank2.GetY() + this.tank2.GetHeight() > MAX_Y.GetY()) {
                    this.tank2.SetY(MAX_Y.GetY() - this.tank2.GetHeight());
                    this.turret2.SetY(this.turret2.GetPreviousY());
                }
            }
        } 
    }
}