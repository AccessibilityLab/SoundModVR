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
        if (notificationClip == null) {
            notificationClip = Resources.Load<AudioClip>("Notifications/notif1_inTheEnd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySmartNotification(AudioSource audioSource) {
        if (smartNotificationOn) { 
            AudioClip prevClip = audioSource.clip;
            audioSource.clip = notificationClip;
            audioSource.Play();
            yield return new WaitForSeconds(notificationClip.length);
            audioSource.clip = prevClip;
        }
    }

    public void ToggleSmartNotification(bool val) { 
        smartNotificationOn = val;
    }
}
