﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicObjectBase : DataChainObjectBase {
    // [[ Variables ]]
    public DataChainObjectBase[] inputs, outputs; // objects connected as inputs and outputs
    [HideInInspector] public bool[] values; // memory of input objects states

    // [[ Functions ]]
    public override void Setup()
    {
        values = new bool[inputs.Length];
        hasOutputs = true;
    }

    public override bool UpdateData()
    {
        UpdateInputData();
        bool remState = state;
        state = LogicFunction();
        return remState != state;
    }

    // grap states of inputs
    public virtual void UpdateInputData()
    {
        for(int i = 0; i < inputs.Length; i++)
        {
            values[i] = inputs[i].state;
        }
    }

    // decides baded on inputs the logic objects curent state
    public virtual bool LogicFunction()
    {
        return false;
    }

    // remove output or add output to outputs
    public override void EditOutput(GameObject to, bool remove)
    {
        DataChainObjectBase[] temp;
        if (remove)
        {
            int min = 0;
            temp = new DataChainObjectBase[outputs.Length - 1];
            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i].gameObject == to)
                {
                    min = 1;
                }
                else
                {
                    temp[i - min] = outputs[i];
                }
            }
        }
        else
        {
            temp = new DataChainObjectBase[outputs.Length + 1];
            for (int i = 0; i < outputs.Length; i++)
            {
                temp[i] = outputs[i];
            }
            temp[temp.Length - 1] = to.GetComponent<DataChainObjectBase>();
        }
        outputs = temp;
    }

    // prosesing for adding and removing inputs
    public override int ToggleInput(GameObject toCheck)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (inputs[i] == null)
            {
                inputs[i] = toCheck.GetComponent<DataChainObjectBase>();
                return 1; // added sucsefuly
            }
            else if (inputs[i].gameObject == toCheck)
            {
                inputs[i] = null;
                return 2; // conection already exsisted and was removed
            }
        }
        return 0; // no slots
    }

    // returns outputs
    public override DataChainObjectBase[] GetOutputs()
    {
        return outputs;
    }

    //returns inputs
    public override DataChainObjectBase[] GetInputs()
    {
        return inputs;
    }

    // show conections in editor
    public override void DrawOutputs()
    {
        for (int i = 0; i < outputs.Length; i++)
        {
            if (outputs[i])
            {
                Gizmos.color = Color.cyan;
                Vector3 baseVec = outputs[i].transform.position - transform.position;
                Vector3 baseVecTan = new Vector3(-baseVec.y, baseVec.x).normalized * 0.3f;
                Gizmos.DrawRay(transform.position, baseVec);
                Gizmos.DrawRay(transform.position + (baseVec * 0.7f), baseVecTan - (baseVec.normalized * 0.3f));
                Gizmos.DrawRay(transform.position + (baseVec * 0.7f), -baseVecTan - (baseVec.normalized * 0.3f));
            }
        }
    }
}
