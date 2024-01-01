using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayScript : MonoBehaviour
{
    public KeywordDetectionManager keywordDetectionManager;
    public string script;
    public SpeechSource speechSource;

    public SmartNotificationManager smartNotificationManager;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DetectKeywordAndPlay()
    {
        StartCoroutine(keywordDetectionManager.detectKeywordAndPlay(script, speechSource));
    }

    public void PlaySmartNotificationAndAudio()
    {
        StartCoroutine(smartNotificationManager.PlaySmartNotificationAndAudio(audioSource));
    }
}
