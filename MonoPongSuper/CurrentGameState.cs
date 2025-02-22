using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper
{
    public class CurrentGameState
    {
        public static GameState currentState = GameState.Title;

        public static void GoToTitle()
        {
            currentState = GameState.Title;
        }
        public static void GoToGame() 
        {
            currentState = GameState.Game;
        }
        public static void PauseGame()
        {
            currentState = GameState.Pause;
        }
        public static void GameOver()
        {
            currentState = GameState.GameOver;
        }
    }
}
