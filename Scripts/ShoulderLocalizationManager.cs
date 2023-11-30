using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShoulderLocalizationManager : MonoBehaviour
{
    public Camera mainCamera;
    public AudioSource targetAudioSource;
    public AudioClip leftAudioClip;
    public AudioClip rightAudioClip;
    //public Caption captionField;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (leftAudioClip == null) {
            leftAudioClip = Resources.Load<AudioClip>("Assistant/Left");
        }
        if (rightAudioClip == null) { 
            rightAudioClip = Resources.Load<AudioClip>("Assistant/Right");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayLocationAlert(Vector3 soundSourcePosition) {
        // 1. decide if the important sound source is from left or right
        // 2. play "To your left" or "To your right" depending on the location decided.

        Vector3 cameraDirection = mainCamera.transform.forward;
        Vector3 targetDirection = soundSourcePosition - mainCamera.transform.position;
        float angle = Vector3.SignedAngle(targetDirection, cameraDirection, Vector3.up);
        if (angle > 0)
        {
            audioSource.clip = leftAudioClip;
            /*if (captionField != null) {
                captionField.ShowCaptionForDuration("To your left.", audioSource.clip.length + 1);
            }*/
            audioSource.Play();
        }
        else
        {
            audioSource.clip = rightAudioClip;
            /*if (captionField != null) {
                captionField.ShowCaptionForDuration("To your right.", audioSource.clip.length + 1);
            }*/
            audioSource.Play();
        }   
    }

    public void PlayAlertWithDefinedTarget() {
        if (targetAudioSource != null) {
            PlayLocationAlert(targetAudioSource.transform.position);
        }
    }
}
