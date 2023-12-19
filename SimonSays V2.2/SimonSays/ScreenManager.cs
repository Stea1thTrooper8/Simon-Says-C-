using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimonSays
{
    class ScreenManager
    {
        public static Texture2D Tex;
        public SpriteFont sf;
        
        public void Draw(SpriteBatch sb)
        {
            if (Box.ScreenOff == false)
            {
                sb.Draw(Tex, new Rectangle (0,0,392,392), Color.Aqua);
                if(Box.Room <= 1)
                {
                    sb.DrawString(sf, "Welcome to Simon Says", new Vector2(45, 50), Color.Black);
                    sb.DrawString(sf, "The HighScore to Beat is: ", new Vector2(40, 100), Color.Black);
                    sb.DrawString(sf, Box.firstScore.ToString(), new Vector2(160, 150), Color.Black);

                }
                else
                {
                    sb.DrawString(sf, "Current Score", new Vector2(100, 100), Color.Black);
                    sb.DrawString(sf, (Box.totalPoints).ToString(), new Vector2(160, 150), Color.Black);
                }
                sb.DrawString(sf, "Hit Spacebar", new Vector2(115, 300), Color.Black); 
                sb.DrawString(sf, "To Start Next Level", new Vector2(80, 340), Color.Black);
                sb.DrawString(sf, "Level:", new Vector2(140, 250), Color.Black);
                sb.DrawString(sf, Box.Room.ToString(), new Vector2(220, 250), Color.Black);                
            }
            if (Box.GotWrong == true)
            {
                sb.Draw(Tex, new Rectangle(0, 0, 392, 392), Color.Aqua);
                sb.DrawString(sf, "You Lost", new Vector2(130, 50), Color.Black);
                sb.DrawString(sf, "You Got To Room", new Vector2(50, 100), Color.Black);
                sb.DrawString(sf, (Box.Room-1).ToString(), new Vector2(290, 100), Color.Black);                
                sb.DrawString(sf, "Your Score this round: ", new Vector2(30, 150), Color.Black);
                sb.DrawString(sf, (Box.totalPoints).ToString(), new Vector2(310, 150), Color.Black);
                sb.DrawString(sf, "First Place: ", new Vector2(90, 220), Color.Black);
                sb.DrawString(sf, (Box.firstScore).ToString(), new Vector2(240, 220), Color.Black);
                sb.DrawString(sf, "Second Place: ", new Vector2(60, 260), Color.Black);
                sb.DrawString(sf, (Box.secondScore).ToString(), new Vector2(240, 260), Color.Black);
                sb.DrawString(sf, "Third Place: ", new Vector2(90, 300), Color.Black);
                sb.DrawString(sf, (Box.thirdScore).ToString(), new Vector2(240, 300), Color.Black);
                sb.DrawString(sf, "Press Enter To Restart", new Vector2(60, 350), Color.Black);
            }
        }
    }
}
