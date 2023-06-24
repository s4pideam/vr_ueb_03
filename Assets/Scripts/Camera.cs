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
    
    // Start is called before the first frame update
    void Start()
    {
        _cameraMode = CameraMode.Third;
        _oldPosition = target.position;
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
        switch (_cameraMode)
        {
            case CameraMode.First:
                transform.position = target.position;
                transform.rotation = target.rotation;
                break;

            case CameraMode.Third:
                _oldPosition.y = target.position.y;
                Vector3 direction = (target.position - _oldPosition).normalized;
                transform.position = Vector3.Lerp(transform.position,target.transform.position - (direction * 5f),Time.deltaTime*2f);
                if (direction == Vector3.zero) break;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*2f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        {
            
        }
    }
}
