using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToeJam_Earl
{
    public class GameObject
    {
        public Texture2D sprite;   
        public Vector2 _position;
        public Rectangle sourceRect;
        public float Scale = 1.0f;
        private Texture2D texture;

        public virtual void Update(GameTime gameTime) 
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
            {
                spriteBatch.Draw(sprite, _position, sourceRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }
    }

    public class Toe_Jam : GameObject
    {
        public Toe_Jam(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }
    public class NPC : GameObject
    {
        public NPC(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }

    //the Lil_Devil sprite = enemy 
    /*public class Enemy : GameObject
    {
        public Enemy(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }*/
    public class Item : GameObject
    {
        public bool IsActive { get; set; }

        public Item(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
            IsActive = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
                base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsActive)
                return;
            base.Update(gameTime);
        }
    }

    public class Elevator : GameObject
    {
        public Elevator(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }
    public class Tile : GameObject
    {
        public Tile(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }
    public class UIelement : GameObject
    {
        public UIelement(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }
    
    public class List : GameObject
    {
        public List(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }
    
    public class Button : GameObject
    {
        //private Rectangle buttonRectangle;
        //public bool IsClicked { get; private set; }
        public Button(Texture2D texture, Vector2 position, Rectangle source)
        {
            sprite = texture;
            _position = position;
            sourceRect = source;
        }
    }
}
