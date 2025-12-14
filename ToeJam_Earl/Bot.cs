using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeJamAndEarlFirstBatch;

namespace ToeJam_Earl
{
    public class Bot : Enemy
    {
        public bool IsActive { get; set; } = true;
        //public float Scale = 1.0f;
        public MovementAI AI { get; set; }
        public FollowMovementAI FollowAI { get; set; }

        public Bot(Texture2D tex, Vector2 pos, Rectangle source) : base(tex, pos, source)
        {
            Speed = 100;
            this.Scale = new Vector2(3f, 3f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive)
                return;

            spriteBatch.Draw(
                sprite,           // from Enemy/Sprite
                _position,
                sourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,            // 👈 your Bot.Scale is used here
                SpriteEffects.None,
                0f);
        }

        

        public override void Update(GameTime time)
        {
            if (AI != null)
            {
                AI.Move(this, time);
            }

            if (!IsActive)
                return;
            base.Update(time);
        }

    }
}