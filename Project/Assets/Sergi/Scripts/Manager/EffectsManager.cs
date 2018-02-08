using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class EffectsManager : MonoBehaviour {
    public PostProcessingProfile ppProfile;
    public int DayState = 0;
    public Environment_State EnvState;

    private void Start() {
        PostProcessingSettings();
        EventManager.DayCycle += SDT;
        SDT();     
    }
    void SDT() {
        Debug.Log("Day state");
        StartCoroutine("Change_Day_State");
    }
    void PostProcessingSettings() {
        ColorGradingModel.Settings colorgrading = ppProfile.colorGrading.settings;
        colorgrading.tonemapping.neutralBlackOut = -0.09f;
        colorgrading.channelMixer.red = new Vector3(1, 0, 0);
        ppProfile.colorGrading.settings = colorgrading;
    }
    //Day States
    public IEnumerator Change_Day_State() {
        switch (DayState) {
            case (0):
                //copy current bloom settings from the profile into a temporary variable
                ColorGradingModel.Settings colorgrading = ppProfile.colorGrading.settings;
                colorgrading.channelMixer.red = new Vector3(2, 0, 0);
                //change the intensity in the temporary settings variable
                while (colorgrading.tonemapping.neutralBlackOut < 0) {
                    colorgrading.tonemapping.neutralBlackOut = colorgrading.tonemapping.neutralBlackOut + 0.005f;
                    yield return new WaitForSeconds(0.05f);
                }

                //set the bloom settings in the actual profile to the temp settings with the changed value
                ppProfile.colorGrading.settings = colorgrading;
                DayState = 1;
                break;
            case (1):
                Debug.Log("State 1");
                //copy current bloom settings from the profile into a temporary variable
                ColorGradingModel.Settings colorgrading1 = ppProfile.colorGrading.settings;
                colorgrading1.channelMixer.red = new Vector3(1, 0, 0);
                //change the intensity in the temporary settings variable
                //set the bloom settings in the actual profile to the temp settings with the changed value
                ppProfile.colorGrading.settings = colorgrading1;
                DayState = 2;
                break;
            case (2):
                //copy current bloom settings from the profile into a temporary variable
                ColorGradingModel.Settings colorgrading2 = ppProfile.colorGrading.settings;
                colorgrading2.channelMixer.red = new Vector3(2, 0, 0);
                //change the intensity in the temporary settings variable
                while (colorgrading2.tonemapping.neutralBlackOut < 0) {
                    colorgrading2.tonemapping.neutralBlackOut = colorgrading2.tonemapping.neutralBlackOut + 0.005f;
                    yield return new WaitForSeconds(0.05f);
                }

                //set the bloom settings in the actual profile to the temp settings with the changed value
                ppProfile.colorGrading.settings = colorgrading2;
                DayState = 0;
                break;
        }
        StopCoroutine("Change_Day_State");
    }

    //Environment states
    public void Change_Environment() {
        switch(EnvState) {
            case (Environment_State.Unhealthy):
                //DO UNHEALTHY STUFF
                break;
            case (Environment_State.Neutral):
                //DO NEUTRAL STUFF
                break;
            case (Environment_State.Healthy):
                //DO HEALTHY STUFF
                break;
        }
    }
}

public enum Environment_State {
    Unhealthy,
    Neutral,
    Healthy
}

public enum Day_State {
    Morning,
    Noon,
    Evening
}