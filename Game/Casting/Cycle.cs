using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A long Bike.</para>
    /// <para>The responsibility of Cycle is to move itself.</para>
    /// </summary>
    public class Cycle : Actor
    {
        private List<Actor> _segments = new List<Actor>();
        private int _counter = 0;
        private int _cycleNum = 0;

        /// <summary>
        /// Constructs a new instance of a Cycle.
        /// </summary>
        public Cycle(int cycleNum)
        {
            _cycleNum = cycleNum;
            PrepareBody();
        }

        /// <summary>
        /// Gets the cycle's body segments.
        /// </summary>
        /// <returns>The body segments in a List.</returns>
        public List<Actor> GetBody()
        {
            return new List<Actor>(_segments.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the cycle's head segment.
        /// </summary>
        /// <returns>The head segment as an instance of Actor.</returns>
        public Actor GetHead()
        {
            return _segments[0];
        }

        /// <summary>
        /// Gets the cycle's segments (including the head).
        /// </summary>
        /// <returns>A list of cycle segments as instances of Actors.</returns>
        public List<Actor> GetSegments()
        {
            return _segments;
        }

        /// <summary>
        /// Grows the cycle's tail by the given number of segments.
        /// </summary>
        /// <param name="numberOfSegments">The number of segments to grow.</param>
        public void GrowTail(int numberOfSegments)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                Actor tail = _segments.Last<Actor>();
                Point velocity = tail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = tail.GetPosition().Add(offset);
                Color tailColor = tail.GetColor();

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetColor(tailColor);
                segment.SetText("#");
                _segments.Add(segment);

                if (tailColor != Constants.WHITE)
                {
                    ColorBody();
                }

            }
        }

        /// <inheritdoc/>
        public override void MoveNext()
        {
            foreach (Actor segment in _segments)
            {
                segment.MoveNext();
            }

            for (int i = _segments.Count - 1; i > 0; i--)
            {
                Actor trailing = _segments[i];
                Actor previous = _segments[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }

            if (_counter % 60 == 0) {
                GrowTail(1);
            }
            _counter ++;
        }

        /// <summary>
        /// Turns the head of the cycle in the given direction.
        /// </summary>
        /// <param name="velocity">The given direction.</param>
        public void TurnHead(Point direction)
        {
            _segments[0].SetVelocity(direction);
        }

        /// <summary>
        /// Prepares the cycle body for moving.
        /// </summary>
        private void PrepareBody()
        {
            int x = (Constants.MAX_X / 4) * (1 + (_cycleNum - 1) * 2);
            int y = Constants.MAX_Y / 2;

            for (int i = 0; i < Constants.CYCLE_LENGTH; i++)
            {
                Point position = new Point(x - i * Constants.CELL_SIZE, y);
                Point velocity = new Point(1 * Constants.CELL_SIZE, 0);
                string text = i == 0 ? "8" : "#";

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText(text);
                _segments.Add(segment);
            }

            ColorBody();
        }

        /// <summary>
        /// Colors the cycle body.
        /// </summary>
        private void ColorBody()
        {

            for (int i = 0; i < _segments.Count; i++)
            {
                Color color = (_cycleNum == 1) ? 
                    (i == 0) ? Constants.YELLOW : Constants.GREEN: 
                    (i == 0) ? Constants.PINK : Constants.PURPLE;

                int red = color.GetRed();
                int green = color.GetGreen();
                int blue = color.GetBlue();

                color = new Color(red, green, blue);
                float colorScale = (_segments.Count - i) / (float)_segments.Count;
                
                color.ScaleColor(colorScale);
                _segments[i].SetColor(color);
            }

        }
    }
}