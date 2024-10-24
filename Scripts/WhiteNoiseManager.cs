using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoiseManager : MonoBehaviour
{
    public List<AudioClip> noiseClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNoiseSelection(int val) {
        if (val == 0)
        {
            audioSource.Stop();
        }
        else { 
            audioSource.clip = noiseClips[val - 1];
            audioSource.Play();
        }
    }
}
