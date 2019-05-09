using UnityEngine.SceneManagement;

namespace Help
{
    public static class LoadLevelHelper
    {
        public static void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        public static void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}