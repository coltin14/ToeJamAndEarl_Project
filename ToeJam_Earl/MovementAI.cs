using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeJam_Earl
{
    public abstract class MovementAI
    {
        public abstract void Move(Sprite Bot, GameTime gameTime);
    }

}
