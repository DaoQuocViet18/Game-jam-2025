using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => {
            Loader.Instance.LoadWithFade(SceneName.SelectLevelScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}