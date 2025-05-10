using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    [SerializeField] private Text lastScoreText;
    [SerializeField] private Text bestScoreText;
    void Start()
    {
        bestScoreText.text = PlayerPrefs.GetInt("maxScore", 0).ToString();
        lastScoreText.text = PlayerPrefs.GetInt("lastScore", 0).ToString();
    }
}
