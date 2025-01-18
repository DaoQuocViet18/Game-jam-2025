using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float levelTimeMax;

    [SerializeField] private int MaxPoint = 4;
    [SerializeField] private int currentPoint = 0;

    [SerializeField] Level[] levels;
    [SerializeField] static int maxCurrentLevel = 0;
    [SerializeField] static int currentLevel = 0;

    public static int MaxCurrentLevel { get => maxCurrentLevel; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    // [SerializeField] private float levelTimeMax;

    private void OnEnable()
    {
        // Đăng ký sự kiện OnLoseGame
        EventDispatcher.Add<EventDefine.OnIncreasePoint>(onIncreasePoint);
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị hủy
        EventDispatcher.Remove<EventDefine.OnIncreasePoint>(onIncreasePoint);
    }



    private void Start()
    {
        //AudioManager.Instance.PlayMusic(GameAudioClip.BGM_PLAYING, -10f);

        onLoadLevel(currentLevel);
    }

    private void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySound(GameAudioClip.POP_SOUND_EFFECT);
        }
    }

    public void onLoadLevel(int levelindex)
    {
        if (currentLevel <= maxCurrentLevel)
        {
            Debug.Log("load level" + levelindex);
            currentLevel = levelindex;
        }

        MaxPoint = levels[currentLevel].MaxPoint;
        currentPoint = 0;

        foreach (var level in levels)
        {
            Debug.Log("level: " + level);
            level.gameObject.SetActive(false);
        }

        levels[currentLevel].gameObject.SetActive(true);
        EventDispatcher.Dispatch(new EventDefine.OnUpdateProgressBar());
    }

    public void onLoadLevelAndDisableLevels(int levelindex)
    {
        if (currentLevel <= maxCurrentLevel)
        {
            Debug.Log("load level" + levelindex);
            currentLevel = levelindex;
        }

        MaxPoint = levels[currentLevel].MaxPoint;
        currentPoint = 0;

        foreach (var level in levels)
        {
            Debug.Log("level: " + level);
            level.gameObject.SetActive(false);
        }

        EventDispatcher.Dispatch(new EventDefine.OnUpdateProgressBar());
    }


    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public string getCurrentHintText()
    {
        switch (currentLevel)
        {
            case 0: return "Hình như trong ống nước có gì đó, đập vỡ nó ra xem sao?";
            case 1: return "Tạo vũng nước từ cái chậu nước của bà kia đi rồi kéo cậu bé vào xem nào, có thế cũng không làm được.";
            case 2: return "Chai bia cuối cùng, hãy để bạn bong bóng nhỏ nốc cạn đêm nay nào!";
        }

        return "";
    }

    public float getCurrenProgress()
    {
        return (float)currentPoint / MaxPoint;
    }

    private void onIncreasePoint(IEventParam param)
    {
        currentPoint++;
        if (currentPoint == MaxPoint)
        {
            activeWinGame();
        }

        EventDispatcher.Dispatch(new EventDefine.OnUpdateProgressBar());
    }

    void activeWinGame()
    {
        if (currentLevel == maxCurrentLevel && maxCurrentLevel < levels.Length - 1)
        {
            maxCurrentLevel++;
        }
        Debug.Log("MaxCurrentLevel: " + maxCurrentLevel);
        Debug.Log("levels.Length: " + levels.Length);
        levels[currentLevel].onWinGame(() =>
        {
            if (currentLevel + 1 == levels.Length)
            {
                Loader.Instance.LoadWithFade(SceneName.EndingScene);
            }
            else
            {
                EventDispatcher.Dispatch(new EventDefine.OnWinGame());
            }
        });
    }


    void setDefualtPoint()
    {
        currentPoint = 0;
    }
}
