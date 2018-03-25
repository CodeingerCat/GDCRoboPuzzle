using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughLogic : LogicObjectBase {
    public void OnEnable()
    {
        Setup(); //[Note] Temporary since Setup would ushualy be called by the object brush on placement
    }

    public override bool LogicFunction()
    {
        return values[0];
    }
}
