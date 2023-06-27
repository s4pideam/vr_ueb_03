using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveWorld : MonoBehaviour
{
    private GlobalSettings _globalSettings;
 
    void Start()
    {
        _globalSettings = FindObjectOfType<GlobalSettings>();

    }
    void Update()
    {
 
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.transform.Translate(0,0,-_globalSettings.velocity*Time.deltaTime);
            if (child.transform.position.z <= 0)
            {
                child.transform.Translate(0, 0, transform.childCount * 50);
            }
        }
        
    }
}
