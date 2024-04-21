using System.Collections.Generic;
using UnityEngine;

public class WaypointContainer : MonoBehaviour
{
    public List<Transform> waypoints;

    void Awake()
    {
        // Clear existing waypoints list to prevent duplicates
        waypoints.Clear();

        // Get all child transforms and add them to the waypoints list
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }

        // Remove the container's transform itself if it's in the waypoints list
        if (waypoints.Contains(transform))
        {
            waypoints.Remove(transform);
        }
    }
}
