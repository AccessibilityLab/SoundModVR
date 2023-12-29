# Sound-Customization-VR-Accessibility Feature Toolkit
 
## 1. Introduction
This is a Unity Toolkit that contains nine features for Sound Customization for DHH (Deaf and Hard of Hearing) Users. It is designed with VR development in mind, but it might be used for other development as well.

## 2. Key Definition
**Sound Customization**: Changing aspects of the sounds used in the VR app such as shifting frequencies of the sound or prioritizing certain sounds to satisfy the needs of individual DHH users.<br />
**Unity Audio Mixer**: You can access the AudioMixer window from Window->Audio->AudioMixer. For more information about AudioMixer, please see Unity documentation: [AudioMixer](https://docs.unity3d.com/Manual/AudioMixer.html) and [AudioMixer Scripting](https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.html).

## 3. Features List
This is the list of features. They are divided into 4 categories - ***Prioritization, Sound Parameter Changes, Spatial Assistance, and Add-on Sounds***. Each feature should work independently, and some features could work together.
### 3.1 Prioritization
[**Group Prioritization**](#group-prioritization): When having multiple groups of sounds, focus on a specific group and lower sounds of all other groups.<br />
[**Keyword Prioritization**](#keyword-prioritization): Set specific keywords you're interested in, and the program will play a notification and increase speech volume when it's played.<br />
[**Sound Prioritization**](#sound-prioritization): Lower environment sounds during speech/important sounds.

### 3.2 Sound Parameter Changes
[**Volume and Pitch Adjustment**](#volume-and-pitch-adjustment): Set volume and pitch both for the system and for individual sound sources in the environment.<br />
[**Speech Speed Adjustment**](#speech-speed-adjustment): Adjust speed for individual speech sound sources.

### 3.3 Spatial Assistance
[**Shoulder Localization Helper**](#shoulder-localization-helper): To alert you when an important sound is played/ by your request, and if it’s on your left/right.<br />
[**Live Listen Helper**](#live-listen-helper): Single out sounds when you move them close to sound sources.

### 3.4 Add-on Sounds
[**Smart Notification**](#smart-notification): Playing a notification before an important sound, like speech or feedback.<br />
[**Custom Feedback Sound**](#custom-feedback-sound): Change the feedback sounds in the system to your individual preferences/needs.

## 4. Usage Guide
<div id="group-prioritization"></div>

### 1) Group Prioritization 
*(* :thumbsup: *Recommended to use in situations where multiple groups of conversation/sounds are happening concurrently.)* <br />
 :eyes:  :eyes:  :eyes: See **GroupPrioritizationExampleScene** for example.<br/><br/>
The **GroupPrioritizationManager** Script has a property `Group Managers List` where you can add **GroupManager** objects. If these are not assigned, it will automatically search for **GroupManager** in their children Game Objects. <br />
Each **GroupManager** will have a property `Speech Source List` you can add **SpeechSource** objects into. If these are not assigned, it will automatically search for **SpeechSource** in their children Game Objects.<br />
Each **SpeechSource** should have a corresponding **AudioSource**, and initially `is Not Focused` is true. The `is Keyword Detected` is used in combination with the Keyword Prioritization Feature.<br />

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

### 2) Keyword Prioritization
*(* :thumbsup: *Recommended to use in situations where important speech is delivered.)* <br />
 :eyes:  :eyes:  :eyes: See **GroupPrioritizationExampleScene** for example.<br/><br/>
The **KeywordDetectionManager** is used to control this feature. It has a property, `keywords`, which is a List of strings that identify the keywords to look out for. <br/>
It also allows you to set a notification sound by setting the `Notification Clip` variable. You could use the notification clip in `Sounds->notif1_inTheEnd.mp3` or any sound clip for the notification clip.<br/>

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

### 3) Sound Prioritization
*(* :thumbsup: *Recommended to use in situations where character speech/ important sound and environment sounds/background music are concurrent.)* <br />
 :eyes:  :eyes:  :eyes: See **SoundPrioritizationManagerExampleScene** for example.<br/><br/>
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


<div id="volume-and-pitch-adjustment"></div>

### 4) Volume and Pitch Adjustment
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

### 5) Speech Speed Adjustment
*(* :thumbsup: *Recommended to use for character/system speech that contains important information.)* <br />
The **SpeechSpeedManager** is used to control this feature. It requires you to assign an **AudioSource** to the script in the `Audio Source` field, otherwise, it will assume it is attached to a game object with an **AudioSource**. You also need to set the audio mixer to be used in the Master Mixer field. You then need to expose the Pitch element in the AudioMixer and add the parameter name of the pitch parameter to the `audioMixerPitchLabel` field.

**Public Functions**

`ShiftSpeed(float)`: This function changes the speed of the AudioSource linked to this manager by the value input into the function.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **SpeechSpeedAdjustmentManager** in the Scene.*
2. *Assign the **AudioSoruce** that should be controlled by this feature to the `Audio Source` field.*
3. *Create a new Mixer in the AudioMixer tab, and for the **AudioSoruce** that should be controlled by this feature, assign its Mixer group to be this new mixer.*
4. *Go to the Inspector of the music mixer, right-click on **Pitch** in **Pitch Shifter**, and select "Expose ... to script" for both.*
5. *In the "Exposed parameters" list in the Audio Mixer tab, get the name of the newly created parameters.*
6. *Enter the new parameter names in the `Audio Mixer Pitch label` fields in the **SpeechSpeedAdjustmentManager** Script.*
7. *Use the `ShiftSpeed()` function documented above to shift the speed of the character's speech.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/e21d8592-825e-46bd-acf3-dd3d57adde21


<div id="shoulder-localization-helper"></div>

### 6) Shoulder Localization Helper
*(* :thumbsup: *Recommended to use in situations where the directional location of a sound-producing object is important to the experience.)* <br />
The **ShoulderLocalizationManager** Script is used for this feature. It requires you to attach the main camera of the player to the `Main Camera` field. It requires you to assign an **AudioSource** to the script in the `Audio Source` field, otherwise, it will assume it is attached to a game object with an **AudioSource**. An optional field is `targetAudioSource`, for the case where there's only one target. The last two fields are `leftAudioClip` and `rightAudioClip`, where the developer can input their own direction indicator sounds or the default sounds in `Sounds->Left.wav` and `Sounds->Right.wav`.

**Public Functions**

`PlayLocationAlert(Vector3)`: This function takes in the location of the target sound source location, determines if that target sound source is on the left side or right side of the camera, and plays the corresponding audio clip using the detected **AudioSoruce** in this script.

`PlayAlertWithDefinedTarget()`: This function takes in no parameter, and will call **PlayLocationAlert** using the optional `targetAudioSource` field as the sound source.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **ShoulderLocalizationManager** Scritp to the Scene.*
2. *Add an **AudioSource** to this object that has **ShoulderLocalizationManager** attached.*
3. *Attach the main camera of the scene to the `Main Camera` Field.*
4. *If the feature is used for a fixed **AudioSource**, you could attach that **AudioSource** to the `Target Audio Source` field.*
5. *Attach the audio clips for indicating "To your left" and "To your right", in the `Left Audio Clip` and `Right Audio Clip` fields. The developer can input their own direction indicator sounds or use the default sounds in `Sounds->Left.wav` and `Sounds->Right.wav`.*
6. *When you want to play the **ShoulderLocalizationManager** alerts, call the function `PlayLocationAlert()` or `PlayAlertWithDefinedTarget()` as documented above.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/04f0b5ba-1bca-4f64-8470-417b97290560


<div id="live-listen-helper"></div>

### 7) Live Listen Helper
*(* :thumbsup: *Recommended to use in situations where user is needs to locate sound source inside a small, reachable environment)* <br />
The **LiveListenHelperManager** Script is used for this feature. It should be attached to a game object that the user can grab and move around the scene. On the same object, there should be an **AudioListener** Component attached. The developer should add the list of **AudioSource** that they want the Live Listen Helper to apply to into the field `Audio Source List`. The developer could also edit the cutoff of the sound single-out effect, the default of the cutoff is 0.5f. To enable the user to start using and finish using the feature, the developer should call the `StartUsingLiveListenHelper` and `StopUsingLiveListenHelper` functions as the user picks up and drops the object.

**Public Functions**
`StartUsingLiveListenHelper()`: This function takes in no parameters. It sets the **AudioListener** of the game from the default listener on the player camera to the **AudioListener** attached to the Live Listen Helper object.

`StopUsingLiveListenHelper()`: This function stops using the Live Listen Helper by switching back the **AudioListener** used and stops the effect of the Live Listen Helper.

<details><summary><b>Implementation Steps:</b></summary>

1. *Instantiate a ball (or other grabbable object of your choice) in the Scene, and attach **LiveListenHelperManager** Scritp to the ball/object.*
2. *Add all the **AudioSources** to be affected by the Live Listen Helper feature to the `AudioSourceList` field.*
3. *Add an **Audio Listener** to this ball/object.*
4. *Change the `Cutoff` field if needed.*
5. *Call `StartUsingLiveListenHelper()` and `StopUsingLiveListenHelper()` as documented above when you want to start and stop using this ball as the Live Listening Tool. One way is to start it when the ball is grabbed and stop when the ball is released (as shown in the video below).*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/fa82c304-8386-4702-9cae-acadb6b5da2e


<div id="smart-notification"></div>

### 8) Smart Notification
*(* :thumbsup: *Recommended to use before important sound message is played.)* <br />
This feature uses the script **SmartNotificationManager** to control the on and off, and the sounds played as the smart notification. The script has a public boolean variable `smartNotificationOn` to indicate whether the smart notification feature is on or off. The developer also need to put the notification clip in the Notification Clip field, with the default provided in the toolkit in `Sounds->notif1_inTheEnd.mp3`. Before playing an important sound, the developer could call `PlaySmartNotification()`.

**Public Functions**
`ToggleSmartNotification(bool)`: This function will turn the Smart notification feature on/off by changing the public flag `smartNotificationOn` variable.

**Public Coroutine**
`PlaySmartNotification(AudioSource)`: This Coroutine will play the notification sound selected. You need to pass in an AudioSource to play the smart notification. It will not change the original AudioClip attached to the AudioSource.

<details><summary><b>Implementation Steps:</b></summary>

1. *Add a **SmartNotificationManager** Script to the scene.*
2. *Add the default notification clip in `Sounds->notif1_inTheEnd.mp3` or another clip of the developer's choice into the `Notification Clip` Field.*
3. *Use `ToggleSmartNotification` to toggle the Smart Notification Feature on or off.*
4. *When playing a sound where you want a notification played before the sound, start the `PlaySmartNotification` Coroutine with the **AudioSource** passed in as the parameter.*
</details>


https://github.com/xinyun-cao/SoundCusVR-Feature-Toolkit/assets/144272763/b92e57b2-088c-4897-b7a6-51d886151ad1


<div id="custom-feedback-sound"></div>

### 9) Custom Feedback Sound
*(* :thumbsup: *Recommended to use when there are feedback sounds in the program.)* <br />
This feature is managed by the Script **CustomFeedbackManager**. It currently supports a correct and incorrect feedback sound. The developer should input the **AudioClip** Files of the correct and incorrect feedback sounds in the `Correct/Incorrect Feedback Clips List`. Then, the developer should put the index of the default feedback sounds into `correctFeedbackIndex` and `incorrectFeedbackIndex` fields. The default indices would be 0. The `audioSource` field will be used to play the feedback sounds. *See some example Feedback sounds in the Sounds-FeedbackSounds folder.*

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

