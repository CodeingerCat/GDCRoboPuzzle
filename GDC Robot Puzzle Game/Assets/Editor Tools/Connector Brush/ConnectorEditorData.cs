using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class ConnectorEditorData
{
    public static bool showNet; // For turning on and off bool chain conctor gizmos
    public static DataChainObjectBase selectedBoolChainStart; // the selected start of a connection in editor
}
