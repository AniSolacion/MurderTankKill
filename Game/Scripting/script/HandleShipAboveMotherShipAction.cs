using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class HandleShipAboveMotherShipAction : genie.script.Action {
        
        // Member Variables
        private (int x, int y) windowSize;
        private Ship? ship;
        private Mothership? mothership;


        // Constructor
        public HandleShipAboveMotherShipAction(int priority, (int x, int y) windowSize) : base(priority) {
            this.windowSize = windowSize;
            this.ship = null;
            this.mothership = null;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // Grab the ship and mothership from the cast
            this.ship = (Ship?) cast.GetFirstActor("ship");
            this.mothership = (Mothership?) cast.GetFirstActor("mothership");

            // Only worry about collision if both actually exists
            if (this.mothership != null && this.ship != null) {

                // Determine the line between ship and mothership
                float line = this.mothership.GetTopLeft().Item2 - (this.ship.GetHeight() / 2);

                // Don't allow the ship to go into the mothership
                if (this.ship.GetY() > line) {
                    this.ship.SetY(line);
                }
            }
        }
    }
}