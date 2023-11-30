using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public SpeechSource[] speechSourceList;
    // Start is called before the first frame update
    void Start()
    {
        if (speechSourceList.Length == 0) {
            speechSourceList = GetComponentsInChildren<SpeechSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LowerVolumeForGroup() {
        foreach (SpeechSource speechSource in speechSourceList) {
            if (!speechSource.isKeywordDetected) {
                speechSource.audioSource.volume = 0.07f;
            }
            speechSource.isNotFocused = true;
        }
    }

    public void ResetVolumeForGroup() {
        foreach (SpeechSource speechSource in speechSourceList)
        {
            if (!speechSource.isKeywordDetected)
            {
                speechSource.audioSource.volume = 1;
            }
            speechSource.isNotFocused = false;
        }
    }
}
