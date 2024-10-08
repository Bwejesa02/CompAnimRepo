using UnityEngine;

public class PathController : MonoBehaviour
{
    public PathManager pathManager; // Assign PathManager in Inspector
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    private int currentWaypointIndex = 0;
    public Animator animator;

    private bool isIdle = false; // Used to control if the character is idle

    void Start()
    {
        animator.SetBool("isWalking", false);
    }

    void Update()
    {
        // Check if we are idle, if yes, don't move
        if (isIdle)
        {
            return;
        }

        // If there are no waypoints, don't proceed
        if (pathManager.waypoints.Count == 0) return;

        // Get current target waypoint position
        Vector3 targetPosition = pathManager.waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Rotate towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

        // Move towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if we've reached the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % pathManager.waypoints.Count;
        }

        // Check if character is walking
        bool isWalking = Vector3.Distance(transform.position, targetPosition) > 0.1f;
        animator.SetBool("isWalking", isWalking);
    }

    // OnCollisionEnter to detect when character collides with an object
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has a specific tag or condition
        // For this example, let's say we stop if we hit any object with the tag "Obstacle"
        if (collision.gameObject.tag == "Obstacle")
        {
            // Stop moving and set isIdle to true
            isIdle = true;
            animator.SetBool("isWalking", false); // Switch to idle animation
        }
    }
}
