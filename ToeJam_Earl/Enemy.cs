using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeJamAndEarlFirstBatch;

namespace ToeJam_Earl
{
    public class Enemy : Sprite
    {
        public Enemy(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }

    }
}
