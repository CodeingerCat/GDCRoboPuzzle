using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomGridBrush(false, true, false, "InputObjectBrush")]
public class DataChainObjectBrush : ObjectBrushBase<DataChainObjectBase>
{
    public override void CreateObjectModifier(GameObject obj, GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        obj.GetComponent<DataChainObjectBase>().Setup();
        base.CreateObjectModifier(obj, gridLayout, brushTarget, position);
    }

    public override void DestoryObject(GameObject obj, GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        DataChainObjectBase dObj = obj.GetComponent<DataChainObjectBase>();
        DataChainObjectBase[] inputs = dObj.GetInputs();
        DataChainObjectBase[] outputs = dObj.GetOutputs();
        // remove input conections
        if (inputs != null)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if(inputs[i] != null)
                    inputs[i].EditOutput(obj, true);
            }
        }
        // remove output connections
        if (outputs != null)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].ToggleInput(obj);
            }
        }
        base.DestoryObject(obj, gridLayout, brushTarget, position);
    }

#if UNITY_EDITOR
    // Add menue button for Adding brush in the project
    [MenuItem("Custom Addons/Brushes/Data Chain Object/New")]
    private static void AddBrush()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save DataChainBrush", "NewDataChainBrush", "asset", "Save DataChainBrush", "Assets");
        if (path == "")
        { return; }
        AssetDatabase.CreateAsset(CreateInstance<DataChainObjectBrush>(), path);
    }
#endif
}

[CustomEditor(typeof(DataChainObjectBrush))]
public class DataChainObjectBrushEditor : ObjectBrushBaseEditor<DataChainObjectBase>
{

}
