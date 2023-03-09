using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField]TMP_Text scoreText;
    [SerializeField]TMP_Text targetScoreText;
    [SerializeField]TMP_Text timeText;
    [SerializeField]Button shipButton;
    
    [SerializeField]private GameLevelConfig gameLevelConfig;
    private int score;
    [HideInInspector] public UnityEvent<GameObject, int> onScoreChanged = new UnityEvent<GameObject, int>();
    private List<GameObject> itemsInBox = new List<GameObject>();
    private void Start()
    {
        initialize();
        onScoreChanged.AddListener(ItemIntoBox);
        onScoreChanged.AddListener(ItemOutBox);
    }

    private void initialize()
    {
        targetScoreText.text = gameLevelConfig.currentLevel.targetScore.ToString();
        score = 0;
        scoreText.text = score.ToString();
    }
    
    private void Update()
    {
        timeText.text = (gameLevelConfig.currentLevel.timeLimit - gameLevelConfig.passedTime).ToString("0.00");
        if (gameLevelConfig.currentLevel.timeLimit - gameLevelConfig.passedTime < 0)
            SceneManager.LoadScene("FailureScene");
        if (score >= gameLevelConfig.currentLevel.targetScore)
        {
            shipButton.gameObject.SetActive(true);
        }
    }
    
    public void ItemIntoBox(GameObject gameObject, int _score)
    {
        if (_score > 0)
        {
            score += _score;
            scoreText.text = score.ToString();
            itemsInBox.Add(gameObject);
        }
    }
    
    public void ItemOutBox(GameObject gameObject, int _score)
    {
        if (_score < 0)
        {
            score += _score;
            scoreText.text = score.ToString();
            itemsInBox.Remove(gameObject);
        }
    }

    public void ShipButtonClicked()
    {
        //TODO Play animation
        
        //Load next level
        if (gameLevelConfig.NextLevel())
        {
            initialize();
        }
        else
        {
            SceneManager.LoadScene("SuccessScene");
        }
        //clear all items in box
        foreach (var item in itemsInBox)
        {
            Destroy(item, 0.05f);
        }
    }

}
