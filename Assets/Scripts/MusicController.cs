using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private static MusicController instance = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Set-up persistance
	void Awake () {
        DontDestroyOnLoad(this);
	}

    public static MusicController GetInstance(AudioClip musicClip)
    {
        return instance;
    }

    public static void InstaniateIfNoInstance(AudioClip musicClip)
    {
        if (instance == null)
        {
            GameObject go = Instantiate(new GameObject());
            go.transform.position = new Vector3(0f, 0f, 0f);
            go.name = "Music Controller";
            MusicController mc = go.AddComponent<MusicController>();
            AudioSource a = go.AddComponent<AudioSource>();
            a.clip = musicClip;
            a.loop = true;
            //a.playOnAwake = true;
            a.Play();
            instance = mc;
        }
    }
}
