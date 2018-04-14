using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectBrushBase<T> : GridBrushBase
{
    // Variable setup //
    public string myTileMap; // name of effected tile map
    [HideInInspector]
    public GameObject myMap; // GameObject asociated with name (set by editor)

    public T[] toUse; // Array of GameObjects that can be panted with
    [HideInInspector]
    public int toUseSlot; // curently selected GameObjects slot

    public const string menueLocation = ("DBlast Tools/Tilemap/Add Brush/Brush1");// location of menu item to create instance of brush

    // Functions //
    // (paint override) Handles placement of GameObjects
    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        GameObject obj = GetObject(gridLayout, position);

        if (!(obj is GameObject))
        {
            if (toUse.Length != 0)
            {
                GameObject ob = Instantiate((toUse[toUseSlot] as Component).gameObject, gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position)) + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity);
                CreateObjectModifier(ob, gridLayout, brushTarget, position);
            }
        }
    }
    // called for placing a gameobject at a positon
    public virtual void CreateObjectModifier(GameObject obj, GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        obj.transform.SetParent(myMap.transform, true);
    }

    // (erase override) Handles removal of GameObjects
    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        GameObject obj = GetObject(gridLayout, position);

        if (obj is GameObject)
        {
            DestoryObject(obj, gridLayout, brushTarget, position);
        }
    }
    // called for destorying a gameobject at a positon
    public virtual void DestoryObject(GameObject obj, GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        DestroyImmediate(obj);
    }

    // finds Game object in selected layer at a Cell Position
    public virtual GameObject GetObject(GridLayout gridLayout, Vector3Int position)
    {
        Transform parent = myMap.transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).GetComponent<T>() != null)
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
