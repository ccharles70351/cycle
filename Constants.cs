using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Unit05.Game.Casting;

namespace Unit05.Game
{
    /// <summary>
    /// <para>A tasty item that snakes like to eat.</para>
    /// <para>
    /// The responsibility of Food is to select a random position and points that it's worth.
    /// </para>
    /// </summary>
    public class Constants
    {
        public static int COLUMNS = 40;
        public static int ROWS = 20;
        public static int CELL_SIZE = 15;
        public static int MAX_X = 900;
        public static int MAX_Y = 600;

        public static int FRAME_RATE = 15;
        public static int FONT_SIZE = 15;
        public static string CAPTION = "Cycle";
        public static int CYCLE_LENGTH = 8;
        public static List<List<string>> CONTROL = new List<List<string>>{
            new List<string>{"a", "d", "w", "s"},
            new List<string>{"j", "l", "i", "k"}
        };
        public static Color RED = new Color(255, 0, 0);
        public static Color WHITE = new Color(255, 255, 255);
        public static Color YELLOW = new Color(255, 255, 0);
        public static Color GREEN = new Color(0, 255, 0);
        public static Color PINK = new Color(255, 125, 125);
        public static Color PURPLE = new Color(255, 0, 255);

    }
}

