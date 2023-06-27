using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WebcamPositioning : MonoBehaviour
{
    private int _positionIndex;
    private RectTransform  _rectTransform;
    private RawImage  _rawImage;
    void Start()
    {
        _positionIndex = 0;
        _rectTransform = transform.GetChild(0).transform.GetComponent<RectTransform>();
        _rawImage = transform.GetChild(0).transform.GetComponent<RawImage>();
        SetLocalCornerByIndex(_positionIndex);
    }

    private void SetLocalCornerByIndex(int i)
    {
        var vertical = (RectTransform.Edge)(_positionIndex % 2 +2);
        var horizontal = (RectTransform.Edge)(_positionIndex/2);
        _rectTransform.SetInsetAndSizeFromParentEdge(horizontal, 0, _rectTransform.rect.width);
        _rectTransform.SetInsetAndSizeFromParentEdge(vertical, 0, _rectTransform.rect.height);
    }
    
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            _positionIndex = ++_positionIndex % 4;
            SetLocalCornerByIndex(_positionIndex);
        }
        
        if (Input.GetKeyDown("h"))
        {
            _rawImage.enabled = (!_rawImage.enabled);
        }
    }
}
