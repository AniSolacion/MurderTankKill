using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

namespace asteroid.script {
    class HandleTankMovementAction : genie.script.Action {
        
        private RaylibKeyboardService keyboardService;
        private List<int> keysOfInterest;
        private int tankMovementVel = 10;

        public HandleTankMovementAction(int priority, RaylibKeyboardService keyboardService) : base(priority) {
            this.keyboardService = keyboardService;
            this.tankMovementVel = 4;
            this.keysOfInterest = new List<int>();

            //PLayer 1 Movement Keys
            this.keysOfInterest.Add(Keys.A);
            this.keysOfInterest.Add(Keys.D);
            this.keysOfInterest.Add(Keys.S);
            this.keysOfInterest.Add(Keys.W);

            //Player 2 Movement Keys
            this.keysOfInterest.Add(Keys.J);
            this.keysOfInterest.Add(Keys.L);
            this.keysOfInterest.Add(Keys.K);
            this.keysOfInterest.Add(Keys.I);
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            
            // Grab the tank from the cast

            Actor tank1 = cast.GetFirstActor("tank1");
            Actor tank2 = cast.GetFirstActor("tank2");


            // Only move if ship is not null
            if (tank1 != null) {
                
                // Get the keysState from the keyboardService
                Dictionary<int, bool> keysState = keyboardService.GetKeysState(this.keysOfInterest);
                
                // Change the velocity to the appropriate value and let MoveActorsAction handle the
                // actual movement
                if (keysState[Keys.A]) {
                    tank1.SetVx(-this.tankMovementVel);
                }
                if (keysState[Keys.D]) {
                    tank1.SetVx(this.tankMovementVel);
                }
                if (keysState[Keys.S]) {
                    tank1.SetVy(this.tankMovementVel);
                }
                if (keysState[Keys.W]) {
                    tank1.SetVy(-this.tankMovementVel);
                }

                // If none of the LEFT or RIGHT keys are down, x-velocity is 0
                if (!(keysState[Keys.A] || keysState[Keys.D])) {
                    tank1.SetVx(0);
                }

                // If none of the UP or DOWN keys are down, y-velocity is 0
                if (!(keysState[Keys.W] || keysState[Keys.S])) {
                    tank1.SetVy(0);
                }
            }

            if (tank2 != null) {
                
                // Get the keysState from the keyboardService
                Dictionary<int, bool> keysState = keyboardService.GetKeysState(this.keysOfInterest);
                
                // Change the velocity to the appropriate value and let MoveActorsAction handle the
                // actual movement
                if (keysState[Keys.J]) {
                    tank2.SetVx(-this.tankMovementVel);
                }
                if (keysState[Keys.L]) {
                    tank2.SetVx(this.tankMovementVel);
                }
                if (keysState[Keys.K]) {
                    tank2.SetVy(this.tankMovementVel);
                }
                if (keysState[Keys.I]) {
                    tank2.SetVy(-this.tankMovementVel);
                }

                // If none of the LEFT or RIGHT keys are down, x-velocity is 0
                if (!(keysState[Keys.J] || keysState[Keys.L])) {
                    tank2.SetVx(0);
                }

                // If none of the UP or DOWN keys are down, y-velocity is 0
                if (!(keysState[Keys.I] || keysState[Keys.K])) {
                    tank2.SetVy(0);
                }
            }
        }
    }
}