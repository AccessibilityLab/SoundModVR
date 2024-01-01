using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartNotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool smartNotificationOn;
    public AudioClip notificationClip;
    void Start()
    {
        smartNotificationOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlaySmartNotificationAndAudio(AudioSource audioSource) {
        if (smartNotificationOn) { 
            AudioClip prevClip = audioSource.clip;
            audioSource.clip = notificationClip;
            if (notificationClip == null)
            {
                Debug.LogError("Notification Clip is null");
                yield return new WaitForSeconds(0);
            }
            else
            {
                audioSource.Play();
                yield return new WaitForSeconds(notificationClip.length);
            }
            audioSource.clip = prevClip;
        }
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }

    public void ToggleSmartNotification(bool val) { 
        smartNotificationOn = val;
    }
}
