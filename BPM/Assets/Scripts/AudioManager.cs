using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource source;
	public AudioClip[] songs;
    //public float bpm;

    public GameObject leftMetronome;
    public GameObject rightMetronome;

    private float beatsPerSecond;
    private float beatTimer;

    public bool downbeat;

	// Use this for initialization
	void Start () {
        beatTimer = 0.0f;
		downbeat = false;

		if (PlayerPrefs.GetString ("Difficulty").Equals("Easy")) 
		{
			source.clip = songs [0];
			beatsPerSecond = 60.0f / 80.0f;
		} 
		else if (PlayerPrefs.GetString ("Difficulty").Equals("Normal")) 
		{
			source.clip = songs [1];
			beatsPerSecond = 60.0f / 100.0f;
		}

		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
        TimeBeat();
        Metronome();
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

    void Metronome()
    {
        if (downbeat)
        {
            leftMetronome.transform.position = new Vector3(-5.0f, -4.48f, -0.5f);
            rightMetronome.transform.position = new Vector3(5.0f, -4.48f, -0.5f);
        }
        else
        {
            float leftPos = -5.0f + 5.0f * (beatTimer / beatsPerSecond);
            float rightPos = 5.0f - 5.0f * (beatTimer / beatsPerSecond);
            leftMetronome.transform.position = new Vector3(leftPos, -4.48f, -0.5f);
            rightMetronome.transform.position = new Vector3(rightPos, -4.48f, -0.5f);
        }
    }
}
