using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDistanceAssistanceManager : MonoBehaviour
{
    public AudioSource targetAudioSource;
    public GameObject startingPoint;
    private float maxDist;
    public Camera mainCamera;
    private bool isTurnedOn;
    // Start is called before the first frame update
    void Start()
    {
        if (startingPoint != null)
        {
            maxDist = Vector3.Distance(targetAudioSource.transform.position, startingPoint.transform.position);
        }
        else {
            maxDist = 500f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurnedOn) { 
            //calculate distance from target audioSource to current transform. Lerp over 0->max to 0.5->1.5, and apply to audioSOurce's pitch.
            float distance = Vector3.Distance(targetAudioSource.transform.position, mainCamera.transform.position);
            float pitchNum = 0.5f + (maxDist - distance) / maxDist;
            targetAudioSource.pitch = pitchNum;
        }
    }

    public void ToggleOnOff(bool toggle) { 
        isTurnedOn = toggle;
    }
}
