using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace XNAGameSpaceShotter.src.core {
    public class RepositorioDeSons {

        List<SoundEffect> sounds = new List<SoundEffect>();
        List<Song> songs = new List<Song>();



        public RepositorioDeSons(GameCore mygame) {
            sounds.Add(mygame.Content.Load<SoundEffect>("explosionSound"));
            sounds.Add(mygame.Content.Load<SoundEffect>("bigExplosionSound"));
            sounds.Add(mygame.Content.Load<SoundEffect>("shot"));
            songs.Add(mygame.Content.Load<Song>("Human nature"));
            songs.Add(mygame.Content.Load<Song>("Battle mode"));
            songs.Add(mygame.Content.Load<Song>("bike chase"));
            MediaPlayer.IsRepeating = true;
        }
        public void playSound(int i) {
            sounds[i].Play();
        }
        public void playSong(int i) {
            MediaPlayer.Play(songs[i]);
        }
            
    }
}
