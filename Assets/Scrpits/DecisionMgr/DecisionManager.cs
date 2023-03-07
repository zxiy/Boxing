using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//
//  a class for processsing DataDriven logic 
//
[System.Serializable]
public class MixinEvent : UnityEvent<DecisionManager> { };
public class DecisionManager : MonoBehaviour
{
    [SerializeField] private List<ConditionalStatement> data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DecisionManagerUpdate()
    {
        foreach(ConditionalStatement cs in data)
        {
            if (cs._conditions.Aggregate(true, (x, y) => x && y.Eval()))
            {
                cs._response.Dispatch();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DecisionManagerUpdate();
    }
}
