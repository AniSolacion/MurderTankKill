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
            Actor? startButton = cast.GetFirstActor("start_button");
            
            // First, fill the screen with white every frame, get ready to draw more stuff
            this.screenService.FillScreen(Color.WHITE);

            Color actorColor = Color.WHITE;

            // Draws around actors
            foreach (Actor actor in cast.GetAllActors()) {
                
                if(actor == tank1){
                    actorColor = Color.BLUE; //Specifies tank1 with a blue surrounding
                }
                else if(actor == tank2){
                    actorColor = Color.RED; //Specifies tank2 with a red surrounding
                }
                else if (actor == startButton) {
                    actorColor = Color.BLACK;
                }
                else {
                    actorColor = Color.WHITE;
                }
                this.screenService.DrawRectangle(actor.GetPosition(), actor.GetWidth(), actor.GetHeight(), actorColor, 5);
            }
            this.screenService.DrawActors(cast.GetAllActors());
        }
    }
}