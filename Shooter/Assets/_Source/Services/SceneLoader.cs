using _Source.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Services
{
    public class SceneLoader:MonoBehaviour
    {
        [SerializeField] private int idMainMenu;
        [SerializeField] private int idGame;
        [SerializeField] private int idFirstLvl;
        [SerializeField] private int idSecondLvl;
        [SerializeField] private int idThirdLvl;

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

        public void LoadFirsLvl()
        {
            InventoryPlayer.ClearInventory();
            SceneManager.LoadScene(idFirstLvl);
        }
        public void LoadSecondLvl()
        {
            InventoryPlayer.ClearInventory();
            SceneManager.LoadScene(idSecondLvl);
        }
        public void LoadThirdLvl()
        {
            InventoryPlayer.ClearInventory();
            SceneManager.LoadScene(idThirdLvl);
        }
    }
}