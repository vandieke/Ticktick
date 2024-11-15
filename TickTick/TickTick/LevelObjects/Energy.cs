using System;
using Microsoft.Xna.Framework;
using Engine;
using System.Diagnostics.Metrics;
using System.Diagnostics;

class Energy : SpriteGameObject
{
    Level level;
    Vector2 startPosition;
    protected float bounce;

    public Energy(Level level, Vector2 startPosition) : base("Sprites/LevelObjects/spr_energy", TickTick.Depth_LevelObjects)
    {
        //moet nog een andere sprite ingesteld worden maar oke.
        this.level = level;
        this.startPosition = startPosition;
        Reset();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + LocalPosition.X;
        bounce = (float)Math.Sin(t) * 0.5f;
        localPosition.Y += bounce;

        // check if the player collects this feather
        if (Visible && level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Visible = false;
            level.Player.Dash();
            //nog andere soundeffect fixen
            ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_watercollected");
        }
    }
    public override void Reset()
    {
        localPosition = startPosition;
        Visible = true;
    }
}
