using UnityEngine;

public class AICarController : MonoBehaviour
{
    public WaypointContainer waypointContainer;
    public float speed = 5f;
    public float rotationSpeed = 2f;

    private int currentWaypointIndex = 0;
    private int waypointsVisited = 0;

    void Start()
    {
        if (waypointContainer == null)
        {
            Debug.LogError("WaypointContainer not assigned!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        FollowWaypoints();
    }

    void FollowWaypoints()
    {
        if (waypointContainer.waypoints.Count == 0)
        {
            Debug.LogWarning("No waypoints defined!");
            return;
        }

        Vector3 targetWaypoint = waypointContainer.waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetWaypoint - transform.position).normalized;

        // Rotate towards the target waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move towards the target waypoint
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the car has reached the waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint);
        if (distanceToWaypoint < 1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypointContainer.waypoints.Count;
            waypointsVisited++;
        }
    }
}
