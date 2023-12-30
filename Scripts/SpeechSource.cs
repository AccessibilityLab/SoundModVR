using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpeechSource : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isKeywordDetected;
    public bool isNotFocused;

    // Start is called before the first frame update
    void Start()
    {
        isKeywordDetected = false;
        isNotFocused = false;
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
