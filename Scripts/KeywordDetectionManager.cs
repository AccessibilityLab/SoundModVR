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
    //public ShoulderLocalizationManager shoulderLocalizationManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator detectKeywordAndPlay(string script, SpeechSource speechSource) {
        AudioSource audioSource = speechSource.audioSource;
        AudioClip audioClip = audioSource.clip;
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

    public void AddKeyword(string keyword) {
        if (!keywords.Contains(keyword))
        {
            keywords.Add(keyword);
        }
        else {
            Debug.LogError("this keyword is already added");
        }
    }

    public void SubtractKeyword(string keyword) {
        if (keywords.Contains(keyword))
        {
            keywords.Remove(keyword);
        }
        else
        {
            Debug.LogError("this keyword is has not been added yet");
        }
    }
}
