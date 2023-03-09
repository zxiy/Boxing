using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelConfig : MonoBehaviour
{
    [Serializable]
    public class ItemConfig
    {
        public string name;
        public GameObject prefab;
        public int value;
        public float generateTime;
    }
    
    [Serializable]
    public class GameLevel
    {
        public string name;
        public int timeLimit;
        public int targetScore;
        public ItemConfig[] items;
    }
    
    [SerializeField] private GameObject itemGeneratePosition;
    
    public GameLevel[] gameLevels;

    private float passedTime;
    
    private GameLevel currentLevel;

    private int generatedItemIndex;
    private void Start()
    {
        passedTime = 0;
        currentLevel = gameLevels[0];
        generatedItemIndex = 0;
        // sort items by generateTime
        Array.Sort(currentLevel.items, (a, b) => a.generateTime.CompareTo(b.generateTime));
    }
    
    private void Update()
    {
        passedTime += Time.deltaTime;
        // check if we need to generate next item
        if (generatedItemIndex < currentLevel.items.Length && passedTime >= currentLevel.items[generatedItemIndex].generateTime)
        {
            // generate item
            GameObject item = Instantiate(currentLevel.items[generatedItemIndex].prefab, 
                itemGeneratePosition.transform.position, Quaternion.identity);
            // increase generatedItemIndex
            generatedItemIndex++;
        }
    }
}
