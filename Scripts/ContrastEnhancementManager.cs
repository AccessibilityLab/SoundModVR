using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class ContrastEnhancementManager : MonoBehaviour
{
    public float frequencyThreshold = 50;
    public float distThreshold = 1;
    private SpeechSource[] allSpeechSourceInScene;
    private Dictionary<SpeechSource, float> fundFreqDict = new Dictionary<SpeechSource, float>();
    private List<KeyValuePair<SpeechSource, float>> sortedSpeechSourceByFreq;
    public AudioMixer _MasterMixer;
    // Start is called before the first frame update
    void Start()
    {
        allSpeechSourceInScene = FindObjectsOfType<SpeechSource>();
        foreach (SpeechSource speechSource in allSpeechSourceInScene)
        {
            fundFreqDict[speechSource] = speechSource.GetFundamentalFrequency();
        }
        sortedSpeechSourceByFreq = fundFreqDict.OrderBy(pair => pair.Value).Take(allSpeechSourceInScene.Length).ToList();
        for (int i = 0; i < sortedSpeechSourceByFreq.Count(); i++)
        {
            /// DEBUG PRINT THE FREQ
            Debug.Log(sortedSpeechSourceByFreq[i].Key.mixerValueName + sortedSpeechSourceByFreq[i].Value.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContrastToggle(bool isIncreaseFrequencyContrast) {
        if (isIncreaseFrequencyContrast)
        {
            IncreaseFrequencyContrast();
        }
        else {
            ResetToOriginalFrequency();
        }
    }

    public void IncreaseFrequencyContrast() {
        
        for (int i = 1; i < sortedSpeechSourceByFreq.Count(); i++)
        {
            SpeechSource lowerSource = sortedSpeechSourceByFreq[i - 1].Key;
            SpeechSource higherSource = sortedSpeechSourceByFreq[i].Key;
            if (sortedSpeechSourceByFreq[i].Value - sortedSpeechSourceByFreq[i - 1].Value < frequencyThreshold &&
                Vector3.Distance(lowerSource.transform.position, higherSource.transform.position) < distThreshold)
            {
                float temp;
                _MasterMixer.GetFloat(lowerSource.mixerValueName, out temp);
                _MasterMixer.SetFloat(lowerSource.mixerValueName, temp - 0.1f);

                _MasterMixer.GetFloat(higherSource.mixerValueName, out temp);
                _MasterMixer.SetFloat(higherSource.mixerValueName, temp + 0.1f);
            }
        }
    }

    public void ResetToOriginalFrequency() {
        foreach (SpeechSource speechSource in allSpeechSourceInScene)
        {
            _MasterMixer.SetFloat(speechSource.mixerValueName, 1f);
        }
    }
}
