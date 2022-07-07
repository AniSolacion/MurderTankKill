using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

namespace asteroid.script {
    class HandleTankMovementAction : genie.script.Action {
        
        private RaylibKeyboardService keyboardService;
        private List<int> keysOfInterest;
        private int tankMovementVel;

        public HandleTankMovementAction(int priority, RaylibKeyboardService keyboardService) : base(priority) {
            this.keyboardService = keyboardService;
            this.tank = null;
            this.tankMovementVel = 4;
            this.keysOfInterest = new List<int>();
            this.keysOfInterest.Add(Keys.LEFT);
            this.keysOfInterest.Add(Keys.RIGHT);
            this.keysOfInterest.Add(Keys.DOWN);
            this.keysOfInterest.Add(Keys.UP);
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            
            // Grab the tank from the cast
            this.tank = cast.GetFirstActor("tank");
            
            Actor tank1 = cast.GetFirstActor("tank1");

            // Only move if ship is not null
            if (this.tank != null) {
                
                // Get the keysState from the keyboardService
                Dictionary<int, bool> keysState = keyboardService.GetKeysState(this.keysOfInterest);
                
                // Change the velocity to the appropriate value and let MoveActorsAction handle the
                // actual movement
                if (keysState[Keys.LEFT]) {
                    this.tank.SetVx(-this.tankMovementVel);
                }
                if (keysState[Keys.RIGHT]) {
                    this.tank.SetVx(this.tankMovementVel);
                }
                if (keysState[Keys.DOWN]) {
                    this.tank.SetVy(this.tankMovementVel);
                }
                if (keysState[Keys.UP]) {
                    this.tank.SetVy(-this.tankMovementVel);
                }

                // If none of the LEFT or RIGHT keys are down, x-velocity is 0
                if (!(keysState[Keys.LEFT] || keysState[Keys.RIGHT])) {
                    this.tank.SetVx(0);
                }

                // If none of the UP or DOWN keys are down, y-velocity is 0
                if (!(keysState[Keys.UP] || keysState[Keys.DOWN])) {
                    this.tank.SetVy(0);
                }
            }
        }
    }
}