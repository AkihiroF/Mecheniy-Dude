using _Source.Player;
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
            InventoryPlayer.ClearInventory();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            SceneManager.LoadScene(idGame);
        }
        public void LoadGame()
        {
            InventoryPlayer.ClearInventory();
            SceneManager.LoadScene(idGame);
        }

        public void LoadMainMenu()
        {
            InventoryPlayer.ClearInventory();
            SceneManager.LoadScene(idMainMenu);
        }

        public void QuitGame()
        {
            InventoryPlayer.ClearInventory();
            Application.Quit();
        }
    }
}