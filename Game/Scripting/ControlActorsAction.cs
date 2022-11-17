using Unit05.Game.Casting;
using Unit05.Game.Services;
using System.Collections.Generic;

namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the snake.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the snake's head.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService _keyboardService;
        private Point _direction = new Point(Constants.CELL_SIZE, 0);

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService)
        {
            this._keyboardService = keyboardService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            for (int i = 0; i < Constants.CONTROL.Count; i++)
            {
                List<string> controls = Constants.CONTROL[i];

                // left
                if (_keyboardService.IsKeyDown(controls[0]))
                {
                    _direction = new Point(-Constants.CELL_SIZE, 0);
                }

                // right
                if (_keyboardService.IsKeyDown(controls[1]))
                {
                    _direction = new Point(Constants.CELL_SIZE, 0);
                }

                // up
                if (_keyboardService.IsKeyDown(controls[2]))
                {
                    _direction = new Point(0, -Constants.CELL_SIZE);
                }

                // down
                if (_keyboardService.IsKeyDown(controls[3]))
                {
                    _direction = new Point(0, Constants.CELL_SIZE);
                }

                Cycle cycle = (Cycle)cast.GetActors("cycle")[i];
                cycle.TurnHead(_direction);

            }
        }
    }
}