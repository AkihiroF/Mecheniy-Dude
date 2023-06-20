using _Source.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.UI
{
    public class UiMainMenuPreviewer : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private Button startNewGameButton;


        private void Awake()
        {
            BindButtons();
        }

        private void BindButtons()
        {
            startNewGameButton.onClick.AddListener(() => sceneLoader.LoadNewGame());
        }
        
        
    }
}