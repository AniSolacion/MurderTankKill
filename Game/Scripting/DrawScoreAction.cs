using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class DrawScoreAction : genie.script.Action {
        
        private RaylibScreenService screenService;
        private PlayerScore? score;

        public DrawScoreAction(int priority, RaylibScreenService screenService) : base(priority) {
            this.screenService = screenService;
            this.score = null;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            
            // Look for the score actor in the actors list
            if (this.score == null) {
                this.score = (PlayerScore?) cast.GetFirstActor("score");
            }

            // Draw the score on the screen
            if (this.score != null) {
                this.screenService.DrawText("Score: " + this.score.GetScore().ToString(), fontSize:48, color:Color.WHITE, position:(20, 20) );
            }
        }
    }
}