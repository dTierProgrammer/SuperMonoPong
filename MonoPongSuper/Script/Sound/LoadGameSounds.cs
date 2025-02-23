using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Sound
{
    public static class LoadGameSounds
    {
        public static SoundEffect score;
        public static SoundEffect bounce;
        public static SoundEffect paddleBounce;
        public static SoundEffect paddleCollide;
        public static SoundEffect ballLoss;
        public static SoundEffect ballRespawn;
        public static void LoadGameSFX(ContentManager cn) 
        {
            score = cn.Load<SoundEffect>("sound/score");
            bounce = cn.Load<SoundEffect>("sound/bounce");
            paddleBounce = cn.Load<SoundEffect>("sound/paddleBounce");
            paddleCollide = cn.Load<SoundEffect>("sound/paddleCollide");
            ballLoss = cn.Load<SoundEffect>("sound/ballLoss");
            ballRespawn = cn.Load<SoundEffect>("sound/ballRespawn");
        }
    }
}
