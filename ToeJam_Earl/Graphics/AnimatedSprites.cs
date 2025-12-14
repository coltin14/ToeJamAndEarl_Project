using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Graphics;

public class AnimatedSprite : Sprite
{
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;

    /// <summary>
    /// Gets or Sets the animation for this animated sprite.
    /// **Modified to reset frame/elapsed time when set.**
    /// </summary>
    public Animation Animation
    {
        get => _animation;
        set
        {
            // Only update if the new animation is actually different
            if (_animation != value)
            {
                _animation = value;
                _currentFrame = 0; // Reset to the first frame
                _elapsed = TimeSpan.Zero; // Reset the elapsed time
                Region = _animation.Frames[_currentFrame];
            }
        }
    }

    /// <summary>
    /// Creates a new animated sprite.
    /// </summary>
    public AnimatedSprite() { }

    /// <summary>
    /// Creates a new animated sprite with the specified frames and delay.
    /// </summary>
    /// <param name="animation">The animation for this animated sprite.</param>
    public AnimatedSprite(Animation animation)
    {
        Animation = animation;
    }

    public int Frame
    {
        get => _currentFrame;
        set
        {
            if (value < 0 || value >= _animation.Frames.Count)
            {
                // Note: Throwing an exception here might crash your game if you are constantly setting Frame=0
                // throw new ArgumentOutOfRangeException(nameof(value), "Frame index is out of range.");
                // A safer approach might be to clamp the value or check your logic in Game1.cs.
                // For now, I'll keep the exception but be mindful of it.
                // Assuming the new logic in Game1.cs sets it to 0 only when not moving.
                if (value == 0 && _animation.Frames.Count > 0)
                {
                    _currentFrame = 0;
                    Region = _animation.Frames[_currentFrame];
                    _elapsed = TimeSpan.Zero; // Reset elapsed time so it doesn't immediately advance
                    return;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Frame index is out of range.");
                }
            }
            _currentFrame = value;
            Region = _animation.Frames[_currentFrame];
        }
    }

    public void Update(GameTime gameTime)
    {
        // Only update the frame if the animation is actually running (i.e., not Frame 0 and standing still)
        // If the animation has only one frame, this still works (0 >= 1 is false).
        if (_animation.Frames.Count > 1)
        {
            _elapsed += gameTime.ElapsedGameTime;


            if (_elapsed >= _animation.Delay)
            {
                _elapsed -= _animation.Delay;
                _currentFrame = (_currentFrame + 1) % _animation.Frames.Count;

                /*if (_currentFrame >= _animation.Frames.Count)
                {
                    _currentFrame = 0;
                }*/

                Region = _animation.Frames[_currentFrame];
            }
        }
    }
}