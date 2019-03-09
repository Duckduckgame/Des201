using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MovePlatform)), CanEditMultipleObjects]
public class MovePlatformEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        MovePlatform movePlatform = (MovePlatform)target;

        Vector3 initialPosition = movePlatform.transform.TransformPoint(movePlatform.m_initialPosition);
        Vector3 targetPosition = movePlatform.transform.TransformPoint(movePlatform.localTargetPosition);

        EditorGUI.BeginChangeCheck();
        Handles.Label(targetPosition, "Target", "button");
        targetPosition = Handles.PositionHandle(targetPosition, movePlatform.transform.rotation);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(movePlatform, "Move Handles");
           
            movePlatform.localTargetPosition = movePlatform.transform.InverseTransformPoint(targetPosition);
        }
    }

}


