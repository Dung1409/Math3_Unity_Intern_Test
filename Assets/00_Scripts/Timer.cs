using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float timer;
    TextMeshProUGUI txtTimer;

    void Awake()
    {
        timer = 60;
        txtTimer = GetComponent<TextMeshProUGUI>();
        ShowTimer(0f);
    }

    void OnEnable()
    {
        timer = 60;
    }

    void ShowTimer(float time)
    {
        timer -= time;
        if (timer <= 0)
        {
            timer = 0;
        }
        txtTimer.text = Mathf.Floor(timer).ToString();
    }

    void Update()
    {
        if (timer <= 0) return;
        ShowTimer(Time.deltaTime);
    }



}
