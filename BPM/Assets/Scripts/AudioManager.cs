using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource source;
    public float bpm;

    private float beatsPerSecond;
    private float beatTimer;

    public bool downbeat;

	// Use this for initialization
	void Start () {
        source.Play();
        beatsPerSecond = 60.0f / bpm;
        beatTimer = 0.0f;
        downbeat = false;
	}
	
	// Update is called once per frame
	void Update () {
        TimeBeat();
	}

    /// <summary>
    /// Uses the bpm to tell the 
    /// </summary>
    void TimeBeat()
    {
        beatTimer += Time.deltaTime;
        if (beatTimer >= beatsPerSecond)
        {
            downbeat = true;
            beatTimer = 0.0f;
        }
        else
        {
            downbeat = false;
        }
    }
}
