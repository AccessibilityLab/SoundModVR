using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BeatEnhancementManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixerGroup beatMixerGroup;
    private AudioMixerGroup origMixerGroup;
    public float bpm;
    private IEnumerator[] songCoroutinesList;
    private int beatEnhancementPatternSelection;

    // Start is called before the first frame update
    void Start()
    {
        beatEnhancementPatternSelection = 0;
        songCoroutinesList = new IEnumerator[] { StartBeatEnhancement1Coroutine(), StartBeatEnhancement2Coroutine() };
        StartBeatEnhancement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBeatEnhancement() {
        foreach (IEnumerator coroutine in songCoroutinesList) {
            StopCoroutine(coroutine);
        }
        if (bpm > 0) {
            foreach (IEnumerator coroutine in songCoroutinesList)
            {
                StartCoroutine(coroutine);
            }
        }
    }

    public void ChooseBeatEnhancementPattern(int index) {
        if (index != 0)
        {
            if (beatEnhancementPatternSelection == 0) {
                origMixerGroup = audioSource.outputAudioMixerGroup;
                audioSource.outputAudioMixerGroup = beatMixerGroup;
            }
        }
        else { 
            if (origMixerGroup != null && beatEnhancementPatternSelection != 0)
            {
                audioSource.outputAudioMixerGroup = origMixerGroup;
            }
        }
        beatEnhancementPatternSelection = index;
    }

    public IEnumerator StartBeatEnhancement1Coroutine() {
        // before this is called, check if the bpm is not <0 or null;
        float calibration = 0.9f;
        yield return new WaitForSeconds(calibration);
        while (true) {
            float interval = 120 / bpm;
            if (beatEnhancementPatternSelection == 1) { beatMixerGroup.audioMixer.SetFloat("Beat_volume", 10); }
            yield return new WaitForSeconds(interval);
            if (beatEnhancementPatternSelection == 1) { beatMixerGroup.audioMixer.SetFloat("Beat_volume", -10); }
            yield return new WaitForSeconds(interval);
        }
    }

    public IEnumerator StartBeatEnhancement2Coroutine()
    {
        // before this is called, check if the bpm is not <0 or null;
        float calibration = 0.9f;
        yield return new WaitForSeconds(calibration);
        while (true)
        {
            float interval = 60 / bpm;
            if (beatEnhancementPatternSelection == 2) { beatMixerGroup.audioMixer.SetFloat("Beat_volume", 10); }
            yield return new WaitForSeconds(interval);
            if (beatEnhancementPatternSelection == 2) { beatMixerGroup.audioMixer.SetFloat("Beat_volume", -10); }
            yield return new WaitForSeconds(interval);
        }
    }
}
