using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour {
    public Day_State DayState;
    public Environment_State EnvState;
    //Day States
    public void Change_Day() {
        switch (DayState) {
            case (Day_State.Morning):
                //DO UNHEALTHY STUFF
                break;
            case (Day_State.Noon):
                //DO NEUTRAL STUFF
                break;
            case (Day_State.Evening):
                //DO HEALTHY STUFF
                break;
        }
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