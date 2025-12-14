using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeJam_Earl
{
    public class PatrolMovement : MovementAI
    {
        private readonly List<Vector2> _path = new();
        private int _current;

        public void AddWaypoint(Vector2 waypoint)
        {
            _path.Add(waypoint);
        }

        public override void Move(Sprite bot, GameTime gameTime)
        {
            if (_path.Count == 0)
                return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 target = _path[_current];
            Vector2 dir = target - bot.Position;

            // If close to target → switch to next waypoint
            if (dir.LengthSquared() < 16f)  // 4px threshold
            {
                _current = (_current + 1) % _path.Count;
                return;
            }

            // Otherwise move towards it
            dir.Normalize();
            bot._position += dir * bot.Speed * dt;
        }
    }
}

