using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float levelTimeMax;

    [SerializeField] private int MaxPoint = 4;

    [SerializeField] Level[] levels;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] static int currentLevel = 0;

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
            AudioManager.Instance.PlaySound(GameAudioClip.POP);
        }
    }

    public void onLoadLevel(int levelindex)
    {
        Debug.Log("load level" + levelindex);
        currentLevel = levelindex;
        MaxPoint = levels[currentLevel].MaxPoint;

        foreach (var level in levels)
        {
            level.gameObject.SetActive(false);
        }

        levels[currentLevel].gameObject.SetActive(true);
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    private void onIncreasePoint(IEventParam param)
    {
        currentPoint++;
        if (currentPoint == MaxPoint)
        {
            activeWinGame();
        }
    }

    void activeWinGame()
    {
        levels[currentLevel].onWinGame(() =>
        {
            EventDispatcher.Dispatch(new EventDefine.OnWinGame());
        });
    }

    void setDefualtPoint()
    {
        currentPoint = 0;
    }
}
