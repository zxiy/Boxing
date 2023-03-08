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
    }
    
    [Serializable]
    public class GameLevel
    {
        public string name;
        public int timeLimit;
        public int targetScore;
        public ItemConfig[] items;
    }
    
    public GameLevel[] gameLevels;
}
