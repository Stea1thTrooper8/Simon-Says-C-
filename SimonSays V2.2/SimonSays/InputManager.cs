using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimonSays
{
    public class InputManager
    {
                
        
        public static MouseState ms = new MouseState(), oms;
        public static KeyboardState ks = new KeyboardState(), oks;

        public static bool Press = false;       


        public static void Update()
        {
            oms = ms;
            oks = ks;

            ms = Mouse.GetState();
            ks = Keyboard.GetState();
            
        }
        
    }

}
