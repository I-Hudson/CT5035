//==============================================================================================================================
// Project: The Life of Others
// File: CS_SoundManager.cs
// Author: Daniel McCluskey
// Date Created: 18/04/18
// Brief: This is the script that contains ease of access functions for loading and playing sounds..
// Last Edited by: Daniel McCluskey
//==============================================================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SoundManager : MonoBehaviour {

    [Header("Settings")]
    [Tooltip("The Current radio track that is playing")]
    [SerializeField] private int iCurrentRadioTrack;//The current Radio track being played

    [Tooltip("The Volume to set the tracks to")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float fVolume;//Volume to set the tracks to whilst playing.

    [Tooltip("The GameObject where you wish the sounds to play from")]
    [SerializeField] private GameObject SpeakerLocation;//To reference the Speakers Position.

    //[FMODUnity.EventRef]
    //public string[] SoundTracksToPlay;//List of Event names to play.
    //public FMOD.Studio.EventInstance[] EventInstanceList;//FMOD Events

    private int iAmountOfRadios = 4;//If you add more radio tracks, make sure to change this value.
   

    //I tried using an array of EventInstance's but I could never get it to CreateInstance() with it, so I've just used 4 Separate variables.
    //I've left the for loops I used Commented down below if you wish to try yourself.
    private FMOD.Studio.EventInstance RadioTrack1;//FMOD Events
    private FMOD.Studio.EventInstance RadioTrack2;//FMOD Events
    private FMOD.Studio.EventInstance RadioTrack3;//FMOD Events
    private FMOD.Studio.EventInstance RadioTrack4;//FMOD Events

    //List of Event names
    [Header("FMOD Events to play")]
    [SerializeField] [FMODUnity.EventRef] private string RadioEvent1;//FMOD Event Name
    [SerializeField] [FMODUnity.EventRef] private string RadioEvent2;//FMOD Event Name
    [SerializeField] [FMODUnity.EventRef] private string RadioEvent3;//FMOD Event Name
    [SerializeField] [FMODUnity.EventRef] private string RadioEvent4;//FMOD Event Name

    [Header("Activated Radios")]
    [SerializeField] private bool bRadio1Activated;//To mute and unmute certain event tracks
    [SerializeField] private bool bRadio2Activated;//To mute and unmute certain event tracks
    [SerializeField] private bool bRadio3Activated;//To mute and unmute certain event tracks
    [SerializeField] private bool bRadio4Activated;//To mute and unmute certain event tracks

    


    private void Start()
    {
        InitialiseRadioTracks(SpeakerLocation);//Initialise each of the radio tracks
        SwitchRadioTracks(iCurrentRadioTrack);//Switch to the given radio track

        /*
        //This is the quick and easy method I tried but couldn't get it to work.
        for (int i = 0; i < SoundTracksToPlay.Length; i++)
        {
            EventInstanceList[i] = FMODUnity.RuntimeManager.CreateInstance(SoundTracksToPlay[i]);//Create the Sound Instance
            EventInstanceList[i].set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(SpeakerLocation));//Move the sound location to the Radio speakers
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(EventInstanceList[i], SpeakerLocation.transform, SpeakerLocation.GetComponent<Rigidbody>());//Attach the instance to the speakers, so sound follows when moved.
            EventInstanceList[i].start();
            bRadioActivated[i] = false;
        }
        */
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//If the player Left clicks
        {
            NextRadioTrack();//Set iCurrentRadioTrack to the next radio track
            SwitchRadioTracks(iCurrentRadioTrack);//Mute all radio tracks and re-enable the given radio track
        }
        if (Input.GetMouseButtonDown(1))//If the player Right Clicks
        {
            RestartAllTracks();//Start playing all tracks
        }
    }



    // @brief	Function to initialise all of the radio tracks, move them to the given game object and then parent them to the given gameobject.
    // @param	GameObject a_GameObjectToAttachTo = The game object you wish to attach the sounds to (Possibly a pair of headphones or a speaker)
    // @comment	A huge function full of repeated code that could easily be shortened down if I could get an array of EventInstances working :/
    private void InitialiseRadioTracks(GameObject a_GameObjectToAttachTo)
    {
        RadioTrack1 = FMODUnity.RuntimeManager.CreateInstance(RadioEvent1);//Create the Sound Instance
        RadioTrack2 = FMODUnity.RuntimeManager.CreateInstance(RadioEvent2);//Create the Sound Instance
        RadioTrack3 = FMODUnity.RuntimeManager.CreateInstance(RadioEvent3);//Create the Sound Instance
        RadioTrack4 = FMODUnity.RuntimeManager.CreateInstance(RadioEvent4);//Create the Sound Instance

        RadioTrack1.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(a_GameObjectToAttachTo));//Move the sound location to the Radio speakers
        RadioTrack2.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(a_GameObjectToAttachTo));//Move the sound location to the Radio speakers
        RadioTrack3.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(a_GameObjectToAttachTo));//Move the sound location to the Radio speakers
        RadioTrack4.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(a_GameObjectToAttachTo));//Move the sound location to the Radio speakers

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(RadioTrack1, a_GameObjectToAttachTo.transform, a_GameObjectToAttachTo.GetComponent<Rigidbody>());//Attach the instance to the speakers, so sound follows when moved.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(RadioTrack2, a_GameObjectToAttachTo.transform, a_GameObjectToAttachTo.GetComponent<Rigidbody>());//Attach the instance to the speakers, so sound follows when moved.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(RadioTrack3, a_GameObjectToAttachTo.transform, a_GameObjectToAttachTo.GetComponent<Rigidbody>());//Attach the instance to the speakers, so sound follows when moved.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(RadioTrack4, a_GameObjectToAttachTo.transform, a_GameObjectToAttachTo.GetComponent<Rigidbody>());//Attach the instance to the speakers, so sound follows when moved.


    }

    

    // @brief	Function to restart playing all of the tracks from the beginning.
    private void RestartAllTracks()
    {
        RadioTrack1.start();
        RadioTrack2.start();
        RadioTrack3.start();
        RadioTrack4.start();
    }

    // @brief	Function that increments "iCurrentRadioTrack" to toggle between dfifferent radio tracks.
    private void NextRadioTrack()
    {
        iCurrentRadioTrack++;
        if(iCurrentRadioTrack >= iAmountOfRadios)
        {
            iCurrentRadioTrack = 0;
        }
    }
    // @brief	Function to mute all radio tracks and then turn on a specific one.
    // @param	int a_iRadioTrack = the radio track to turn on.
    public void SwitchRadioTracks(int a_iRadioTrack)
    {

        MuteAllTracks();//Mute every track and deactivate them

        switch(a_iRadioTrack)
        {
            case 0:
                {
                    RadioTrack1.setVolume(fVolume);
                    bRadio1Activated = true;
                    break;
                }
            case 1:
                {
                    RadioTrack2.setVolume(fVolume);
                    bRadio2Activated = true;
                    break;
                }
            case 2:
                {
                    RadioTrack3.setVolume(fVolume);
                    bRadio3Activated = true;
                    break;
                }
            case 3:
                {
                    RadioTrack4.setVolume(fVolume);
                    bRadio4Activated = true;
                    break;
                }
        }
    }

    // @brief	Function to Mute all tracks and deactivate them
    public void MuteAllTracks()
    {
        RadioTrack1.setVolume(0.0f);
        RadioTrack2.setVolume(0.0f);
        RadioTrack3.setVolume(0.0f);
        RadioTrack4.setVolume(0.0f);
        bRadio1Activated = false;
        bRadio2Activated = false;
        bRadio3Activated = false;
        bRadio4Activated = false;

    }


    // @brief	Function to play a sound and attach it to a game object so it will move with it.
    // @brief	Looped Sounds will play forever with no way to stop it.
    // @param	Vector3 a_v3Position = XYZ Vector3 to play the sound at.
    // @param	string a_sSoundEventName = Sound to Play
    // @example Play3DSoundAtPos(new Vector3(0.0f,0.0f,0.0f), "PlayerShoot");
    public static void PlaySoundOnObject(GameObject a_goGameObject, string a_sSoundEventName)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(("event:/" + a_sSoundEventName), a_goGameObject);

    }
    // @brief	Function to play a sound and attach it to a transform so it will move with it.
    // @brief	Looped Sounds will play forever with no way to stop it.
    // @param	Vector3 a_v3Position = XYZ Vector3 to play the sound at.
    // @param	string a_sSoundEventName = Sound to Play
    // @example Play3DSoundAtPos(new Vector3(0.0f,0.0f,0.0f), "PlayerShoot");
    public static void PlaySoundOnObject(Transform a_Position, string a_sSoundEventName)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(("event:/" + a_sSoundEventName), a_Position.gameObject);
    }



    // @brief	Function to play a 3D sound event at a given Vector3 Position
    // @brief	Looped Sounds will play forever with no way to stop it.
    // @param	Vector3 a_v3Position = XYZ Vector3 to play the sound at.
    // @param	string a_sSoundEventName = Sound to Play
    // @example Play3DSoundAtPos(new Vector3(0.0f,0.0f,0.0f), "PlayerShoot");
    public static void PlaySoundAtPos(Vector3 a_v3Position, string a_sSoundEventName)
    {
        FMODUnity.RuntimeManager.PlayOneShot(("event:/" + a_sSoundEventName), a_v3Position);        
    }

    // @brief	Function to play a 3D sound event at a given GameObject's Position
    // @brief	Looped Sounds will play forever with no way to stop it.
    // @param	GameObject a_goGameObject = GameObject to play the sound at.
    // @param	string a_sSoundEventName = Sound to Play
    // @example Play3DSoundAtPos(gameObject, "PlayerShoot");
    public static void PlaySoundAtPos(GameObject a_goGameObject, string a_sSoundEventName)
    {
        FMODUnity.RuntimeManager.PlayOneShot(("event:/" + a_sSoundEventName), a_goGameObject.transform.position);
    }



}
