using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Represents a rocket enemy that flies horizontally through the screen.
/// </summary>
class Rocket : AnimatedGameObject
{
    Level level;
    Vector2 startPosition;
    const float speed = 500;

    public Rocket(Level level, Vector2 startPosition, bool facingLeft) 
        : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;

        LoadAnimation("Sprites/LevelObjects/Rocket/spr_rocket@3", "rocket", true, 0.1f);
        PlayAnimation("rocket");
        SetOriginToCenter();
        CreateCollider();

        sprite.Mirror = facingLeft;
        if (sprite.Mirror)
        {
            velocity.X = -speed;
            this.startPosition = startPosition + new Vector2(2*speed, 0);
        }
        else
        {
            velocity.X = speed;
            this.startPosition = startPosition - new Vector2(2 * speed, 0);
        }
        Reset();
    }

    public void CreateCollider()
    {
        //make a collider for the rocket
    }

    public override void Reset()
    {
        // go back to the starting position
        LocalPosition = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // if the rocket has left the screen, reset it
        if (sprite.Mirror && BoundingBox.Right < level.BoundingBox.Left)
            Reset();
        else if (!sprite.Mirror && BoundingBox.Left > level.BoundingBox.Right)
            Reset();

        //code voor aanpassing
        /*
        // if the rocket touches the player, the player dies
        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
            level.Player.Die();
        */

        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            if (level.Player.GlobalPosition.Y <= GlobalPosition.Y)
            {
                Reset();
                level.Player.Jump();
                Debug.WriteLine("from above");

            }
            else
            {

                // we did not.
                level.Player.Die();
                Debug.WriteLine("from under");
            }

            Debug.WriteLine($" Player pos: {level.Player.GlobalPosition.Y}");
            Debug.WriteLine($" Rocket pos: {GlobalPosition.Y}");
        }



        #region Poging 1, werkte niet
        /*
        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Debug.WriteLine("player is on y: " + level.Player.LocalPosition.Y);
            Debug.WriteLine("were on y: " + LocalPosition.Y);
            if (level.Player.LocalPosition.Y > LocalPosition.Y)
            {
                //we came from above
                Visible = false;
                Debug.WriteLine("from above");
            }
            else
            {
                
                // we did not.
                level.Player.Die();
                Debug.WriteLine("from under"); 
            }
        }
        */
        #endregion

        #region poging 2, werkt niet
        /*
        if (HasPixelPreciseCollision(level.Player))
        {
            Rectangle overlap = CollisionDetection.CalculateIntersection(BoundingBox, level.Player.BoundingBox);
            Debug.WriteLine("Overlap box is : " + overlap.ToString());

            //if we collide horizontally, the overlapbox's width will be smaller than it's height and so the player should die.
            if (overlap.Width < overlap.Height)
            {
                level.Player.Die();
            }
            //if this is not the case, the player came from above and so the rocket should die.
            else
            {
                //if the player jumps on top of upper collider and can collide with objects, the rocket dies.
                Visible = false;
            }
        }
        */
        #endregion

        #region poging 3, werkt ook niet
        /*
        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Debug.WriteLine("Player Bottom Y: " + (level.Player.BoundingBox.Bottom));
            Debug.WriteLine("Rocket Top Y: " + (BoundingBox.Top));

            //check who's above who
            bool playerIsAboveRocket = level.Player.BoundingBox.Bottom >= BoundingBox.Top;

            if (playerIsAboveRocket)
            {
                //were above the rocket, so reset it and maybe give the player a doublejump?
                this.Reset();
                //level.Player.Jump(600);
            }
            else
            {
                level.Player.Die();
            }
        }
        */
        #endregion
    }
}
