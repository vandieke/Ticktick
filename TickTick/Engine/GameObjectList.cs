using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// A non-visual game object that has a list of game objects as its children.
    /// </summary>
    public class GameObjectList : GameObject
    {
        /// <summary>
        /// The child objects of this game object.
        /// </summary>
        List<GameObject> children;

        public SpriteBatch spriteBatchUI;

        /// <summary>
        /// Creates a new GameObjectList with an empty list of children.
        /// </summary>
        public GameObjectList()
        {
            children = new List<GameObject>();
        }

        /// <summary>
        /// Adds an object to this GameObjectList, and sets this GameObjectList as the parent of that object.
        /// </summary>
        /// <param name="obj">The game object to add.</param>
        public void AddChild(GameObject obj)
        {
            obj.Parent = this;
            children.Add(obj);
        }

        /// <summary>
        /// Performs input handling for all game objects in this GameObjectList.
        /// </summary>
        /// <param name="inputHelper">An object required for handling player input.</param>
        public override void HandleInput(InputHelper inputHelper)
        {
            for (int i = children.Count - 1; i >= 0; i--)
                children[i].HandleInput(inputHelper);
        }

        /// <summary>
        /// Performs the Update method for all game objects in this GameObjectList.
        /// </summary>
        /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameObject obj in children)
                obj.Update(gameTime);
        }

        /// <summary>
        /// Performs the Draw method for all game objects in this GameObjectList.
        /// </summary>
        /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            // Drawing normal elements.
            foreach (GameObject obj in children)
            {
               
                if (obj is SpriteGameObject && !(obj is TextGameObject))
                {
                    SpriteGameObject spriteGameobject = obj as SpriteGameObject;

                    // Depth is higher or equal to 0.9f meaning we are dealing with a UI element.
                    if (spriteGameobject.depth >= 0.9f) continue;

                    obj.Draw(gameTime, spriteBatch);
                }
                else if (!(obj is TextGameObject))
                {
                    obj.Draw(gameTime, spriteBatch);
                }
            }

            
            // Drawing UI elements.
            foreach (GameObject obj in children)
            {
                if (obj is TextGameObject) obj.Draw(gameTime, ExtendedGame.spriteBatchUI);
            
                if (obj is SpriteGameObject)
                {                 
                    SpriteGameObject spriteGameobject = obj as SpriteGameObject;

                    // Depth is lower or equal to 0.9f so this must not be a UI element.
                    if (spriteGameobject.depth < 0.9f) continue;

                    obj.Draw(gameTime, ExtendedGame.spriteBatchUI);
                }
                
            }
            
        }

        /// <summary>
        /// Performs the Reset method for all game objects in this GameObjectList.
        /// </summary>
        public override void Reset()
        {
            foreach (GameObject obj in children)
                obj.Reset();
        }
    }
}