using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour
{

    public AudioClip[] clips;

    int cur = 0;
    private AudioSource src;


void Start(){
    
    src = GetComponent<AudioSource>();
    StartCoroutine(SongLooper());
}
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SongLooper()
    {
        
        src.clip = clips[cur];
        src.Play();
        yield return new WaitForSeconds(src.clip.length);
        cur=(cur+1)%clips.Length;
        StartCoroutine(SongLooper());
    }
}
