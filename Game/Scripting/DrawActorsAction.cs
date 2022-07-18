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

            Actor? tank1 = cast.GetFirstActor("tank1");
            Actor? tank2 = cast.GetFirstActor("tank2");
            
            // First, fill the screen with white every frame, get ready to draw more stuff
            this.screenService.FillScreen(Color.WHITE);

            Color actorColor = Color.BLACK;

            // Draw all actors as rectangles for now.
            foreach (Actor actor in cast.GetAllActors()) {


                if(actor == tank1){
                    actorColor = Color.BLUE;
                }
                else if(actor == tank2){
                    actorColor = Color.RED;
                }
                // Color actorColor = actor is Tank ? Color.BLUE : Color.BLACK;
                this.screenService.DrawRectangle(actor.GetPosition(), actor.GetWidth(), actor.GetHeight(), actorColor, 5);
            }
            this.screenService.DrawActors(cast.GetAllActors());
        }
    }
}