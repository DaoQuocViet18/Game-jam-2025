using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button[] levelButtons; // Mảng các nút level

    private void Awake()
    {
        quitButton.onClick.AddListener(() => {
            Loader.Instance.LoadWithFade(SceneName.MainMenuScene);
        });

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Lưu trữ chỉ số level
            levelButtons[i].onClick.AddListener(() => {
                Loader.Instance.LoadWithFade((SceneName)System.Enum.Parse(typeof(SceneName), "Level" + levelIndex));
            });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                levelButtons[i].interactable = false;
        }
    }
}