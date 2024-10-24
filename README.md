# Sound-Customization-VR-Accessibility Feature Toolkit
 
## 1. Introduction
**This toolkit is in Beta! We welcome any suggestions! Please provide them through the SoundCusVR Developer Study or email Xinyun Cao at xinyunc@umich.edu directly.**<br/>
This is a Unity Toolkit that contains nine features for Sound Customization for DHH (Deaf and Hard of Hearing) Users. It is designed with VR development in mind, but it might be used for other development as well.

## 2. Key Ideas
**Sound Customization**: Changing aspects of the sounds used in the VR app such as shifting frequencies of the sound or prioritizing certain sounds to satisfy the needs of individual DHH users.<br />
**Unity Audio Mixer**: You can access the AudioMixer window from Window->Audio->AudioMixer. For more information about AudioMixer, please see Unity documentation: [AudioMixer](https://docs.unity3d.com/Manual/AudioMixer.html) and [AudioMixer Scripting](https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.html).

## 3. Features List
This is the list of features. They are divided into 4 categories - ***Prioritization, Sound Parameter Changes, Spatial Assistance, and Add-on Sounds***. Each feature should work independently, and some features could work together.
### 3.1 Prioritization
[**Group Prioritization**](#group-prioritization): When having multiple groups of sounds, focus on a specific group and lower sounds of all other groups.<br />
[**Keyword Prioritization**](#keyword-prioritization): set specific keywords you're interested in, and the program will play a notification and increase speech volume when it's played.<br />
[**Sound Prioritization**](#sound-prioritization): lower environment sounds during speech/important sounds.<br />
[**Direction-Based Prioritization**](#direction-based-prioritization): amplifies the sounds in the direction the user faces while simultaneously reducing the volume of sounds coming from other directions.

### 3.2 Sound Parameter Changes
[**Volume and Pitch Adjustment**](#volume-and-pitch-adjustment): set volume and pitch both for the system and for individual sound sources in the environment.<br />
[**Speech Speed Adjustment**](#speech-speed-adjustment): adjust speed for individual speech sound sources.<br />
[**Frequency Contrast Enhancement**](#frequency-contrast-enhancement): adjusts the frequencies of adjacent sound sources, elevating one while lowering the other to enhance their distinction.<br />
[**Beat Enhancement**](#beat-enhancement): boosts the rhythm of music sounds by dynamically increasing and decreasing the volume along with the beats.


### 3.3 Spatial Assistance
[**Shoulder Localization Helper**](#shoulder-localization-helper): To alert you when an important sound is played/ by your request, and if it’s on your left/right.<br />
[**Live Listen Helper**](#live-listen-helper): single out sounds when you move them close to sound sources.<br />
[**Left-Right Balance**](#left-right-balance): adjust the system sound balance to either the left or right.<br />
[**Hearing Range Adjustment**](#hearing-range-adjustment):  allows users to adjust the range for sound activation.<br />
[**Sound Distance Assistance**](#sound-distance-assistance): aids in perceiving distance by modulating sound pitch based on the user’s proximity to the source: pitch decreases as distance increases and vice versa.<br />
[**Silence Zone**](#silence-zone): increases the contrast between spatial sounds by including a silence zone between them.<br />

### 3.4 Add-on Sounds
[**Smart Notification**](#smart-notification): Playing a notification before an important sound, like speech or feedback.<br />
[**Custom Feedback Sound**](#custom-feedback-sound): Change the feedback sounds in the system to your individual preferences/needs.<br />
[**Calming Noise**](#calming-noise): enables users to select among white noise, pink noise, and rain sounds to add to the VR environment.<br />

## 4. Usage Guide
<div id="group-prioritization"></div>

### 1.1) Group Prioritization 
*(* :thumbsup: *Recommended to use in situations where multiple groups of conversation/sounds are happening concurrently.)* <br />
 :eyes:  :eyes:  :eyes: See **GroupPrioritizationExampleScene** for example.<br/><br/>
 Use **GroupPrioritizationmanager**, **GroupManager** and **SpeechSource** scripts for this feature.<br/>
- The **GroupPrioritizationManager** Script has a property `Group Managers List` where you can add **GroupManager** objects. If these are not assigned, it will automatically search for **GroupManager** in their children Game Objects. <br />
- Each **GroupManager** will have a property `Speech Source List` you can add **SpeechSource** objects into. If these are not assigned, it will automatically search for **SpeechSource** in their children Game Objects.<br />
- Each **SpeechSource** should have a corresponding **AudioSource**, and initially `is Not Focused` is true. The `is Keyword Detected` is used in combination with the Keyword Prioritization Feature.<br />

**Public Functions**:

`OnSelectedGroupChange()`: This function takes in the number corresponding to the selected group. It makes the sound volume for the newly selected group higher and sets all other groups to a lower volume. If the selected group number is 0, then all groups will be reset to the original volume. There is no output for this function.

<details><summary><b>Implementation Steps:</b></summary>

1. *Create a **Group Prioritization Manager** Object, and create several groups as its children.*
2. *Add Script **GroupPrioritizationManager** to the **Group Prioritization Manager** Object, and attach a **GroupManager** Script to each Group Object.*
3. *Add **SpeechSource** Script to different **AudioSources** involved in the different groups, and attach these **SpeechSource** scripts to the `Speech Source List` in their corresponding **GroupManager** Script.*
4. *Add the **GroupManger** Scripts to the `Group Managers List` in the corresponding **GroupPrioritizationManager** Script.*
5. *Call `OnSelectedGroupChange()` when the selected group is changed. See the documentation of the function above for more details.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/e7726d5b-49cd-4c1e-8964-fd3ffb3185df


<div id="keyword-prioritization"></div>

### 1.2) Keyword Prioritization
*(* :thumbsup: *Recommended to use in situations where important speech is delivered.)* <br />
 :eyes:  :eyes:  :eyes: See **KeywordPrioritizationExampleScene** for example.<br/><br/>
The **KeywordDetectionManager** is used to control this feature.<br/>
- It has a property, `keywords`, which is a List of strings that identify the keywords to look out for. <br/>
- It also allows you to set a notification sound by setting the `Notification Clip` variable. You could use the notification clip in `Sounds->notif1_inTheEnd.mp3` or any sound clip for the notification clip.<br/>

**Public Functions**:

`AddKeyword(string keyword)`: This function takes in a string and adds it to the list of keywords. If such a keyword already exists, it will log an error.

`SubtractKeyword(string keyword)`: This function takes in a string and removes it from the list of keywords. If such a keyword is not in the keyword list, it will log an error.

**Public Coroutines**:

`detectKeywordAndPlay(string script, SpeechSource speechSource)`: This coroutine locates keywords in the script and plays a notification sound. If the sentence that the keyword is a part of has lowered volume because of group prioritization, it also increases the volume of the sentence. When the sentence is done, it reverts to the volume before the keyword is detected. The inputs include the script, wherein the keyword might be detected, and the **SpeechSource** which will be used to output the sounds. There is an IEnumerator returned.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **Keyword Detection Manager** to the scene and attach a **KeywordDetectionManager** Script. Select the `notification clip`.*
2. *To enable the user to add or remove a keyword, call `AddKeyword(string)` and `SubtractKeyword(string)` as documented above.*
3. *Attach a **SpeechSource** Script to the **AudioSource** that should play the sentences and attach the **AudioSource** to the **SpeechSource**.*
4. *When playing a sentence that might contain the keyword and you want to detect it, instead of playing it with the audio source, start a `detectKeywordAndPlay` coroutine with the sentence's script, corresponding **SpeechSource**, and have the corresponding **AudioSource** contain the right **AudioClip**.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/98a22bcb-bd05-4966-bc7f-783efe25e694


<div id="sound-prioritization"></div>

### 1.3) Sound Prioritization
*(* :thumbsup: *Recommended to use in situations where character speech/ important sound and environment sounds/background music are concurrent.)* <br />
 :eyes:  :eyes:  :eyes: See **SoundPrioritizationExampleScene** for example.<br/><br/>
The **SoundPrioritizationManager** is used to control this feature. It has several properties to set before use, including the `Audio Mixer`, the `Env Vol Label`, the `Character Audio Source List`, and the `Lower Env On Speech Setting`.<br/>
- The `Audio Mixer` is the audio mixer that contains the mixer group for the environment sounds.<br/>
- The `Env Vol Label` is the exposed parameter from the environment audio mixer group. To do this, right-click on the volume in the audio mixer inspector of that audio mixer group and select “Expose “” to script”. It will then be accessible in exposed parameters.<br/>
- The `Character Audio Source List` should contain all of the character audio sources to keep track of. <br/>
- The boolean Lower Env On Speech Setting is default as true. It controls whether the feature will be turned on or off. It can be controlled by the developer, or by the user through ChangeLowerEnvOnSpeechSetting(), as described below.<br/>

Right before the character speech, call `LowerEnvSoundsVolume(audioSource)`, as described below.

**Public Functions**:

`ChangeLowerEnvOnSpeechSetting()`: This takes in the boolean input from the associated toggle. If the input is True, the environment volume will change when character audio is played, and if any are currently playing, the volume of the environment is decreased. If the input is False, the environment volume won't change when character audio is played, and if character audio is currently playing, it will reset the environment volume to normal. There is no output.

`LowerEnvSoundsVolume(AudioSource)`: This takes the input of an AudioSource, which should be the character audio source that is triggering this feature. This lowers the environment volume -30 dB and gives no output.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **Sound Prioritization Manager** to the scene and attach a **SoundPrioritizationManager** Script.*
2. *Create a new Mixer Group in the **AudioMixer** tab, and for all **AudioSoruce** that should be considered the Environment Sound, assign their Mixer group to be this new mixer group.*
3. *Go to the Inspector of the music mixer group controller, right-click on **Attenuation - Volume**, and select "Expose ... to script"*
4. *In the "Exposed parameters" list in the Audio Mixer tab, get the name of the newly created parameter.*
5. *Assign the Mixer that contains the new mixer group to the `Environment_Mixer` field in **SpeechPrioritizationManager**, and input the name of the new parameter to `Env Mixer Vol Label` field.*
6. *Add all **AudioSource** of Character speech to the `Character Audio Source List`.*
7. *Before the character speech, call `lowerEnvSoundsVolume(AudioSource)`, documented above.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/89e8aa06-dd85-4c75-89b2-9b9d2013ea3f

<div id="direction-based-prioritization"></div>

### 1.4) Direction-Based Prioritization
*(* :thumbsup: *Recommended to use in situations where users should prioritize the sound they are facing.)* <br />
The **DirectionPrioritizationManager** is used to control this feature. It has several properties to set before use, including the `Prioritization Audio Mixer`, the `General Audio Mixer`, the `General Audio Volume Label`, `Main Camera`, and the `Degree Threshold`.<br/>
- The `Prioritization Audio Mixer` is the AudioMixerGroup that will be used for the sound to prioritize.<br/>
- The `General Audio Mixer` is the AudioMixerGroup that will be used for all other sounds.<br/>
- The `General Audio Volume Label` is the exposed parameter from the general audio mixer group. To do this, right-click on the volume in the audio mixer inspector of that audio mixer group and select “Expose “” to script”. It will then be accessible in exposed parameters. <br/>
- The Main Camera should be assigned to the `Main Camera` field.<br/>
- The `Degree threshold` is used to determine whether a user is facing a sound source. It is the maximum degree of angle between their line of sight and the line connecting the location of the source to the player.<br/>

You will also need an **AudioManager** Script in the scene. Please see the implementation details below.

**Public Functions**:

`ToggleOnOff(bool toggle)`: This function toggles the function on and off.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add an **AudioManager** to the scene and attach a **AudioManager** Script.*
2. *Add an **DirectionPrioritizationManager** to the scene and attach a **DirectionPrioritizationManager** Script.*
3. *Create two new Mixer Group in the **AudioMixer** tab. Assign one to be the Prioritization Audio Mixer and the other to be the General Audio Mixer*
4. *Go to the Inspector of the music mixer group controller of the General Audio Mixer, right-click on **Attenuation - Volume**, and select "Expose ... to script"*
5. *In the "Exposed parameters" list in the Audio Mixer tab, get the name of the newly created parameter.*
6. *Input the name of the new parameter to `General Audio Volume Label` field.*
7. *Assign thee Main Camera to be the main camera that is linked to the player's head movement.*
8. *Set the Degree threshold. The default is 10 degrees.*
</details>



https://github.com/user-attachments/assets/3c458945-df81-452d-a360-a5f2f738845b




<div id="volume-and-pitch-adjustment"></div>

### 2.1) Volume and Pitch Adjustment
*(* :thumbsup: *Recommended to use for any sound, especially sounds on the lower/higher registry.)* <br />
 :eyes:  :eyes:  :eyes: See **VolPitchShiftExampleScene** for example.<br/><br/>
The **VolPitchShiftManager** is used to control this feature. It has several properties to set before use, including `Audio Mixer Group`, `Audio Sources List`, `Pitch Label`, and `Volume Label`.<br/>
- The Audio Mixer Group allows you to set the audio mixer group to be used.
- The Audio Sources List compiles all audio sources to be impacted by this volume and pitch control, with an empty or unset list resulting in system-wide changes.
- The Pitch and Volume Labels are the exposed parameters from the audio mixer. To do so, right-click on the pitch and volume in the audio mixer and select “Expose “” to script”. These will then be accessible in exposed parameters. <br/>

**Public Functions**: 

`ShiftPitch(float val)`: This function changes the pitch to the float input value for each audio mixer in the group. There is no output.

`ShiftVolume(float val)`: This function changes the volume to the float input value for each audio mixer in the group. There is no output.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **VolPitchShiftManager** Script to the Scene.*
3. *Add all **AudioSources** affected by this shift to the `Audio Sources List`. For system-level control, you can leave this field empty.*
2. *Create a new Mixer Group in the AudioMixer tab, and for all **AudioSoruce** that should be controlled by this feature, assign their Mixer group to be this new mixer group. Also, assign this to the `Audio Mixer Group` field in **VolPitchShiftManager***
4. *Go to the Inspector of the music mixer and add **Pitch shifter** using **Add Effect**. Then, right-click on **Volume** in **Attenuation** and **Pitch** in **Pitch Shifter**, and select "Expose ... to script" for both.*
4. *In the "Exposed parameters" list in the Audio Mixer tab, get the names of the newly created parameters.*
5. *Enter the new parameter names in the `Pitch Label` and `Volume Label` fields in the **VolPitchShiftManager** Script.*
6. *To shift the volume and pitch, use `ShiftPitch()` and `ShiftVolume()` documented above.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/a00cf925-13d5-4c4f-8bbb-6e6e8681a27f

https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/ee1cd19c-d8b2-4c97-9b69-6915e4e3236c


<div id="speech-speed-adjustment"></div>

### 2.2) Speech Speed Adjustment
*(* :thumbsup: *Recommended to use for character/system speech that contains important information.)* <br />
 :eyes:  :eyes:  :eyes: See **SpeechSpeedAdjustmentExampleScene** for example.<br/><br/>
The **SpeechSpeedManager** is used to control this feature.<br/>
- It requires you to assign an **AudioSource** to the script in the `Audio Source` field, otherwise, it will assume it is attached to a game object with an **AudioSource**.<br/>
- You also need to set the audio mixer to be used in the Master Mixer field. You then need to expose the Pitch element. To do so, right-click on the pitch in the audio mixer group controller and select “Expose “” to script”. These will then be accessible in exposed parameters. Then, add the parameter name of the pitch parameter to the `Audio Mixer Pitch Label` field.<br/>

**Public Functions**

`ShiftSpeed(float)`: This function changes the speed of the AudioSource linked to this manager by the value input into the function.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **SpeechSpeedAdjustmentManager** Script to the Scene.*
2. *Assign the **AudioSoruce** that should be controlled by this feature to the `Audio Source` field.*
3. *Create a new Mixer Group in the AudioMixer tab, and for the **AudioSoruce** that should be controlled by this feature, assign its Mixer group to be this new mixer group.*
4. *Go to the Inspector of the audio mixer and add **Pitch shifter** using **Add Effect**. Right-click on **Pitch** in **Pitch Shifter**, and select "Expose ... to script".*
5. *In the "Exposed parameters" list in the Audio Mixer tab, get the name of the newly created parameters.*
6. Assign the Mixer that contains this new mixer group to the field `Audio Mixer`.
7. *Enter the new parameter names in the `Audio Mixer Pitch Label` fields in the **SpeechSpeedAdjustmentManager** Script.*
8. *Use the `ShiftSpeed()` function documented above to shift the speed of the character's speech.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/e21d8592-825e-46bd-acf3-dd3d57adde21


<div id="frequency-contrast-enhancement"></div>

### 2.3) Frequency Contrast Enhancement
*(* :thumbsup: *Recommended to use for voices similar in pitch.)* <br />
The **ContrastEnhancementManager** is used to control this feature. It has the following variables: <br/>
- The `Frequency Threshold` is the threshold for the frequency difference between characters for this tool to be triggered.<br/>
- The `Dist Threshold` is the maximum distance between characters for this tool to be triggered.<br/>
- The `Master Mixer` is the mixer group that all sound sources that this tool could affect should be assigned to.<br/>

**Public Functions**

`OnContrastToggle(float)`: This function toggles the functionality of this tool on and off.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **SpeechSource** Script to each of the sources of speech that you wish to be modified by this tool, assign the `AudioSource` to the field.*
2. *Create an `AudioMixer` for each SpeechSource, and assign them to the corresponding SpeechSource Script.*
3. *Expose the pitch of each AudioMixer and input the name of that parameter into the corresponding `Mixer Value Name` field of each `SpeechSource`*
4. *The `Sample Size`, `Min Freq`, and `Max Freq` could be adjusted, but the default is 8192, 50, and 450, correspondingly.*
5. *Add a **ContrastEnhancementManager** Script to the Scene.*
6. *Adjust `Frequency Threshold` and `Dist Threshold` if needed.*
7. *Assign the `Audio Mixer Group` that contains the `Audio Mixer` of the target `SpeechSource` to the `Master Mixer` field.*
</details>



https://github.com/user-attachments/assets/8f0dfec3-6a1e-4193-bacd-b633f52b0192

<div id="beat-enhancement"></div>

### 2.4) Beat Enhancement
*(* :thumbsup: *Recommended to use for scenes where the music beat is an important part of the experience.)* <br />
The **BeatEnhancementManager** is used to control this feature. It has the following variables: <br/>
- The `Audio Source` is the Audio Source that is playing the music.<br/>
- The `Beat Mixer Group` is the AudioMixer Group used for the change of volume of the music.<br/>
- The `bpm` is the beats per minute of the music.<br/>

**Public Functions**

`StartBeatEnhancement()`: This function should be called to start the Beat Enhancement cycles, usually when the music starts playing.
`ChooseBeatEnhancementPattern(int)`: Choose the Beat Enhancement pattern. 0 is no Beat Enhancement. Input should be in {0, 1, 2}.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **BeatEnhancementManager** Script to the Scene and assign the `AudioSource` of the music to the `Audio Source` field.*
2. *Create an `Audio Mixer Group` for the Beat Enhancement modifications, and assign it to the `Beat Mixer Group` field.*
3. Set the `bpm` field to the BPM of the music.
4. Call the `StartBeatEnhancement` function when the music starts playing.
5. Use `ChooseBeatEnhancementPattern` to turn the feature on/off and choose between patterns.

</details>

https://github.com/user-attachments/assets/54b07858-b973-42ec-91df-b481d67e7900

<div id="shoulder-localization-helper"></div>

### 6) Shoulder Localization Helper
*(* :thumbsup: *Recommended to use in situations where the directional location of a sound-producing object is important to the experience.)* <br />
 :eyes:  :eyes:  :eyes: See **ShoulderLocalizationExampleScene** for example.<br/><br/>
The **ShoulderLocalizationManager** Script is used for this feature.<br/>
- The `Audio Source` field will be used to play the Shoulder Localization Helper notification sounds.<br/>
- The `Main Camera` field should contain the main camera of the user.<br/>
- An optional field is `targetAudioSource`, for the case where there's only one target.<br/>
- The last two fields are `leftAudioClip` and `rightAudioClip`, where the developer can input their own direction indicator sounds or the default sounds in `Sounds->Left.wav` and `Sounds->Right.wav`.<br/>

**Public Functions**

`PlayLocationAlert(Vector3)`: This function takes in the location of the target sound source location, determines if that target sound source is on the left side or right side of the camera, and plays the corresponding audio clip using the detected **AudioSoruce** in this script.

`PlayAlertWithDefinedTarget()`: This function takes in no parameter, and will call **PlayLocationAlert** using the optional `targetAudioSource` field as the sound source.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **ShoulderLocalizationManager** Scritp to the Scene.*
2. *Add an **AudioSource** to this object that has **ShoulderLocalizationManager** attached, or assign an **AudioSource** to the `Audio Source. field*
3. *Attach the main camera of the scene to the `Main Camera` Field.*
4. *If the feature is used for a fixed **AudioSource**, you could attach that **AudioSource** to the `Target Audio Source` field.*
5. *Attach the audio clips for indicating "To your left" and "To your right", in the `Left Audio Clip` and `Right Audio Clip` fields. The developer can input their own direction indicator sounds or use the default sounds in `Sounds->Left.wav` and `Sounds->Right.wav`.*
6. *When you want to play the **ShoulderLocalizationManager** alerts, call the function `PlayLocationAlert()` or `PlayAlertWithDefinedTarget()` as documented above.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/04f0b5ba-1bca-4f64-8470-417b97290560


<div id="live-listen-helper"></div>

### 7) Live Listen Helper
*(* :thumbsup: *Recommended to use in situations where user is needs to locate sound source inside a small, reachable environment)* <br />
 :eyes:  :eyes:  :eyes: See **LivelistenHelperExampleScene** for example.<br/><br/>
The **LiveListenHelperManager** Script is used for this feature.<br/>
- It should be attached to a game object that the user can grab and move around the scene.<br/>
- On the same object, there should be an **AudioListener** Component attached and should initially be set as a **disabled component**.<br/>
- The developer should add the list of **AudioSource** that they want the Live Listen Helper to apply to into the field `Audio Source List`.<br/>
- The developer could also edit the cutoff of the sound single-out effect, the default of the cutoff is 0.5f.<br/>
- To enable the user to start using and finish using the feature, the developer should call the `StartUsingLiveListenHelper` and `StopUsingLiveListenHelper` functions as the user picks up and drops the object.<br/>

**Public Functions**
`StartUsingLiveListenHelper()`: This function takes in no parameters. It sets the **AudioListener** of the game from the default listener on the player camera to the **AudioListener** attached to the Live Listen Helper object. It will also start the single-out sound effects of the Live Listen Helper.

`StopUsingLiveListenHelper()`: This function stops using the Live Listen Helper by switching back the **AudioListener** to the original and stops the effect of the Live Listen Helper.

<details><summary><b>Implementation Steps:</b></summary>

1. *Instantiate a ball (or other grabbable objects of your choice) in the Scene, and attach **LiveListenHelperManager** Script to the ball/object.*
2. *Add all the **AudioSources** to be affected by the Live Listen Helper feature to the `AudioSourceList` field.*
3. *Add an **Audio Listener** to this ball/object and disable this component.*
4. *Change the `Cutoff` field if needed.*
5. *Call `StartUsingLiveListenHelper()` and `StopUsingLiveListenHelper()` as documented above when you want to start and stop using this ball as the Live Listening Tool. One way is to start it when the ball is grabbed and stop when the ball is released (as shown in the video below).*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/fa82c304-8386-4702-9cae-acadb6b5da2e


<div id="smart-notification"></div>

### 8) Smart Notification
*(* :thumbsup: *Recommended to use before important sound message is played.)* <br />
 :eyes:  :eyes:  :eyes: See **SmartNotificationExampleScene** for example.<br/><br/>
This feature uses the script **SmartNotificationManager** to control the on and off, and the sounds played as the smart notification.<br/>
- The script has a public boolean variable `smartNotificationOn` to indicate whether the smart notification feature is on or off.<br/>
- The developer also needs to put the notification clip in the Notification Clip field, with the default provided in the toolkit in `Sounds->notif1_inTheEnd.mp3`.
- Before playing an important sound, the developer could start a `PlaySmartNotification()` Coroutine.

**Public Functions**
`ToggleSmartNotification(bool)`: This function will turn the Smart notification feature on/off by changing the public flag `smartNotificationOn` variable.

**Public Coroutine**
`PlaySmartNotification(AudioSource)`: You need to pass in the AudioSource that will be used to play the following important sound. This Coroutine will play the notification sound selected followed by the audio clip of the AudioSource that is passed in.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **SmartNotificationManager** Script to the scene.*
2. *Add the default notification clip in `Sounds->notif1_inTheEnd.mp3` or another clip of the developer's choice into the `Notification Clip` Field.*
3. *Use `ToggleSmartNotification` to toggle the Smart Notification Feature on or off.*
4. *When playing a sound where you want a notification played before the sound, start the `PlaySmartNotification` Coroutine with the **AudioSource** of the important sound passed in as the parameter.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/b92e57b2-088c-4897-b7a6-51d886151ad1


<div id="custom-feedback-sound"></div>

### 9) Custom Feedback Sound
*(* :thumbsup: *Recommended to use when there are feedback sounds in the program.)* <br />
 :eyes:  :eyes:  :eyes: See **CustomFeedbackSoundExampleScene** for example.<br/><br/>
This feature is managed by the Script **CustomFeedbackManager**. It currently supports a correct and incorrect feedback sound.<br/>
- The developer should input the **AudioClip** Files of the correct and incorrect feedback sounds in the `Correct/Incorrect Feedback Clips List`.<br/>
- Then, the developer should put the index of the default feedback sounds into `correctFeedbackIndex` and `incorrectFeedbackIndex` fields. The default indices would be 0.<br/>
- The `audioSource` field will be used to play the feedback sounds. *See some example Feedback sounds in the Sounds-FeedbackSounds folder.*<br/>

**Public Functions**
`SelectCorrectFeedback(int)`: This function sets the correct feedback index to be the input int.

`SelectIncorrectFeedback(int)`: This function sets the incorrect feedback index to be the input int.

`PlayCorrectFeedbackSound()`: This function loads and plays the correct notification sound from the audio source linked to the script.

`PlayIncorrectFeedbackSound()`: This function loads and plays the correct notification sound from the audio source linked to the script.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **CustomFeedbackManager** Script to the Scene.*
2. *Enter the List of the **AudioClips** for the correct and incorrect feedback.*
4. *Add the **AudioSource** where the feedback sounds should be played to the `Audio Source` field.*
5. *To enable the user to change the feedback sounds used, use the `SelectCorrectFeedback()` and `SelectIncorrectFeedback()` as documented above.*
6. *To play the feedback sounds, use `PlayCorrectFeedbackSound()` or `PlayIncorrectFeedbackSound()` as documented above.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/e1fbfebf-2059-4096-b323-22d097b61f42

