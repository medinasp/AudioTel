$(document).ready(function () {
    Main.init();
});

// ----------
window.Main = {
    // ----------
    init: function () {
        var self = this;

        if (!Modernizr.audio) {
            alert("Este navegador não suporta a tag audio.");
            return;
        }

        if (!Modernizr.video) {
            alert("Este navegador não suporta a tag video.");
            return;
        }
        //Verifica se há recursos e cria dois objetos,
        //e vincula o controle volume aos controles HTML
        this.video = this.initMedia("video");
        this.audio = this.initMedia("audio");

        this.$volume = $("#volume");

        this.$volumeUp = $("#volume-up")
            .click(function () {
                self.audio.media.muted = false;
                self.audio.media.volume += 0.1;
            });

        this.$volumeDown = $("#volume-down")
            .click(function () {
                self.audio.media.muted = false;
                self.audio.media.volume -= 0.1;
            });

        this.$mute = $("#mute")
            .click(function () {
                self.audio.media.muted = !self.audio.media.muted;
            });

        this.audio.$media
            .bind("volumechange", function () {
                self.showVolume();
            });

        this.showVolume();
    },

    // ----------
    initMedia: function (name) {

        //Encontra o elemento mídia com jQuery 
        var result = {};
        result.$media = $("#" + name);
        result.media = result.$media[0];
        result.$controls = $("#" + name + "-controls");
        //Com controles <div> encontra o botão find play e time
        result.$play = result.$controls.find(".play");
        result.$time = result.$controls.find(".time");

        result.$play
            .click(function () {
                if (result.media.paused)
                    result.media.play();
                else
                    result.media.pause();
            });

        result.$media
            .bind("playing", function () {
                result.$play.text("pausar");
            })
            .bind("pause", function () {
                result.$play.text("ouvir");
            })
            .bind("ended", function () {
                result.media.play();
            })
            .bind("timeupdate", function () {
                var prettyTime = Math.round(result.media.currentTime * 100) / 100;
                result.$time
                    .text("time: " + prettyTime + "s");
            });
        //executa a midia
        result.media.play();
        return result;
    },

    // ----------
    showVolume: function () {
        var prettyVolume = Math.round(this.audio.media.volume * 10) / 10;
        if (this.audio.media.muted) {
            prettyVolume = 0;
            this.$mute.text("ouvir");
        } else {
            this.$mute.text("mudo");
        }

        this.$volume.text(prettyVolume);
    }
};