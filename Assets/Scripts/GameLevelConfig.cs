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

    private float timeSinceLastSpawn;

    int itemToInstantiate;

    [HideInInspector]public int currentLevelIndex;
    [HideInInspector]public GameLevel currentLevel;
    [HideInInspector]public float passedTime;

    private int generatedItemIndex;
    private void Awake()
    {
        timeSinceLastSpawn = 0;
        currentLevelIndex = 0;
        passedTime = 0;
        currentLevel = gameLevels[currentLevelIndex];
        itemToInstantiate = UnityEngine.Random.Range(0, currentLevel.items.Length);
        print("Item to spawn = " + itemToInstantiate);
    }
    
    private void Update()
    {
        passedTime += Time.deltaTime;
        // check if we need to generate next item
        if (Time.fixedTime - timeSinceLastSpawn >= currentLevel.items[itemToInstantiate].generateTime)
        {
            // generate selected item
            GameObject item = Instantiate(currentLevel.items[itemToInstantiate].prefab, 
                itemGeneratePosition.transform.position, Quaternion.identity);
            // select new item to spawn
            itemToInstantiate = UnityEngine.Random.Range(0, currentLevel.items.Length);
            // set the time the item spawned
            timeSinceLastSpawn = Time.fixedTime;
            print("Time of spawn = " + timeSinceLastSpawn);
        }
    }
    
    public bool NextLevel()
    {
        if (currentLevelIndex >= gameLevels.Length - 1)
            return false;
        currentLevelIndex++;
        currentLevel = gameLevels[currentLevelIndex];
        passedTime = 0;
        return true;
    }
}
