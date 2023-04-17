using _Source.Saving_System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Services
{
    public class SceneLoader:MonoBehaviour
    {
        [SerializeField] private int idMainMenu;
        [SerializeField] private int idGame;

        public void LoadNewGame()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(idGame);
        }
        public void LoadGame()
        {
            SceneManager.LoadScene(idGame);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(idMainMenu);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}