﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineLib.PCL.Graphics.Map;

namespace MineLib.PCL.Graphics.Components
{
    public class FPSCounterComponent : DrawableGameComponent
    {
        readonly SpriteBatch _spriteBatch;
        readonly SpriteFont _spriteFont;

        int _frameRate;
        int _frameCounter;
        TimeSpan _elapsedTime = TimeSpan.Zero;


        public FPSCounterComponent(Game game, SpriteBatch batch, SpriteFont font) : base(game)
        {
            _spriteFont = font;
            _spriteBatch = batch;
        }
        
        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime > TimeSpan.FromSeconds(1))
            {
                _elapsedTime -= TimeSpan.FromSeconds(1);
                _frameRate = _frameCounter;
                _frameCounter = 0;
            }
        }
        
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _frameCounter++;

            string fps = string.Format("FPS  : {0}",                        _frameRate);
            string ram = string.Format("RAM : {0} (KB)",                    GC.GetTotalMemory(false) / 1024);
            string gpu = string.Format("Chunks : {0}",                 Client.Chunks);
            string opa = string.Format("Opaque Sections : {0}",        WorldVBO.DrawingOpaqueSections);
            string tra = string.Format("Transparent Sections : {0}",   WorldVBO.DrawingTransparentSections);

            DrawString(_spriteBatch, _spriteFont, Color.Black, fps, new Rectangle(1, 1, Game.GraphicsDevice.DisplayMode.Width, 30));
            DrawString(_spriteBatch, _spriteFont, Color.White, fps, new Rectangle(0, 0, Game.GraphicsDevice.DisplayMode.Width, 30));

            DrawString(_spriteBatch, _spriteFont, Color.Black, ram, new Rectangle(1, 31, Game.GraphicsDevice.DisplayMode.Width, 30));
            DrawString(_spriteBatch, _spriteFont, Color.White, ram, new Rectangle(0, 30, Game.GraphicsDevice.DisplayMode.Width, 30));

            DrawString(_spriteBatch, _spriteFont, Color.Black, gpu, new Rectangle(1, 61, Game.GraphicsDevice.DisplayMode.Width, 30));
            DrawString(_spriteBatch, _spriteFont, Color.White, gpu, new Rectangle(0, 60, Game.GraphicsDevice.DisplayMode.Width, 30));

            DrawString(_spriteBatch, _spriteFont, Color.Black, opa, new Rectangle(1, 91, Game.GraphicsDevice.DisplayMode.Width, 30));
            DrawString(_spriteBatch, _spriteFont, Color.White, opa, new Rectangle(0, 90, Game.GraphicsDevice.DisplayMode.Width, 30));

            DrawString(_spriteBatch, _spriteFont, Color.Black, tra, new Rectangle(1, 121, Game.GraphicsDevice.DisplayMode.Width, 30));
            DrawString(_spriteBatch, _spriteFont, Color.White, tra, new Rectangle(0, 120, Game.GraphicsDevice.DisplayMode.Width, 30));


            _spriteBatch.End();
        }

        private static void DrawString(SpriteBatch spriteBatch, SpriteFont font, Color color, string strToDraw, Rectangle boundaries)
        {
            var size = font.MeasureString(strToDraw);

            var xScale = (boundaries.Width / size.X);
            var yScale = (boundaries.Height / size.Y);

            // Taking the smaller scaling value will result in the text always fitting in the boundaries.
            var scale = Math.Min(xScale, yScale);

            var position = new Vector2 { X = boundaries.X, Y = boundaries.Y };
            
            // Draw the string to the sprite batch!
            spriteBatch.DrawString(font, strToDraw, position, color, 0.0f, new Vector2(0, 0), scale, new SpriteEffects(), 0.0f);
        }
    }
}
