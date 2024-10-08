using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector]
    public List<Waypoint> waypoints = new List<Waypoint>();

    public void AddWaypoint(Vector3 newPosition)
    {
        Waypoint newWaypoint = new Waypoint { position = newPosition };
        waypoints.Add(newWaypoint);
    }
}
