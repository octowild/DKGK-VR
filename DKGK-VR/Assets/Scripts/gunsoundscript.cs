using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class gunsoundscript : MonoBehaviour
{
    public bool shootnow = false;
    public bool reelnow = false;
    public bool loadnow = false;
    public AudioClip shoot;
    public AudioClip reel;
    public AudioClip load;
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
        if (loadnow)
        {
            sounder.PlayOneShot(load);
            loadnow = false;
        }
    }
    IEnumerator playshoot()
    {
        sounder.clip = shoot;
        sounder.Play();
        sounder.loop = false;
        yield return new WaitForSeconds(shoot.length);
        sounder.clip = reel;
        sounder.Play();
        sounder.loop = true;
    }
    void playreel()
    {
        sounder.clip = reel;
        sounder.Play();
        sounder.loop = true;
    }
}
