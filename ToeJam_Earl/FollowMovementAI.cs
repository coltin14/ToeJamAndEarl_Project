using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeJam_Earl
{
    public class FollowMovementAI : MovementAI
    {
        public MonoGameLibrary.Graphics.Sprite Target { get; set; }

        public override void Move(MonoGameLibrary.Graphics.Sprite Bot, GameTime gameTime)
        {
            if (Target is null)
                return;

            var dir = Target.Position - Bot.Position;

            if (dir.Length() > 4)
            {
                dir.Normalize();
                Bot._position += dir * Bot.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
