using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SimonSays
{
    public class Game1 : Game
    {
        const int COUNT = 9;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Box[] Boxes = new Box[COUNT];
        InputManager inputManager = new InputManager();
        ScreenManager screenManager = new ScreenManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 392;
            graphics.PreferredBackBufferHeight = 392;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenManager.sf = Content.Load<SpriteFont>("Font");
            ScreenManager.Tex = Content.Load<Texture2D>("Box");

            Box.Tex = new Texture2D[2];
            Box.Tex[0] = Content.Load<Texture2D>("Unpressed");            
            Box.Tex[1] = Content.Load<Texture2D>("Bright");

            for (int i = 0; i < COUNT; i++)
            {
                Boxes[i] = new Box(i);
            }
            
        }
        protected override void UnloadContent()
        {           
        }
        protected override void Update(GameTime gameTime)
        {
            foreach (var box in Boxes)
            {
                box.Update();
            }            
            InputManager.Update();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var box in Boxes)
            {
                box?.Draw(spriteBatch);
            }
            screenManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
