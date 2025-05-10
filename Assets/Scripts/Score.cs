using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    public int score;
    private int _maxScore;
    void Start()
    {
        _maxScore = PlayerPrefs.GetInt("maxScore", 0);
        score = 0;
        scoreText.text = score.ToString();
        maxScoreText.text = _maxScore.ToString();
        PlayerPrefs.SetInt("lastScore", score);
    }

    public void IncreaseScore(int value)
    {
        score += value;

        if (score > _maxScore)
        {
            _maxScore = score;
            PlayerPrefs.SetInt("maxScore", _maxScore);
        }

        scoreText.text = score.ToString();
        maxScoreText.text = _maxScore.ToString();
        
        PlayerPrefs.SetInt("lastScore", score);
    }
}
