using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource source;
    public Text currently;

    // Start is called before the first frame update
    void Start()
    {
        source.loop = false;

    }

    // Update is called once per frame
    void Update()
    {
        bool musicOn = PlayerPrefs.GetInt("music") == 1;

        if (!source.isPlaying && musicOn)
        {
            source.clip = GetRandomClip();
            source.Play();

            currently.text = "Song: " + source.clip.name;
        }
        else if (!source.isPlaying)
        {
            currently.text = "";
        }
    }

    AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
