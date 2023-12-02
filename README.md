# Sound-Customization-VR-Accessibility Feature Toolkit
 
## 1. Introduction
This is a Unity Toolkit that contains nine features for Sound Customization for DHH (Deaf and Hard of Hearing) Users. It is designed with VR development in mind, but it might be used for other development as well.

## 2. Key Definition
**Sound Customization**: Changing aspects of the sounds used in the VR app such as shifting frequencies of the sound or prioritizing certain sounds to satisfy the needs of individual DHH users.

## 3. Features List
This is the list of features. They are divided into 4 categories - ***Prioritization, Sound Parameter Changes, Spatial Assistance, and Add-on Sounds***. Each feature should work independently, and some features could work together.
### 3.1 Prioritization
**Group Prioritization**: When having multiple groups of sounds, focus on a specific group and lower sounds of all other groups.<br />
**Keyword Prioritization**: Set specific keywords you're interested in, and the program will play a notification and increase speech volume when it's played.<br />
**Speech Prioritization**: Lower environment sounds during speech.

### 3.2 Sound Parameter Changes
**Volume and Pitch Management**: Set volume and pitch both for the system and for individual sound sources in the environment.<br />
**Speech Speed Adjustment**: Adjust speed for individual speech sound sources.

### 3.3 Spatial Assistance
**Shoulder Localization Helper**: To alert you when an important sound is played/ by your request, and if it’s on your left/right.<br />
**Live Listen Helper**: Single out sounds when you move them close to sound sources.

### 3.4 Add-on Sounds
**Smart Notification**: Playing a notification before an important sound, like speech or feedback.<br />
**Custom Feedback Sound**: Change the feedback sounds in the system to your individual preferences/needs.

## 4. Usage Guide
### 1) Group Prioritization
See example in Group Prioritization Example Scene.<br />
The **GroupPrioritizationManager** Script has a property `Group Managers List` where you can add **GroupManager** objects. If these are not assigned, it will automatically search for **GroupManager** in their children Game Objects. <br />
Each **GroupManager** will have a property `Speech Source List` you can add **SpeechSource** objects into. If these are not assigned, it will automatically search for **SpeechSource** in their children Game Objects.<br />
Each **SpeechSource** should have a corresponding **AudioSource**, and initially `is Not Focused` is true. The `is Keyword Detected` is used in combination with the Keyword Prioritization Feature.<br />

**Public Functions**:

`OnSelectedGroupChange()`: This function takes in the number corresponding to the selected group. It makes the sound volume for the newly selected group higher and sets all other groups to a lower volume. If the selected group number is 0, then all groups will be reset to the original volume. There is no output for this function.

**Implementation Steps:**
1. *Create a **Group Prioritization Manager** Object, and create several groups as its children.*
2. *Add Script **GroupPrioritizationManager** to the **Group Prioritization Manager** Object, and attach a **GroupManager** Script to each Group Object.*
3. *Add **SpeechSource** Script to different **AudioSources** involved in the different groups, and attach these **SpeechSource** scripts to the `Speech Source List` in their corresponding **GroupManager** Script.*
4. *Add the **GroupManger** Scripts to the `Group Managers List` in the corresponding **GroupPrioritizationManager** Script.*
5. *Call `OnSelectedGroupChange()` when the selected group is changed. See the documentation of the function above for more details.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/a0d4451d-3a63-47f3-967a-7e1bd93900ec

### 2) Keyword Prioritization
See example in Keyword Prioritization Example Scene. <br/>
The **KeywordDetectionManager** is used to control this feature. It has a property, `keywords`, which is a List of strings that identify the keywords to look out for. <br/>
It also allows you to set a notification sound by setting the `Notification Clip` variable. <br/>

**Public Functions**: 

`AddOrSubtractKeyword(bool isAdding, string keyword)`: This function allows users to add a keyword to the keywords list or remove them, based on the value of the `isAdding` variable. If this variable is True, then this function will add the keyword. If it is False, the keyword will be removed. There is no return from this function.

**Public Coroutines**:

`detectKeywordAndPlay(string script, SpeechSource speechSource, AudioClip audioClip)`: This coroutine locates keywords in the script and plays a notification sound. It also increases the volume of the sentence the keyword is a part of. When the sentence is done, it reverts to the volume prior to the keyword being detected. The inputs include the script, wherein the keyword might be detected, the speechSource, and the audio clip, both of which will be used to output the sounds. The output is an IEnumerator, which could be started with a `StartCoroutine()`.

**Implementation Steps:**
1. *Add a **Keyword Detection Manager** to the scene and attach a **KeywordDetectionManager** Script.*
2. *To add a keyword, call `AddOrSubtractKeyword()` as documented above.*
3. *Attach a **SpeechSource** Script to the **AudioSource** that will play the sentences and attach the **AudioSource** to the **SpeechSource**.*
4. *When playing a sentence that might contain the keyword and you want to detect it, start the `detectKeywordAndPlay` coroutine with the sentence's script, corresponding **SpeechSource**, and corresponding **AudioClip**.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/fb1c8767-987c-4594-a488-c216781977a9


### 3) Speech Prioritization
See example in Speech Prioritization Example Scene. <br/>
The **SpeechPrioritizationManager** is used to control this feature. It has several properties to set before use, including the `Environment Mixer`, the `Env Mixer Vol Label`, and the `Character Audio Source List`. The environment mixer is the audio mixer for the environment sounds. The Env Mixer Vol Label is the exposed parameter from the environment audio mixer. To do this, right-click on the volume in the audio mixer inspector and select “Expose “” to script”. It will then be accessible in exposed parameters. The character audio source list contains all of the character audio sources to keep track of. <br/>
Before the character speech, call `LowerEnvSoundsVolume()`, and after the character finishes speaking, run the `RecoverEnvSoundsVolume` Coroutine, both described below.

**Public Functions**:

`ChangeLowerEnvOnSpeechSetting()`: This takes in the boolean input from the associated toggle. If the input is True, all character audio sources in the list are checked and if any are playing, the volume of the environment is decreased. If the input is False, the environment volume is reset. There is no output.

`LowerEnvSoundsVolume()`: This sets the environment volume to -30 and gives no output.

**Public Coroutines**:

`recoverEnvSoundsVolume()`: This resets the environment volume once no characters are speaking. There is an IEnumerator returned. 

**Implementation Steps:**
1. *Add a **Speech Prioritization Manager** to the scene and attach a **SpeechPrioritizationManager** Script.*
2. *Create a new Mixer in the **AudioMixer** tab, and for all **AudioSoruce** that should be considered the Environment Sound, assign their Mixer group to be this new mixer.*
3. *Go to the Inspector of the music mixer, right-click on **Volume**, and select "Expose ... to script"*
4. *In the "Exposed parameters" list in the Audio Mixer tab, get the name of the newly created parameter.*
5. *Assign the new Mixer to the `Environment_Mixer` field in **SpeechPrioritizationManager**, and input the name of the new parameter to `Env Moxer Vol Label` field.*
6. *Add all **AudioSource** of Character speech to the `Character Audio Source List`.*
7. *Before the character speech, call `lowerEnvSoundsVolume()`, and after the character finishes speaking, run the `RecoverEnvSoundsVolume` Coroutine, both documented above.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/1ef3bbd5-ab21-4c19-9ec8-122664c0a60c

### 4) Volume and Pitch Adjustment
See example in Volume and Pitch Management Example Scene. <br/>
The **VolPitchShiftManager** is used to control this feature. It has several properties to set before use, including `Audio User Group`, `Audio Sources List`, `Pitch Label`, and `Volume Label`. The Audio Mixer Group allows you to set the audio mixer to be used. The Audio Sources List compiles all audio sources to be impacted by this volume and pitch control, with an empty or unset list resulting in system-wide changes. The Pitch and Volume Labels are the exposed parameters from the audio mixer. To do so, right-click on the pitch and volume in the audio mixer and select “Expose “” to script”. These will then be accessible in exposed parameters. <br/>

**Public Functions**: 

`ShiftPitch(float val)`: This function changes the pitch to the float input value for each audio mixer in the group. There is no output.

`ShiftVolume(float val)`: This function changes the volume to the float input value for each audio mixer in the group. There is no output.

**Implementation Steps:**
1. *Add a **VolumePitchShiftManager** Script to the Scene.*
3. *Add all **AudioSources** affected by this shift to the `Audio Sources List`. For system-level control, you can leave this field empty.*
2. *Create a new Mixer in the AudioMixer tab, and for all **AudioSoruce** that should be controlled by this feature, assign their Mixer group to be this new mixer.*
4. *Go to the Inspector of the music mixer, right-click on **Volume** in **Attenuation** and **Pitch** in **Pitch Shifter**, and select "Expose ... to script" for both.*
4. *In the "Exposed parameters" list in the Audio Mixer tab, get the names of the newly created parameters.*
5. *Enter the new parameter names in the `Pitch Label` and `Volume Label` fields in the **VolPitchShiftManager** Script.*
6. *To shift the volume and pitch, use `ShiftPitch()` and `ShiftVolume()` documented above.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/a2b2f449-8776-4f9f-9a66-2a1372f41c4c

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/db57b924-ec4e-42ed-a51b-16bb0f2d0f06

### 5) Speech Speed Adjustment
See example in Speech Speed Adjustment Example Scene. <br/>
The **SpeechSpeedManager** is used to control this feature. It requires you to assign an **AudioSource** to the script in the `Audio Source` field, otherwise, it will assume it is attached to a game object with an **AudioSource**. You also need to set the audio mixer to be used in the Master Mixer field. You then need to expose the Pitch element in the AudioMixer and add the parameter name of the pitch parameter to the `audioMixerPitchLabel` field.

**Public Functions**

`ShiftSpeed(float)`: This function changes the speed of the AudioSource linked to this manager by the value input into the function.

**Implementation Steps:**
1. *Add a **SpeechSpeedAdjustmentManager** in the Scene.*
2. *Assign the **AudioSoruce** that should be controlled by this feature to the `Audio Source` field.*
3. *Create a new Mixer in the AudioMixer tab, and for the **AudioSoruce** that should be controlled by this feature, assign its Mixer group to be this new mixer.*
4. *Go to the Inspector of the music mixer, right-click on **Pitch** in **Pitch Shifter**, and select "Expose ... to script" for both.*
5. *In the "Exposed parameters" list in the Audio Mixer tab, get the name of the newly created parameters.*
6. *Enter the new parameter names in the `Audio Mixer Pitch label` fields in the **SpeechSpeedAdjustmentManager** Script.*
7. *Use the `ShiftSpeed()` function documented above to shift the speed of the character's speech.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/821f3831-8010-4157-8ffe-ceb5c6d27ae6


### 6) Shoulder Localization Helper
See example in Shoulder Localization Helper Example Scene. <br/>
The **ShoulderLocalizationManager** Script is used for this feature. It requires you to attach the main camera of the player to the `Main Camera` field. It requires you to assign an **AudioSource** to the script in the `Audio Source` field, otherwise, it will assume it is attached to a game object with an **AudioSource**. One optional field is `targetAudioSource`, for the case where there's only one target. Two additional optional fields are `leftAudioClip` and `rightAudioClip`, where the developer can input their own direction indicator than the default sounds.

**Public Functions**
`PlayLocationAlert(Vector3)`: This function takes in the location of the target sound source location, determines if that target sound source is on the left side or right side of the camera, and plays the corresponding audio clip using the detected **AudioSoruce** in this script.

`PlayAlertWithDefinedTarget()`: This function takes in no parameter, and will call **PlayLocationAlert** using the optional `targetAudioSource` field as the sound source.

**Implementation Steps:**
1. *Add a **ShoulderLocalizationManager** Scritp to the Scene.*
2. *Add an **AudioSource** to this object that has **ShoulderLocalizationManager** attached.*
3. *Attach the main camera of the scene to the `Main Camera` Field.*
4. *If the feature is used for a fixed **AudioSource**, you could attach that **AudioSource** to the `Target Audio Source` field.*
5. *If you wish to use audio clips or notification clips other than the default sounds "To your left" and "To your right", attach them in the `Left Audio Clip` and `Right Audio Clip` fields.*
6. *When you want to play the **ShoulderLocalizationManager** alerts, call the function `PlayLocationAlert()` or `PlayAlertWithDefinedTarget()` as documented above.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/45d2db36-bea6-479f-a17e-a2fd176c55c1


### 7) Live Listen Helper
See example in Live Listen Helper Example Scene. <br/>
The **LiveListenHelperManager** Script is used for this feature. It should be attached to a game object that the user can grab and move around the scene. On the same object, there should be an **AudioListener** Component attached. The developer should add the list of **AudioSource** that they want the Live Listen Helper to apply to into the field `Audio Source List`. The developer could also edit the cutoff of the sound single-out effect, the default of the cutoff is 0.5f. To enable the user to start using and finish using the feature, the developer should call the `StartUsingLiveListenHelper` and `StopUsingLiveListenHelper` functions as the user picks up and drops the object.

**Public Functions**
`StartUsingLiveListenHelper()`: This function takes in no parameters. It sets the **AudioListener** of the game from the default listener on the player camera to the **AudioListener** attached to the Live Listen Helper object.

`StopUsingLiveListenHelper()`: This function stops using the Live Listen Helper by switching back the **AudioListener** used and stops the effect of the Live Listen Helper.

**Implementation Steps:**
1. *Instantiate a ball (or other grabbable object of your choice) in the Scene, and attach **LiveListenHelperManager** Scritp to the ball/object.*
2. *Add all the **AudioSources** to be affected by the Live Listen Helper feature to the `AudioSourceList` field.*
3. *Add an **Audio Listener** to this ball/object.*
4. *Change the `Cutoff` field if needed.*
5. *Call `StartUsingLiveListenHelper()` and `StopUsingLiveListenHelper()` as documented above when you want to start and stop using this ball as the Live Listening Tool. One way is to start it when the ball is grabbed and stop when the ball is released (as shown in the video below).*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/001c540c-0072-494e-9e65-dd1ce43be5b3


### 8) Smart Notification
See example in Smart Notification Example Scene. <br/>
This feature uses the script **SmartnotificationManager** to control the on and off, and the sounds played as the smart notification. The script has a public boolean variable `smartNotificationOn` to indicate whether the smart notification feature is on or off. The developer can also customize the notification clip in the Notification Clip field, with the default provided in the toolkit. Before playing an important sound, the developer could call `PlaySmartNotification()`.

**Public Functions**
`ToggleSmartNotification(bool)`: This function will turn the Smart notification feature on/off by changing the public flag `smartNotificationOn` variable.

**Public Coroutine**
`PlaySmartNotification(AudioSource)`: This Coroutine will play the notification sound selected. You need to pass in an AudioSource to play the smart notification. It will not change the original AudioClip attached to the AudioSource.

**Implementation Steps:**
1. *Add a **SmartNotificationManager** Script to the scene.*
2. *If you want to use a notification sound other than the default notification clip, then add it to the `Notification Clip` Field.*
3. *Use `ToggleSmartNotification` to toggle the Smart Notification Feature on or off.*
4. *When playing a sound where you want a notification played before the sound, start the `PlaySmartNotification` Coroutine with the **AudioSource** passed in as the parameter.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/c906ed4a-8ba2-4d19-bdf2-f7327c4e7296

### 9) Custom Feedback Sound
This feature is managed by the Script CustomFeedbackManager. It currently supports a correct and incorrect feedback sound. The developer should input the file names of the correct and incorrect feedback sounds in the `Correct/Incorrect Feedback Name List`. Then, the developer should put the filename of the default feedback sounds into `correctFeedbackName` and `incorrectFeedbackName` fields. The default would be the first one on the corresponding Feedback Name List. The `path` field is used for providing the path to the folder that contains all the notification sound files. The `audioSource` field will be used to play the feedback sounds.

**Public Functions**
`SelectCorrectFeedback(int)`: This function sets the correct feedback to the file corresponding to the input index on the `correctFeedbackNameList`.

`SelectIncorrectFeedback(int)`: This function sets the incorrect feedback to the file corresponding to the input index on the `incorrectFeedbackNameList`.

`PlayCorrectFeedbackSound()`: This function loads and plays the correct notification sound from the audio source linked to the script.

`PlayIncorrectFeedbackSound()`: This function loads and plays the correct notification sound from the audio source linked to the script.

**Implementation Steps:**
1. *Add a **CustomFeedbackManager** Script to the Scene.*
2. *Enter the List of the file names for the correct and incorrect feedback.*
3. *Enter the path to the folder that contains all the feedback sound files.*
4. *Add the **AudioSource** where the feedback sounds are supposed to be played to the `Audio Source` field.*
5. *To change the feedback sounds used, use the `SelectCorrectFeedback()` and `SelectIncorrectFeedback()` as documented above.*
6. *To play the feedback sounds, use `PlayCorrectFeedbackSound()` or `PlayIncorrectFeedbackSound()` as documented above.*

https://github.com/xinyun-cao/Feature-Playground-Sound-Customization-VR-DHH/assets/144272763/9c376886-3e9f-4214-9ef1-9cd5c5dcda82
