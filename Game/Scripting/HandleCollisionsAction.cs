using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleFoodCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleFoodCollisions(Cast cast)
        {
            List<Actor> cycles = cast.GetActors("cycle");
            List<Actor> scores = cast.GetActors("score");
            Food food = (Food)cast.GetFirstActor("food");

            for (int i = 0; i < cycles.Count; i++)
            {
                if (((Cycle)cycles[i]).GetHead().GetPosition().Equals(food.GetPosition()))
                {
                    int points = food.GetPoints();
                    ((Cycle)cycles[i]).GrowTail(points);
                    ((Score)scores[i]).AddPoints(points);
                    food.Reset();
                }
            }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            List<Actor> cycles = cast.GetActors("cycle");
            List<Actor> segments = new List<Actor>();

            foreach (Cycle cycle in cycles)
            {
                segments.AddRange(cycle.GetBody());
            }

            for (int i = 0; i < segments.Count; i++)
            {
                foreach (Cycle cycle in cycles)
                {
                    if (segments[i].GetPosition().Equals(cycle.GetHead().GetPosition()))
                    {
                        _isGameOver = true;
                    }
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                List<Actor> cycles = cast.GetActors("cycle");
                List<Actor> segments = new List<Actor>();

                foreach (Cycle cycle in cycles)
                {
                    segments.AddRange(cycle.GetSegments());
                }
                
                Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }
                
                food.SetColor(Constants.WHITE);
            }
        }

    }
}