using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class KeywordDetectionManager : MonoBehaviour
{
    public List<string> keywords;
    public AudioClip notificationClip;
    private bool isAddingKeyword;
    //public ShoulderLocalizationManager shoulderLocalizationManager;
    // Start is called before the first frame update
    void Start()
    {
        isAddingKeyword = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator detectKeywordAndPlay(string script, SpeechSource speechSource, AudioClip audioClip) {
        AudioSource audioSource = speechSource.audioSource;
        if (detectKeywords(script))
        {
            speechSource.isKeywordDetected = true;
            audioSource.volume = 1;
            audioSource.clip = notificationClip;
            /*if (shoulderLocalizationManager) {
                shoulderLocalizationManager.PlayLocationAlert(speechSource.transform.position);
                yield return new WaitForSeconds(1);
            }*/
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        speechSource.isKeywordDetected = false;
        if (speechSource.isNotFocused) { audioSource.volume = 0.1f; }
    }

    bool detectKeywords(string script)
    {
        string[] wordList = GetWords(script);
        // If the corresponding word list contain any keyword, play notif sound and raise audiosource volume to 1 until finish playing.
        bool containsKeyword = false;
        foreach (string keyword in keywords)
        {
            if (Array.Exists(wordList, element => element == keyword))
            {
                containsKeyword = true;
            }
        }
        return containsKeyword;
    }


    static string[] GetWords(string input)
    {
        MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

        var words = from m in matches.Cast<Match>()
                    where !string.IsNullOrEmpty(m.Value)
                    select TrimSuffix(m.Value);

        return words.ToArray();
    }

    static string TrimSuffix(string word)
    {
        int apostropheLocation = word.IndexOf('\'');
        if (apostropheLocation != -1)
        {
            word = word.Substring(0, apostropheLocation);
        }

        return word;
    }

    public void SetAddOrSubtract(bool toggleVlaue) { 
        isAddingKeyword = toggleVlaue;
    }

    public void ChangeKeywordList(string keyword)
    {
        if (isAddingKeyword)
        {
            keywords.Add(keyword);
        }
        else { 
            keywords.Remove(keyword);
        }
    }

    public void AddOrSubtractKeyword(bool isAdding, string keyword) {
        SetAddOrSubtract(isAdding);
        ChangeKeywordList(keyword);
    }
}
