using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpeechSource : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isKeywordDetected;
    public bool isNotFocused;
    //private SimpleSpectrum spectrumScript;

    // remove if remove FrequencyContrastEnhancement
    public int sampleSize = 1024; 
    public float minFrequency = 50f;
    public float maxFrequency = 1000f;
    public string mixerValueName;
    public Caption captionField;
    // Remove ends

    // Start is called before the first frame update
    void Start()
    {
        isKeywordDetected = false;
        isNotFocused = false;
        mixerValueName = gameObject.name + "_pitch";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public float GetMainFrequency() {
        float frequencyLimitLow = spectrumScript.frequencyLimitLow;
        float frequencyLimitHigh = spectrumScript.frequencyLimitHigh;
        float[] spectrum = spectrumScript.spectrumInputData;
        float maxValue = spectrum.Max();
        int maxIndex = spectrum.ToList().IndexOf(maxValue);
        float maxFreq = frequencyLimitLow + maxIndex * (frequencyLimitHigh - frequencyLimitLow) / (spectrum.Length - 1);
        return maxFreq;
    }*/

    // REMOVE if remove FrequencyContrastEnhancement
    public float GetFundamentalFrequency() {
        // Get Fundamental Frequency Using Autocorrelation
        mixerValueName = gameObject.name + "_pitch";
        AudioClip audioClip = audioSource.clip;
        float[] samples = new float[sampleSize];
        audioClip.GetData(samples, 0);

        float[] autoCorrelation = new float[sampleSize];

        for (int lag = 0; lag < sampleSize; lag++)
        {
            for (int i = 0; i < sampleSize - lag; i++)
            {
                autoCorrelation[lag] += samples[i] * samples[i + lag] / (sampleSize - lag);
            }
        }

        int fundamentalFrequencyIndex = 0;
        float maxAutoCorrelation = 0f;

        /*for (int i = (int)(minFrequency * sampleSize / audioClip.frequency);
             i < (int)(maxFrequency * sampleSize / audioClip.frequency);
             i++)
        {
            if (autoCorrelation[i] > maxAutoCorrelation)
            {
                maxAutoCorrelation = autoCorrelation[i];
                fundamentalFrequencyIndex = i;
            }
        }

        float fundamentalFrequency = audioClip.frequency * fundamentalFrequencyIndex / sampleSize;*/

        for (int i = (int)(audioClip.frequency / maxFrequency);
             i < (int)(audioClip.frequency / minFrequency);
             i++)
        {
            if (autoCorrelation[i] > maxAutoCorrelation)
            {
                maxAutoCorrelation = autoCorrelation[i];
                fundamentalFrequencyIndex = i;
            }
        }

        float fundamentalFrequency = audioClip.frequency / fundamentalFrequencyIndex;
        return fundamentalFrequency;
    }
    // REMOVE END

    //REMOVE if remove caption functionality
    public void ShowCaptionForDuration(string caption, float duration) {
        captionField.ShowCaptionForDuration(caption, duration);
    }
    // REMOVE END
}
