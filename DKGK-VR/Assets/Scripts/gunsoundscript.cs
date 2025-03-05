using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class gunsoundscript : MonoBehaviour
{
    public bool shootnow = false;
    public bool reelnow = false;
    public bool loadnow = false;
    public bool shootnow2 = false;
    public AudioClip shoot;
    public AudioClip reel;
    public AudioClip load;
    public AudioClip reel2;
    private AudioSource sounder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sounder = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootnow)
        {
            playshoot();
        }
        else if(reelnow){
            playreel();
        }
        else if (!shootnow && !reelnow && !loadnow)
        {
            sounder.Stop();
        }
        if (loadnow)
        {
            sounder.PlayOneShot(load);
            loadnow = false;
        }
        if (shootnow2)
        {
            sounder.PlayOneShot(shoot);
            shootnow2 = false ;
        }
    }
    void playshoot()
    {
        sounder.clip = reel;
        if (!sounder.isPlaying)
        {
            sounder.Play();
        }
        sounder.loop = true;
    }
    void playreel()
    {
        sounder.clip = reel2;
        if (!sounder.isPlaying)
        {
            sounder.Play();
        }
        sounder.loop = true;
    }
}
