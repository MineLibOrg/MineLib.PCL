﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MineLib.PCL.Graphics.Screens.GUI.Text
{
    public sealed class BaseShadowText : GUIText
    {
        private Color TextShadowColor { get; set; }
        
        private Rectangle TextShadowRectangle { get; set; }


        public BaseShadowText(Client game, Screen screen, Rectangle textRect, string text, Color textColor, Color shadowColor) : base(game, screen, text, textRect, textColor)
        {
            TextShadowColor = shadowColor;

            TextShadowRectangle = new Rectangle(TextRectangle.X + 1, TextRectangle.Y + 1, TextRectangle.Width, TextRectangle.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);
            MainTextRenderer.DrawText(SpriteBatch, Text, TextShadowRectangle, TextShadowColor);
            MainTextRenderer.DrawText(SpriteBatch, Text, TextRectangle, TextColor);
            SpriteBatch.End();
        }
    }
}
