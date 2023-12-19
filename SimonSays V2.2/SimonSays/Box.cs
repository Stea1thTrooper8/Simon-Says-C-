using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;

namespace SimonSays
{
    public class Box
    {        
        public static Texture2D[] Tex; //Texture of square //0= unpressed  1=Star Press
        public static List<int> PlayList = new List<int>(); // used to hold pattern
        public static List<string> Highscores = new List<string>();

        Rectangle Position = new Rectangle(0, 0, 128, 128);  
        
        public static int CurrentPlay = -1; // Telling the pattern to start and keeps pattern
        public static int Timer = -1; // Timer for each square
        public static int Room = 1; // count of levels/rooms         
        public static int GotRight = 0; // User gets Pattern correct
        public static int SequenceTimer = -1;
        public static int ScreenDelay = 30;
        public static int totalPoints = 0;// used to add all points for high score        
        public static int firstScore = 1200;        
        public static int secondScore = 850;        
        public static int thirdScore = 760;

        public static bool CanPlay = false; 
        public static bool ScreenOff = false; //Toggles screen between the levels
        public static bool GotWrong = false; // sends to lose screen
        public static bool NameInput = false; // controls when screen will go white to input winners name
        public static bool Init = false;

        int Press = 0; // Changing TEX for square
        int Index; // number tied to squares 0 to 8
        int ClickTimer = -1; // timers for user clicks
        int RoomWin = 0; // if user passes room   
        int number;// used to grab rand numbers and put in list
        static int boxPoints = 0;//used to track points gained by clickeing on each corrrect box 10 per box
        static int levelPoints = 0;// used to track points gained by clearing a level 100 per level
        string path = @"C:\Users\rober\Desktop\Four_Games_Atrifact_Project\SimonSays V2.2\Highscores.txt";
        



        Color[] colors =
        {
            new Color(255,0,0), new Color(255,255,0), new Color(0, 255, 255),
            new Color(0,0,255), new Color(0,255,0), new Color(255,0,255),
            new Color(255,69,0), new Color(153,50,204), new Color(26,148,49)
        };

        public Box(int index)
        {
            Position.X = index % 3 * 132;
            Position.Y = index / 3 * 132;
            Index = index;
        }  


        public void Update()
        {
            if (!File.Exists(path))
            {
                // Create a file to write to.
                WriteFile();
            }
            

            //for highscores to be read from file
            Highscores = File.ReadLines(path).ToList();
            firstScore = Convert.ToInt32(Highscores[0]);
            secondScore = Convert.ToInt32(Highscores[1]);
            thirdScore= Convert.ToInt32(Highscores[2]);
            

            if (ScreenOff == false && InputManager.ks.IsKeyDown(Keys.Space) 
                && InputManager.oks.IsKeyUp(Keys.Space) && GotWrong == false)
            {
                ScreenOff = true;                
                SequenceTimer = 60;                
            }

            if(RoomWin == 1)
            {                    
                if(ScreenDelay-- == 0)
                {
                    ScreenOff = false;                    
                    RoomWin = 2;
                    ScreenDelay = 30;
                }
            }

            if(InputManager.ks.IsKeyDown(Keys.Enter) && InputManager.oks.IsKeyUp(Keys.Enter))
            {
                Room = 1;
                GotRight = 0;
                GotWrong = false;
                ScreenOff = false;
                boxPoints = 0;
                levelPoints = 0;
            }

            if(RoomWin == 1 || ScreenOff == false)
            {                 
                CanPlay = false;
            }

            if (Room == 1 && SequenceTimer >= 0 && ScreenOff == true)
            {
                if (SequenceTimer-- == 0)
                {
                    Level();
                    CurrentPlay = 0;
                    Room++;
                }
            }            

            if (RoomWin == 2 && SequenceTimer >= 0 && ScreenOff == true)
            {
                if (SequenceTimer-- == 0)
                {
                    RoomWin = 0;
                    Level();
                    CurrentPlay = 0;
                    Room++;
                }
            }
            
            Sequence();
            Clicked();
            
            
            /*if(NameInput == true)
            {
                NameWrite();
            }*/
            

        }               

        public void Level()
        {
            PlayList.Clear();
            if (PlayList.Count < Room)
            {
                Random rnd = new Random(); // creating rand numbers for Playlist
                for (int i = 0; i < Room; i++)
                {                    
                    number = rnd.Next(0, 9);
                    PlayList.Add(number);
                }
            }            
        }

        public void Clicked()
        {
            if (InputManager.ks.IsKeyDown(Keys.Tab))//cheat
            {
                boxPoints++;
            }
            if (InputManager.ms.LeftButton == ButtonState.Pressed && InputManager.oms.LeftButton != ButtonState.Pressed &&
                Position.Contains(InputManager.ms.Position) && CanPlay == true)
            {
                Press = 1;                
                ClickTimer = 20;
                if(Index == PlayList[GotRight])
                {
                    GotRight++;
                    boxPoints++;
                    if(GotRight == PlayList.Count)
                    {
                        RoomWin = 1;
                        GotRight = 0;
                        levelPoints++;
                        totalPoints = ((boxPoints * 10) + (levelPoints * 100));
                    }
                }
                else
                {                    
                    GotWrong = true;
                    totalPoints = ((boxPoints * 10) + (levelPoints * 100));
                    if(totalPoints > firstScore)
                    {
                        thirdScore = secondScore;
                        secondScore = firstScore;
                        firstScore = totalPoints;                        
                    }
                    else if(totalPoints > secondScore)
                    {
                        thirdScore = secondScore;
                        secondScore = totalPoints;                                                                             
                    }
                    else if(totalPoints > thirdScore)
                    {
                        thirdScore = totalPoints;                        
                    }
                    WriteFile();
                }                
            }

            if (ClickTimer-- == 0 && Press == 1)
            {
                Press = 0;
                ClickTimer = -1;
            }           
            
        }

        public void Sequence()
        {
            
            if (CurrentPlay >= 0 && PlayList[CurrentPlay] == Index && Timer == -1)
            {
                Press = 1;
                Timer = 30;
            }

            if (Press == 1 && Timer >= 0)
            {
                if (Timer-- == 0)
                {
                    Press = 0;
                    CurrentPlay++;
                }
            }

            if (CurrentPlay == PlayList.Count)
            {
                CurrentPlay = -1;
                CanPlay = true;
                Timer = -1;
            }
        }

        public void WriteFile()
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                // Create a file to write to.
                sw.WriteLine(firstScore);                
                sw.WriteLine(secondScore);                
                sw.WriteLine(thirdScore);
            }
        }   

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex[Press], Position, colors[Index]);            
        }
    }
}
