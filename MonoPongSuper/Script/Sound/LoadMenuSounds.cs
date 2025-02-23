using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Sound
{
    public static class LoadMenuSounds
    {
        public static SoundEffect selection;
        public static SoundEffect select;
        public static void LoadMenuSFX(ContentManager cn) 
        {
            selection = cn.Load<SoundEffect>("sound/selection");
            select = cn.Load<SoundEffect>("sound/select");
        }
    }
}
