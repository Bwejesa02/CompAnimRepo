using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : Editor
{
    PathManager pathManager;

    void OnEnable()
    {
        pathManager = (PathManager)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add Point to Path"))
        {
            pathManager.AddWaypoint(Vector3.zero);
        }

        // Draw each waypoint
        for (int i = 0; i < pathManager.waypoints.Count; i++)
        {
            EditorGUILayout.Vector3Field("Waypoint " + i, pathManager.waypoints[i].position);
        }
    }

    void OnSceneGUI()
    {
        for (int i = 0; i < pathManager.waypoints.Count; i++)
        {
            // Store the current position of the waypoint
            Vector3 waypointPosition = pathManager.waypoints[i].position;

            // Draw a handle at the waypoint's position and allow it to be moved
            Vector3 newPosition = Handles.PositionHandle(waypointPosition, Quaternion.identity);

            // If the position has changed, update the waypoint's position in the list
            if (waypointPosition != newPosition)
            {
                Undo.RecordObject(pathManager, "Move Waypoint");
                pathManager.waypoints[i].position = newPosition;
            }

            // Draw the sphere to visualize the waypoint
            Handles.SphereHandleCap(0, pathManager.waypoints[i].position, Quaternion.identity, 0.5f, EventType.Repaint);

            // Draw lines between waypoints to visualize the path
            if (i > 0)
            {
                Handles.DrawLine(pathManager.waypoints[i - 1].position, pathManager.waypoints[i].position);
            }
        }
    }

}
