using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ToeJam_Earl;

public class Camera2D
{
    public Matrix Transform { get; private set; }
    public Vector2 Position { get; private set; }
    public float Zoom { get; set; } = 1f;

    public void Follow(Vector2 targetPosition, int screenWidth, int screenHeight)
    {
        Position = targetPosition - new Vector2(screenWidth / 2f, screenHeight / 2f);

        Transform =
            Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
            Matrix.CreateScale(Zoom, Zoom, 3f);
    }
}

