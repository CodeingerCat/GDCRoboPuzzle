using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : InputObjectBase {
    public override void Selected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = !state;
            Trigger(state);
        }
    }
}
