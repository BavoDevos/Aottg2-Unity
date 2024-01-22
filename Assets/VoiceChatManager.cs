using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Settings;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    public static VoiceChatManager Instance;
    private List<AudioSource> _audio = new List<AudioSource>();
    // Start is called before the first frame update

    private void Start()
    {
        if (!Instance) 
        {
            Instance = this;
        }
    }
    public void ApplySoundSettings()
    {
        int i = 0;
        var microphones = GameObject.FindGameObjectsWithTag("Speaker");
        foreach (GameObject MicObj in microphones) 
        {
            AudioSource Mic = MicObj.GetComponent<AudioSource>();
            Apply(Mic);
        }
    }


    public override void OnPlayerEnteredRoom(Player player) 
    {
        Debug.Log("test");
        ApplySoundSettings();

    }
    public void Apply(AudioSource Mic) 
    {
        Mic.volume = GetVoiceChatVolume();
        Mic.spatialBlend = GetTypeOfAudio();
    }

    private float GetVoiceChatVolume()
    {
        if (SettingsManager.SoundSettings.VoiceChat.Value)
        {
            return SettingsManager.SoundSettings.VoiceChatVolume.Value * 2.5f;
        }
        else
        {
            return 0f;
        }
    }
    private float GetTypeOfAudio()
    {
        if (SettingsManager.SoundSettings.SpatialVoiceChat.Value)
        {
            return 1;
        }
        else 
        {
            return 0;
        }
    }
}