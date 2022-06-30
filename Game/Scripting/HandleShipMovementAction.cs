using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

namespace asteroid.script {
    class HandleShipMovementAction : genie.script.Action {
        
        private RaylibKeyboardService keyboardService;
        private genie.cast.Actor? ship;
        private List<int> keysOfInterest;
        private int shipMovementVel;

        public HandleShipMovementAction(int priority, RaylibKeyboardService keyboardService) : base(priority) {
            this.keyboardService = keyboardService;
            this.ship = null;
            this.shipMovementVel = 4;
            this.keysOfInterest = new List<int>();
            this.keysOfInterest.Add(Keys.LEFT);
            this.keysOfInterest.Add(Keys.RIGHT);
            this.keysOfInterest.Add(Keys.DOWN);
            this.keysOfInterest.Add(Keys.UP);
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            
            // Grab the ship from the cast
            this.ship = cast.GetFirstActor("ship");

            // Only move if ship is not null
            if (this.ship != null) {
                
                // Get the keysState from the keyboardService
                Dictionary<int, bool> keysState = keyboardService.GetKeysState(this.keysOfInterest);
                
                // Change the velocity to the appropriate value and let MoveActorsAction handle the
                // actual movement
                if (keysState[Keys.LEFT]) {
                    this.ship.SetVx(-this.shipMovementVel);
                }
                if (keysState[Keys.RIGHT]) {
                    this.ship.SetVx(this.shipMovementVel);
                }
                if (keysState[Keys.DOWN]) {
                    this.ship.SetVy(this.shipMovementVel);
                }
                if (keysState[Keys.UP]) {
                    this.ship.SetVy(-this.shipMovementVel);
                }

                // If none of the LEFT or RIGHT keys are down, x-velocity is 0
                if (!(keysState[Keys.LEFT] || keysState[Keys.RIGHT])) {
                    this.ship.SetVx(0);
                }

                // If none of the UP or DOWN keys are down, y-velocity is 0
                if (!(keysState[Keys.UP] || keysState[Keys.DOWN])) {
                    this.ship.SetVy(0);
                }
            }
        }
    }
}