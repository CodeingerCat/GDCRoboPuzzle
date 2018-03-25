using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMechanism : MechanismObjectBase {
    public Light myLight;

    public override void onStateChange()
    {
        if (myLight)
            myLight.enabled = state;
    }
}
