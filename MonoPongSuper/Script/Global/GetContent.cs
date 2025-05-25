using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Global
{
    public static class GetContent
    {
        private static Game1 _game;

        public static void Initialize(Game1 game) 
        {
            _game = game;
        }

        public static Texture2D GetTexture(string path) 
        {
            Texture2D item = _game.Content.Load<Texture2D>(path);
            return item;
        }

        public static SoundEffect GetSound(string path)
        {
            SoundEffect item = _game.Content.Load<SoundEffect>(path);
            return item;
        }

        public static SpriteFont GetFont(string path) 
        {
            SpriteFont item = _game.Content.Load<SpriteFont>(path);
            return item;
        }
    }
}
