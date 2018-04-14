using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughLogic : LogicObjectBase {

    public override bool LogicFunction()
    {
        return values[0];
    }
}
