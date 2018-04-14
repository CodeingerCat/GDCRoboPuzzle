using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(ObjectBrushBase<>))]
public class ObjectBrushBaseEditor<T> : GridBrushEditor
{
    // Variables // 
    private ObjectBrushBase<T> nlBrush { get { return target as ObjectBrushBase<T>; } } // sets brush to the corasponding brush in use
    private bool safeStart = true; // safestart (used to makes shure a grid is selected for edeting)

    private Vector2 scrollv2 = new Vector2(0, 0); // vector2 value for ObjectScrollMenue

    // Functions//
    // called on rendering of UGI in editor for the brush
    public override void OnPaintInspectorGUI()
    {
        // safe grid selection selector
        if (Selection.activeTransform == null || safeStart)
        {
            Selection.activeTransform = (GameObject.Find("Grid").transform);
            safeStart = false;
        }

        //nlBrush = brush as ObjectBrushBase<T>;

        Title();
        ObjectScrollMenue();
        GUILower();

        // tests tilemap layer name for object brush
        nlBrush.myMap = TestAMap(nlBrush.myTileMap);
    }
    // Brush title
    public virtual void Title()
    {
        GUILayout.Label("[[ " + typeof(T).Name + " ]]");
        GUILayout.Label("This brush paints " + typeof(T).Name + " Game objects");
    }

    // Horizontal selection scroll menue
    public virtual void ObjectScrollMenue()
    {
        GUILayout.Label("{ " + typeof(T).Name + " objects }");
        scrollv2 = GUILayout.BeginScrollView(scrollv2, GUILayout.Height(75f));
        GUILayout.BeginHorizontal();
        for (int i = 0; i < nlBrush.toUse.Length; i++)
        {
            Texture tempTexture = AssetPreview.GetAssetPreview((nlBrush.toUse[i] as Component).gameObject);
            if (nlBrush.toUseSlot != i)
            {
                bool test = GUILayout.Button(tempTexture, GUILayout.Width(tempTexture.width / 2.5f), GUILayout.Height(tempTexture.height / 2.5f));
                if (test)
                    nlBrush.toUseSlot = i;
            }
            else
            {
                GUILayout.Box(tempTexture, GUILayout.Width(tempTexture.width / 2.5f), GUILayout.Height(tempTexture.height / 2.5f));
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }

    // UGI stuff to apper under the ObjectScrollMenu
    public virtual void GUILower()
    {

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

    public override void OnPaintSceneGUI(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing)
    {
        //base.OnPaintSceneGUI(gridLayout, nlBrush.myMap, position, tool, false);
    }

    //public override void
}