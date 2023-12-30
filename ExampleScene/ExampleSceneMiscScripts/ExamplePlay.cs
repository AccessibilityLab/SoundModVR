using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlay : MonoBehaviour
{
    public KeywordDetectionManager keywordDetectionManager;
    public string script;
    public SpeechSource speechSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectKeywordAndPlay() {
        StartCoroutine(keywordDetectionManager.detectKeywordAndPlay(script, speechSource));
    }
}
