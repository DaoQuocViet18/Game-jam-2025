using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float levelTimeMax;

    [SerializeField] private int MaxPoint = 4;

    [SerializeField] Level[] level;
    [SerializeField] private int currentPoint = 0;
    int currentLevel = 0;

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

        onLoadLevel(0);
    }

    private void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySound(GameAudioClip.POP);
        }
    }

    void onLoadLevel(int levelindex)
    {
        Debug.Log("load level" + levelindex);
        currentLevel = levelindex;
        MaxPoint = level[currentLevel].MaxPoint;
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
        EventDispatcher.Dispatch(new EventDefine.OnWinGame());
        Debug.Log("Phát sự kiện chiến thắng.");
    }

    void setDefualtPoint()
    {
        currentPoint = 0;
    }
}
