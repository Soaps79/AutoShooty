using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RewardsManager))]
public class RewardsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var manager = (RewardsManager)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Level Up"))
        {
            manager.ForceLevelUp();
        }
    }
}
