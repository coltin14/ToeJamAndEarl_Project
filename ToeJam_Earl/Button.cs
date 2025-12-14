/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ToeJam_Earl;

public class MenuButton
{
    public Rectangle Bounds;
    public string Text;

    private SpriteFont font;
    private Texture2D pixel;
    private MouseState lastMouse;

    public MenuButton(SpriteFont font, Rectangle bounds, string text)
    {
        this.font = font;
        Bounds = bounds;
        Text = text;
    }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        pixel = new Texture2D(graphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

    }


    public bool Update()
    {
        MouseState mouse = Mouse.GetState();

        bool clicked =
            Bounds.Contains(mouse.Position) &&
            mouse.LeftButton == ButtonState.Pressed &&
            lastMouse.LeftButton == ButtonState.Released;

        lastMouse = mouse;
        return clicked;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(pixel, Bounds, Color.Gray);

        Vector2 size = font.MeasureString(Text);
        Vector2 pos = new Vector2(
            Bounds.Center.X - size.X / 2,
            Bounds.Center.Y - size.Y / 2
        );

        spriteBatch.DrawString(font, Text, pos, Color.White);
    }
}

*/