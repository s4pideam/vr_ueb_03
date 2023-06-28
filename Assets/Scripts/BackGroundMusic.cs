using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance _eventAudio;
 
    [Range(0.0f, 1.0f)] public float volume = 0.3f;

    [Range(0.0f, 1.0f)] public float musicstate;
    private float _yearnMusicState = 0;
    FMOD.Studio.EventInstance playerState;

    public bool debug = false;
    // Start is called before the first frame update
    void Start()
    {
        _eventAudio = FMODUnity.RuntimeManager.CreateInstance("event:/bg");
        _eventAudio.start();
        _eventAudio.setVolume(0.1f);
    }

    void OnDestroy()
    {
        _eventAudio.release();
    }


    // Update is called once per frame
    void Update()
    {
        _eventAudio.getVolume(out float _currentVolume);
        if (_currentVolume != volume)
        {
            _eventAudio.setVolume(volume);
        }

        if (Math.Abs(_yearnMusicState - musicstate) > 0.01f || debug )
        {
            if(!debug) musicstate = Mathf.Lerp(musicstate, _yearnMusicState, Time.deltaTime);
            _eventAudio.setParameterByName("musicstate", musicstate);
        }


    }

    public void setMusicStateTo(float i)
    {
        _yearnMusicState = i;
    }
}
