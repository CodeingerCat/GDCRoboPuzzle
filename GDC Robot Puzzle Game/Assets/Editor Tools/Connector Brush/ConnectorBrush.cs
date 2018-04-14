using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomGridBrush(true, false, false, "ConnectorBrush")]
public class ConnectorBrush : GridBrush{
    // [[ Variables ]]
    [HideInInspector] public string myMapSerchName = "Bool Chain"; // name of map scaned for data chain objects
    [HideInInspector] public GameObject myTileMap; // game object found using name by the editor on selection of brush
    public GameObject start; // used as temporary pointers to start and end game objects
    public GameObject temp; // used as imtermidiate while finding start and end


    // [[ Functions ]]

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        //base.Paint(gridLayout, brushTarget, position);
        temp = GetObject(gridLayout, position, myTileMap);
        if (temp)
        {
            if (start)
            {
                if (temp != start)
                {
                    int ret = temp.GetComponent<DataChainObjectBase>().ToggleInput(start);
                    if (ret == 1 || ret == 2)
                    {
                        start.GetComponent<DataChainObjectBase>().EditOutput(temp, ret == 2);
                        EditorUtility.SetDirty(start);
                        EditorUtility.SetDirty(temp);
                    }
                    else
                    {
                        Debug.LogWarning("Failure to add as input: Canceling conection.");
                    }
                }
                ConnectorEditorData.selectedBoolChainStart = null;
                start = null;
            }
            else
            {
                start = temp;
                if (!start.GetComponent<DataChainObjectBase>().hasOutputs)
                {
                    start = null;
                    Debug.LogWarning("You cant start a conection from this object since it dosent output a signal.");
                }
                else
                    ConnectorEditorData.selectedBoolChainStart = start.GetComponent<DataChainObjectBase>();
            }
        }
    }

    // finds Game object in selected layer at a Cell Position
    public virtual GameObject GetObject(GridLayout gridLayout, Vector3Int position, GameObject myMap)
    {
        Transform parent = myMap.transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).GetComponent<DataChainObjectBase>() != null)
            {
                Vector3 p = parent.GetChild(i).position;
                if (gridLayout.WorldToCell(p) == position)
                {
                    return parent.GetChild(i).gameObject;
                }
            }
        }
        return default(GameObject);
    }
}


[CustomEditor(typeof(ConnectorBrush))]
public class ConnectorBrushEditor : GridBrushEditor
{
    private ConnectorBrush myBrush { get { return target as ConnectorBrush; } } // sets brush to the corasponding brush in use

    // turn on and off gizmos
    public void Awake()
    {
        if(myBrush.start != null)
            ConnectorEditorData.selectedBoolChainStart = myBrush.start.GetComponent<DataChainObjectBase>();
        ConnectorEditorData.showNet = true;
    }

    public override void OnInspectorGUI()
    {
        myBrush.myTileMap = TestAMap(myBrush.myMapSerchName);

        if (GUILayout.Button("Toggle Conection Display " + (ConnectorEditorData.showNet ? "off" : "on")))
            ConnectorEditorData.showNet = !ConnectorEditorData.showNet;

        GUILayout.Label("Instuctions");
        GUILayout.Label("[[ Step 1 ]]");
        GUILayout.Label("  Select a Data Chain object to start a conection from.");
        GUILayout.Label("[[ Setp 2]]");
        GUILayout.Label("  Select a second Data Chain Object to");
        GUILayout.Label("    1) make a conection to it.");
        GUILayout.Label("    2) remove a conection to it if one alreay exsists.");
        GUILayout.Label("  Select the same Data Chain Object to Deselect it.");
    }

    // finds if selected tile map exists
    public virtual GameObject TestAMap(string toTest)
    {
        // step 1 -- dose game object by name exsist
        GameObject myMap = GameObject.Find(toTest);
        if (myMap == null)
        {
            Debug.LogError("The selected tilemap " + toTest + " dose not exsist.");
            return null;
        }
        // step 2 -- dose game object contain a Tilemap component
        if (myMap.GetComponents<Tilemap>() == null)
        {
            myMap = null;
            Debug.LogError("The selected tilemap " + toTest + " is not a tilemap.");
            return null;
        }

        //Selection.SetActiveObjectWithContext(GameObject.Find("Grid"), GameObject.Find("Grid"));
        return myMap;
    }

#if UNITY_EDITOR
    // Add toggle for conection display to editor menues
    [MenuItem("Custom Addons/Brushes/Connector Brush/Toggle Conection Display #c")]
    private static void ToggleDisplay()
    {
        ConnectorEditorData.showNet = !ConnectorEditorData.showNet;
        SceneView.RepaintAll();
    }
    #endif
}
