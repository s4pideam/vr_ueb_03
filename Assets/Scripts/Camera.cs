using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public Transform target;
    [Range(5f, 30f)] public float cameraSpace = 10.0f;
    [Range(1f, 30f)] public float cameraVelocity = 5.0f; 
    
    private enum CameraMode {Third, First};
    private CameraMode _cameraMode;

    
    // Start is called before the first frame update
    void Start()
    {
        _cameraMode = CameraMode.Third;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            int modes = Enum.GetValues(typeof(CameraMode)).Length;
            _cameraMode = (CameraMode)(((int)_cameraMode+1) % modes);
        }
    }

    private void LateUpdate()
    {
        Vector3 direction;
        switch (_cameraMode)
        {
            case CameraMode.First:
                if (Vector3.Distance(transform.position, target.position) > 1f)
                {
                    direction = target.position;
                    direction.y += 0.5f;
                    transform.position = Vector3.Lerp(transform.position,direction,Time.deltaTime*cameraVelocity); 
                }
                else
                {
                    direction = target.position;
                    direction.y += 0.5f;
                    transform.position = direction;
                }


                break;

            case CameraMode.Third:
                direction = target.position - Vector3.forward*cameraSpace;
                direction.y = MathF.Max(1f, direction.y);
                transform.position = Vector3.Lerp(transform.position,direction,Time.deltaTime*cameraVelocity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        {
            
        }
    }
}
