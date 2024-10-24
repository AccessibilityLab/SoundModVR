using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearingRangeManager : MonoBehaviour
{
    public AudioSource[] audioSourceList;
    // Start is called before the first frame update
    void Start()
    {
        if (audioSourceList.Length == 0){
            audioSourceList = GetComponentsInChildren<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHearingRange(float v) {
        foreach (AudioSource source in audioSourceList) {
            source.maxDistance = v;
        }
    }
}
