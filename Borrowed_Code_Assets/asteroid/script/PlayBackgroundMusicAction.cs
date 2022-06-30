using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class PlayBackgroundMusicAction : genie.script.Action {
        
        // Member Variables
        RaylibAudioService audioService;
        private string path;
        private bool backgroundPlaying;


        // Constructor
        public PlayBackgroundMusicAction(int priority, string path, RaylibAudioService audioService) : base(priority) {
            this.audioService = audioService;
            this.path = path;
            this.backgroundPlaying = false;
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            // If the background music is not already playing, play it
            if (!this.backgroundPlaying) {
                this.audioService.PlaySound(this.path, 1);
                this.backgroundPlaying = true;
            }
        }
    }
}