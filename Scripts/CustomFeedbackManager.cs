using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFeedbackManager : MonoBehaviour
{
    public int correctFeedbackIndex;
    public int incorrectFeedbackIndex;
    public AudioClip[] correctFeedbackClipsList;
    public AudioClip[] incorrectFeedbackClipsList;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (correctFeedbackIndex == null) {
            correctFeedbackIndex = 0;
        }
        if (correctFeedbackIndex == null)
        {
            correctFeedbackIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCorrectFeedback(int index) {
        if (index < correctFeedbackClipsList.Length)
        {
            correctFeedbackIndex = index;
            PlayCorrectFeedbackSound();
        }
        else {
            Debug.LogError("Please make sure the correct feedback clip list is populated correctly.");
        }
    }

    public void SelectIncorrectFeedback(int index)
    {
        if (index < incorrectFeedbackClipsList.Length)
        {
            incorrectFeedbackIndex = index;
            PlayIncorrectFeedbackSound();
        }
        else
        {
            Debug.LogError("Please make sure the incorrect feedback clip list is populated correctly.");
        }
    }

    public void PlayCorrectFeedbackSound() {
        audioSource.clip = correctFeedbackClipsList[correctFeedbackIndex];
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void PlayIncorrectFeedbackSound()
    {
        audioSource.clip = incorrectFeedbackClipsList[incorrectFeedbackIndex];
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
