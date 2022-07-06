using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class DrawActorsAction : genie.script.Action {
        
        private RaylibScreenService screenService;

        public DrawActorsAction(int priority, RaylibScreenService screenService) : base(priority) {
            this.screenService = screenService;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {

            // First, fill the screen with white every frame, get ready to draw more stuff
            this.screenService.FillScreen(Color.WHITE);

            // Draw all actors as rectangles for now.
            foreach (Actor actor in cast.GetAllActors()) {
                if(actor is tank1){
                    Color actorColor = Color.BLUE;
                }
                else if(actor is tank2){
                    Color actorColor = Color.RED;
                }
                else{
                    Color actorColor = Color.BLACK;
                }
                // Color actorColor = actor is Tank ? Color.BLUE : Color.BLACK;
                this.screenService.DrawRectangle(actor.GetPosition(), actor.GetWidth(), actor.GetHeight(), actorColor, 5);
            }
            this.screenService.DrawActors(cast.GetAllActors());
        }
    }
}