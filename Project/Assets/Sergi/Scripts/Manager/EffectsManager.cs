using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using DG.Tweening;
public class EffectsManager : MonoBehaviour {
    public PostProcessingProfile ppProfile;
    public int DayState = 0;
    public Environment_State EnvState;
    public AudioSource[] EnvAudio;
    public SpriteRenderer EnvironmentImage;
    public Sprite[] Environment;


	private void Start() {
        PostProcessingSettings();
        EventManager.DayCycle += SDT;
        EventManager.NightCycle += StartNight;
        EnvAudio[1].DOFade(0, 0.01f);
        EnvAudio[2].DOFade(0, 0.01f);
        EnvAudio[0].DOFade(1, 1);
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
        float time = 0;
        switch (DayState) {
            case (0):
                //copy current bloom settings from the profile into a temporary variable
                ColorGradingModel.Settings colorgrading = ppProfile.colorGrading.settings;
                colorgrading.channelMixer.red = new Vector3(2, 0, 0);
                Debug.Log("GOING THROUGH");
                //change the intensity in the temporary settings variable
                while (time < 2) {
                    colorgrading.tonemapping.neutralBlackOut = colorgrading.tonemapping.neutralBlackOut + 0.05f;
                    time = time + 0.5f;
                    yield return new WaitForSeconds(0.5f);
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
    void StartNight() {
        EventManager.InterMission_Disable();
        StartCoroutine("Night_State");
    }
    public IEnumerator Night_State() {
        float time = 0;
        float light = 2;
        yield return new WaitForSeconds(10f);
        while (time < 5) {
            //copy current bloom settings from the profile into a temporary variable
            ColorGradingModel.Settings colorgrading = ppProfile.colorGrading.settings;
            light = light - 0.025f;
            colorgrading.channelMixer.red = new Vector3(light, 0, 0);
            colorgrading.tonemapping.neutralBlackOut = (0 - (light / 100));
            //change the intensity in the temporary settings variable
            //set the bloom settings in the actual profile to the temp settings with the changed value
            ppProfile.colorGrading.settings = colorgrading;
            time = time + 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("Change_Day_State");
        DayState = 0;
        Change_Environment();
        EventManager.InterMission_Disable();
        EventManager.InterMission_Continue();
    }


    //Environment states
    public void Change_Environment() {
        if(EventManager.Get_Environment() < 30) {
            EnvironmentImage.sprite = Environment[2];
            EnvAudio[1].DOFade(0, 0.5f);
            EnvAudio[0].DOFade(0, 0.5f);
            EnvAudio[2].DOFade(1, 1);
        }
        if (EventManager.Get_Environment() > 60) {
            EnvAudio[1].DOFade(0, 0.5f);
            EnvAudio[2].DOFade(0, 0.5f);
            EnvAudio[0].DOFade(1, 1);
            EnvironmentImage.sprite = Environment[0];
        }
            if (EventManager.Get_Environment() >= 30 && EventManager.Get_Environment() <= 60) {
            EnvAudio[0].DOFade(0, 0.5f);
            EnvAudio[2].DOFade(0, 0.5f);
            EnvAudio[1].DOFade(1, 1);
            EnvironmentImage.sprite = Environment[1];
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