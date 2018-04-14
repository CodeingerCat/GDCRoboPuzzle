using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataChainObjectBase : MonoBehaviour {
    // [[ Variables ]]
    [HideInInspector] public bool state; // curent bool state of the object in the chain
    [HideInInspector] public bool hasOutputs; // if the object has outputs

    // [[ Functions ]]
    // for updateing input output data (retuns true if state change)
    public virtual bool UpdateData() { return false; }

    // atempts to add an input or removes exsisting
    public virtual int ToggleInput(GameObject toCheck) { return 0; } // 0 failed, 1 added, 2 removed

    // adds/remove a object from outputs
    public virtual void EditOutput(GameObject to, bool remove) { }

    // returns any outputs object gose to
    public virtual DataChainObjectBase[] GetOutputs() { return null; }

    // returns any inputs that go into the object
    public virtual DataChainObjectBase[] GetInputs() { return null; }

    // Dose any needed aditonal setup on placement in editor
    public virtual void Setup() { }

    // Gizmos for conecting bool chain objects
    public void OnDrawGizmos()
    {
        if (ConnectorEditorData.showNet)
        {
            if (this == ConnectorEditorData.selectedBoolChainStart)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(transform.position, new Vector3(1, 1));
                Gizmos.DrawWireCube(transform.position, new Vector3(0.7f, 0.7f));
            }
            DrawOutputs();
        }
    }
    // for drawing output conections
    public virtual void DrawOutputs() { }
}
