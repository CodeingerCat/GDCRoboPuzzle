using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismObjectBase : DataChainObjectBase {
    // [[ Variables ]]
    public DataChainObjectBase input; // input Date Chain object

    // [[ Functions ]]
    // toggle the input for a object if space available
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

    // updates state data
    public override bool UpdateData()
    {
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

    // retuns the input
    public override DataChainObjectBase[] GetInputs()
    {
        return new DataChainObjectBase[] { input };
    }

    // called when the state is changed by the bool data chain
    public virtual void onStateChange() { }
}
