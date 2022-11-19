using Unit05.Game;
using Unit05.Game.Casting;
using Unit05.Game.Directing;
using Unit05.Game.Scripting;
using Unit05.Game.Services;


namespace Unit05
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();
            
            // we no longer need the food
            // cast.AddActor("food", new Food());

            Cycle cycle1 = new Cycle(1);
            Cycle cycle2 = new Cycle(2);
            cast.AddActor("cycle", cycle1);
            cast.AddActor("cycle", cycle2);

            Score score1 = new Score();
            Score score2 = new Score();
            score1.SetPosition(new Point(20, 0));
            score2.SetPosition(new Point(Constants.MAX_X - 80, 0));
            cast.AddActor("score", score1);
            cast.AddActor("score", score2);

            // create the services
            KeyboardService keyboardService = new KeyboardService();
            VideoService videoService = new VideoService(false);
           
            // create the script
            Script script = new Script();
            script.AddAction("input", new ControlActorsAction(keyboardService));
            script.AddAction("update", new MoveActorsAction());
            script.AddAction("update", new HandleCollisionsAction());
            script.AddAction("output", new DrawActorsAction(videoService));

            // start the game
            Director director = new Director(videoService);
            director.StartGame(cast, script);
        }
    }
}