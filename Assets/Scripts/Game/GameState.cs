using UnityEngine;

namespace Game
{
    public static class GameState
    {
        public static bool Paused
        {
            get { return _paused; }
        }

        public static bool HasFinished
        {
            get { return _hasFinished; }
        }

        private static bool _paused;
        private static bool _hasFinished;

        public static void Reset()
        {
            Time.timeScale = 1;
            _paused = false;
            _hasFinished = false;
        }
        
        public static void Pause()
        {
            _paused = true;
            Time.timeScale = 0;
        }

        public static void Unpause()
        {
            _paused = false;
            Time.timeScale = 1;
        }

        public static void Finishe()
        {
            _hasFinished = true;
        }
    }
}