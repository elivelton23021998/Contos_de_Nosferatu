using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConfig : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Musica;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    float MasterVolume = 1f;
    float MusicaVolume = 0.5f;
    float SFXVolume = 0.5f;

    void Awake()
    {
        Musica = FMODUnity.RuntimeManager.GetBus("bus:/Master/Musicas");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SFXVolumeTest");
    }

    void Update()
    {
        Musica.setVolume(MusicaVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicaVolumeLevel(float newMusicVolume)
    {
        MusicaVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }
}