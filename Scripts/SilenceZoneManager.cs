using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceZoneManager : MonoBehaviour
{
    public List<AudioSource> audioSourceList;
    private List<float> originalRange;
    // Start is called before the first frame update
    void Start()
    {
        originalRange = new List<float>();
        foreach (AudioSource audioSource in audioSourceList) {
            originalRange.Add(audioSource.maxDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SilenceZoneToggle(bool toggle) {
        if (toggle)
        {
            AddSilenceZone();
        }
        else { 
            RemoveSilenceZone();
        }
    }

    public void AddSilenceZone() {
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            for (int j = i + 1; j < audioSourceList.Count; j++)
            {
                AudioSource source1 = audioSourceList[i];
                AudioSource source2 = audioSourceList[j];
                float range1 = source1.maxDistance;
                float range2 = source2.maxDistance;
                float dist = Vector3.Distance(source1.transform.position, source2.transform.position);
                if (dist < range1 + range2)
                {
                    source1.maxDistance = range1 - ((range1 + range2 - dist) / 2) - 1;
                    source2.maxDistance = range2 - ((range1 + range2 - dist) / 2) - 1;
                }
            }
        }
    }

    public void RemoveSilenceZone() {
        for (int i = 0; i < audioSourceList.Count; i++) { 
            AudioSource audioSource = audioSourceList[i];
            audioSource.maxDistance = originalRange[i];
        }
    }
}
