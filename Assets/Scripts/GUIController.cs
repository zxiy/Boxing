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
    [HideInInspector]public UnityEvent<GameObject, int> onScoreChanged = new UnityEvent<GameObject, int>();
    private List<GameObject> itemsInBox = new List<GameObject>();
    [SerializeField]private float shipAnimationTime;
    [SerializeField]private float shipAnimationDelay;
    [SerializeField]private Animator shipAnimator;
    [SerializeField]private List<Animator> flapAnimators;
    private bool frzzeTime = false;
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
        gameLevelConfig.passedTime = 0;
        frzzeTime = false;
    }
    
    private void Update()
    {
        if (frzzeTime)
        {
            shipButton.gameObject.SetActive(false);
            return;
        }
        timeText.text = (gameLevelConfig.currentLevel.timeLimit - gameLevelConfig.passedTime).ToString("0.00");
        if (gameLevelConfig.currentLevel.timeLimit - gameLevelConfig.passedTime < 0)
            SceneManager.LoadScene("FailureScene");
        if (score >= gameLevelConfig.currentLevel.targetScore)
        {
            shipButton.gameObject.SetActive(true);
        }
        else
        {
            shipButton.gameObject.SetActive(false); 
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
        //Load next level
        if (gameLevelConfig.NextLevel())
        {
            frzzeTime = true;
            //play close box animation
            foreach (var animator in flapAnimators)
            {
                animator.SetFloat("PlayTime", shipAnimationTime);
                animator.SetTrigger("Close");
            }
            foreach (var item in itemsInBox)
            {
                Destroy(item, shipAnimationTime);
            }
            StartCoroutine(shipAnimation(shipAnimationTime));
        }
        else
        {
            SceneManager.LoadScene("SuccessScene");
        }
        //clear all items in box
        
    }
    
    private IEnumerator shipAnimation(float time)
    {
        //play move box animation
        yield return new WaitForSeconds(time);
        shipAnimator.SetTrigger("Ship");
        StartCoroutine(moveBox(shipAnimationDelay));
    }

    private IEnumerator moveBox(float time)
    {
        yield return new WaitForSeconds(time);
        initialize();
    }

}
