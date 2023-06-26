using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour
{
    private float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            pos.z = pos.z - speed*Time.deltaTime;
            transform.GetChild(i).position = pos;
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            if (pos.z <= 0)
            {
                pos.z = transform.childCount * 50;
                transform.GetChild(i).position = pos;    
            }

            
        }
        
    }
}
