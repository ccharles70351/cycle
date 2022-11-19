using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;
using Unit05.Game.Scripting;


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
        private bool _isGameOver;
        private int _gameOverTimer;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
            Reset();
        }

        // <inheritdoc/>
        public void Reset()
        {
            _isGameOver = false;
            _gameOverTimer = 0;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
            else
            {
                if(_gameOverTimer == 0)
                {
                    // Remove all actors we don't need for the restart
                    List<Actor> cycles = cast.GetActors("cycle");
                    List<Actor> messages = cast.GetActors("messages");
                    foreach (Actor cycle in cycles)
                    {
                        cast.RemoveActor("cycle", cycle);
                    }
                    foreach (Actor message in messages)
                    {
                        cast.RemoveActor("messages", message);
                    }

                    // Reset directions and add cycle again
                    List<Action> inputActions = script.GetActions("input");
                    foreach (Action action in inputActions)
                    {
                        action.Reset();
                    }
                    cast.AddActor("cycle", new Cycle(1));
                    cast.AddActor("cycle", new Cycle(2));

                    // Reset collision handling script
                    Reset();
                }
                else
                {
                    _gameOverTimer--;

                    List<Actor> messages = cast.GetActors("messages");
                    foreach (Actor message in messages)
                        {
                            message.SetText($"Game Over!\nWe Will Restart in {(_gameOverTimer / Constants.FRAME_RATE) + 1}...");
                        }
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
            List<Actor> scores = cast.GetActors("score");
            List<Actor> segments = new List<Actor>();

            foreach (Cycle cycle in cycles)
            {
                segments.AddRange(cycle.GetBody());
            }

            foreach (Actor segment in segments)
            {
                for (int i = 0; i < cycles.Count; i++)
                {
                    if (segment.GetPosition().Equals(((Cycle)cycles[i]).GetHead().GetPosition()))
                    {
                        _isGameOver = true;
                        for (int j = 0; j < cycles.Count; j++)
                        {
                        ((Score)scores[j]).AddPoints((i == j) ? 0: 1);
                        }
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

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!\nWe Will Restart in 3...");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }

                // set the gameover timer to 3 seconds
                _gameOverTimer = 3 * Constants.FRAME_RATE;
            }
        }

    }
}