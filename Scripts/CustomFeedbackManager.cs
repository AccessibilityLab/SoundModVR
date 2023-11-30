using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFeedbackManager : MonoBehaviour
{
    public string correctFeedbackName;
    public string incorrectFeedbackName;
    public string[] correctFeedbackNameList;
    public string[] incorrectFeedbackNameList;
    public string path;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (correctFeedbackName == null || correctFeedbackName.Length == 0) {
            correctFeedbackName = correctFeedbackNameList[0];
        }
        if (incorrectFeedbackName == null || incorrectFeedbackName.Length == 0)
        {
            incorrectFeedbackName = incorrectFeedbackNameList[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCorrectFeedback(int index) {
        if (index < correctFeedbackNameList.Length)
        {
            correctFeedbackName = correctFeedbackNameList[index];
            PlayCorrectFeedbackSound();
        }
        else {
            Debug.LogError("Please make sure the correct notif name list is populated correctly.");
        }
    }

    public void SelectIncorrectFeedback(int index)
    {
        if (index < incorrectFeedbackNameList.Length)
        {
            incorrectFeedbackName = incorrectFeedbackNameList[index];
            PlayIncorrectFeedbackSound();
        }
        else
        {
            Debug.LogError("Please make sure the incorrect notif name list is populated correctly.");
        }
    }

    public void PlayCorrectFeedbackSound() { 
        string fullPath = path + "/" + correctFeedbackName;
        audioSource.clip = Resources.Load<AudioClip>(fullPath);
        audioSource.Play();
    }

    public void PlayIncorrectFeedbackSound()
    {
        string fullPath = path + "/" + incorrectFeedbackName;
        audioSource.clip = Resources.Load<AudioClip>(fullPath);
        audioSource.Play();
    }
}
