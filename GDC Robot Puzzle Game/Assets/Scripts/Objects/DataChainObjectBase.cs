using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChainObjectBase : MonoBehaviour {
    // [[ Variables ]]
    [HideInInspector] public bool state;
    [HideInInspector] public bool hasOutputs;

    // [[ Functions ]]
    // for updateing input output data (retuns true if state change)
    public virtual bool UpdateData() { return false; }

    // atempts to add an input or removes exsisting
    public virtual int ToggleInput(GameObject toCheck) { return 0; } // 0 failed, 1 added, 2 removed

    // adds/remove a object from outputs
    public virtual void EditOutput(GameObject to, bool remove) { }

    // returns any outputs object gose to
    public virtual DataChainObjectBase[] GetOutputs() { return null; }

    // Dose any needed aditonal setup on placement in editor
    public virtual void Setup() { }
}
