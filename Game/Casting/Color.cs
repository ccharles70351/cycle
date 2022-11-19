using System.Collections.Generic;
using System;


namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A color.</para>
    /// <para>The responsibility of Color is to hold and provide information about itself. Color has 
    /// a few convenience methods for comparing and converting them.
    /// </para>
    /// </summary>
    public class Color
    {
        private int _red = 0;
        private int _green = 0;
        private int _blue = 0;
        private int _alpha = 255;
        private float _scaling = 1;

        /// <summary>
        /// Constructs a new instance of Color using the given red, green and blue values.
        /// </summary>
        /// <param name="red">The given red value (0-255).</param>
        /// <param name="green">The given green value (0-255).</param>
        /// <param name="blue">The given blue value (0-255).</param>
        public Color(int red, int green, int blue)
        {
            this._red = red;
            this._green = green;
            this._blue = blue;
        }

        /// <summary>
        /// Gets the color's alpha value.
        /// </summary>
        /// <returns>The alpha value.</returns>
        public int GetAlpha()
        {
            return _alpha;
        }

        /// <summary>
        /// Gets the color's blue value.
        /// </summary>
        /// <returns>The blue value.</returns>
        public int GetBlue()
        {
            return (int)Math.Round(_blue * _scaling);
        }

        /// <summary>
        /// Gets the color's green value.
        /// </summary>
        /// <returns>The green value.</returns>
        public int GetGreen()
        {
            return (int)Math.Round(_green * _scaling);
        }

        /// <summary>
        /// Gets the color's red value.
        /// </summary>
        /// <returns>The red value.</returns>
        public int GetRed()
        {
            return (int)Math.Round(_red * _scaling);
        }

        /// <summary>
        /// Gets the color's scale value.
        /// </summary>
        /// <returns>The scale value.</returns>
        public float GetScale()
        {
            return _scaling;
        }

        /// <summary>
        /// Scales the color from black to the user defined value
        /// </summary>
        /// <param name="scale">The amount from 0-1 to scale the color by.</param>
        public void ScaleColor(float scale)
        {
            _scaling = scale;
        }

    }
}