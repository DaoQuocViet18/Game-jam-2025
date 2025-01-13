using UnityEngine;

public class TimerManager : Singleton<TimerManager> {
    [SerializeField] private float timerMax = 10f; 
    private float timer;

    private void Start() {
        timer = timerMax;
    }

    private void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            timer = timerMax;
            Debug.Log("TIME UP!");
            EventDispatcher.Dispatch(new EventDefine.OnLoseGame());
        }
    } 

    public void SetTimerMax(float timerMax) {
        this.timerMax = timerMax;
    }

    public float GetCurrentTime() {
        return timer;
    }
}
