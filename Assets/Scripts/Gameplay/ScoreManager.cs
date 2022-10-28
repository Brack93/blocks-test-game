using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreManager : BlocksDestroyedListener
{
    private TextMeshProUGUI scoreText;
    private float score;

    protected override void BlocksDestroyedHandler(int x)
    {
        score += (x - 1) * 80 + Mathf.Pow((x - 2f) / 5, 2);
        scoreText.text = GetRoundedScore().ToString();
    }

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "0";
    }

    private void OnDestroy()
    {
        SaveState();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveState();
    }

    private void SaveState()
    {
        int highscore = PlayerPrefs.GetInt(ScoreTags.Highscore.ToString(), 0);
        int roundedScore = GetRoundedScore();
        if (roundedScore > highscore)
        {
            PlayerPrefs.SetInt(ScoreTags.Highscore.ToString(), roundedScore);
        }
        PlayerPrefs.SetInt(ScoreTags.Score.ToString(), roundedScore);
    }

    private int GetRoundedScore()
    {
        return (int)Mathf.Round(score);
    }
}
