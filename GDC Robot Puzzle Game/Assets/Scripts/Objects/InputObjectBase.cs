﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputObjectBase : DataChainObjectBase {
    // [[ Variables ]]
    public DataChainObjectBase[] outputs; // outputs state is sent to
    [HideInInspector] List<DataChainObjectBase> chain = new List<DataChainObjectBase>(); // Data Chain Objects 

    // [[ Functions ]]
    public override void Setup()
    {
        gameObject.layer = 8; // layer 8 = InputObjects
        hasOutputs = true;
    }

    // Called by other object during a "Update" step to siganl it being available to activate.
    public virtual void Selected() { }

    // Runs Data Chain
    public virtual void Trigger(bool set)
    {
        // cleer and then add output objects to chain
        chain.Clear();
        chain.AddRange(outputs);
        
        // update data of objects in chan then if state change add its outputs to the chain
        for(int i = 0; i < chain.Count; i++)
        {
            if (chain[i].UpdateData())
            {
                DataChainObjectBase[] temp = chain[i].GetOutputs();
                if(temp != null)
                    chain.AddRange(temp);
            }  
        }
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

    // returns outputs
    public override DataChainObjectBase[] GetOutputs()
    {
        return outputs;
    }

    // show conections in editor
    public override void DrawOutputs()
    {
        for(int i = 0; i < outputs.Length; i++)
        {
            if (outputs[i])
            {
                Gizmos.color = Color.cyan;
                Vector3 baseVec = outputs[i].transform.position - transform.position;
                Vector3 baseVecTan = new Vector3(-baseVec.y, baseVec.x).normalized * 0.3f;
                Gizmos.DrawRay(transform.position,baseVec);
                Gizmos.DrawRay(transform.position + (baseVec * 0.7f), baseVecTan - (baseVec.normalized * 0.3f));
                Gizmos.DrawRay(transform.position + (baseVec * 0.7f), -baseVecTan - (baseVec.normalized * 0.3f));
            }
        }
    }
}
