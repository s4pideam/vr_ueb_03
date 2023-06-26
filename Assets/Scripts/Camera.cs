using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public Transform target;

    private enum CameraMode {Third, First};

    private CameraMode _cameraMode;
    private Vector3 _oldPosition;
    private float space = 10.0f;
    private float camera_velocity = 5.0f; 
    
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
                    transform.position = Vector3.Lerp(transform.position,direction,Time.deltaTime*camera_velocity); 
                }
                else
                {
                    direction = target.position;
                    direction.y += 0.5f;
                    transform.position = direction;
                }


                break;

            case CameraMode.Third:
                direction = target.position - Vector3.forward*space;
                direction.y = MathF.Max(1f, direction.y);
                transform.position = Vector3.Lerp(transform.position,direction,Time.deltaTime*camera_velocity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        {
            
        }
    }
}
