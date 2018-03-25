using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismObjectBase : DataChainObjectBase {
    // [[ Variables ]]
    public DataChainObjectBase input; // input Date Chain object

    // [[ Functions ]]
    public override int ToggleInput(GameObject toCheck)
    {
        if(input != null)
        {
            if (input == toCheck.GetComponent<DataChainObjectBase>())
            {
                input = null;
                return 2;
            }
            return 0;
        }
        input = toCheck.GetComponent<DataChainObjectBase>();
        return 1;
    }

    public override bool UpdateData()
    {
        Debug.Log("Check 4");
        if (input)
        {
            bool remState = state;
            state = input.state;
            if (remState != state)
                onStateChange();
        }
        else
        {
            state = false;
        }
        return false;
    }

    // called when the state is changed by the bool data chain
    public virtual void onStateChange() { }
}
