using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeJam_Earl
{
    public class ChaseMovement : MovementAI
    {
        private readonly Func<Vector2> _targetGetter;
        private readonly float _speed; // pixels per second
        private readonly float _stopDistance;

        public ChaseMovement(Func<Vector2> targetGetter, float speed = 140f, float stopDistance = 8f)
        {
            if (targetGetter != null)
            {
                _targetGetter = targetGetter;
            }
            else
            {
                _targetGetter = () => Vector2.Zero;
            }
            _speed = speed;
            _stopDistance = stopDistance;
        }

        public override void Move(Sprite bot, GameTime gameTime)
        {
            if (bot == null || _targetGetter == null || gameTime == null) return;

            Vector2 target = _targetGetter();
            Vector2 toTarget = target - bot._position;

            float distance = toTarget.Length();

            // If already very close, don't move (prevents jitter/overshoot)
            if (distance <= _stopDistance) return;

            toTarget.Normalize();
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 movement = toTarget * _speed * time;

            // Clamp movement to not overshoot target
            if (movement.Length() > distance)
                movement = toTarget * distance;

            bot._position += movement;
        }
    }
}
