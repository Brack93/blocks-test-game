using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeManager : BlocksDestroyedListener
{
    private float totalAvailableTime;
    private float startTime;
    private TextMeshProUGUI timerText;

    protected override void BlocksDestroyedHandler(int x)
    {
        totalAvailableTime += 10 + 20 * Mathf.Pow((x - 2f) / 3, 2);
    }

    private void Start()
    {
        totalAvailableTime = BoardSettings.COUNTDOWN;
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = "";
        startTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        float timeLeft = totalAvailableTime - (Time.timeSinceLevelLoad - startTime);
        if (timeLeft <= 0)
        {
            SceneManager.Instance.LoadScene(Scenes.GameoverScene);
        }
        else
        {
            GetMinutesAndSeconds(timeLeft, out int minutes, out int seconds);
            timerText.text = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        }
    }

    private void GetMinutesAndSeconds(float inputSeconds, out int minutes, out int seconds)
    {
        float x = inputSeconds / 60f;
        minutes = (int)Mathf.Floor(x);
        seconds = (int)Mathf.Ceil((x - minutes) * 60);

        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
        }
    }

}
