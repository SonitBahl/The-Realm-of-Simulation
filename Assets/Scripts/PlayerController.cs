using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MonoBehaviour CarController;
    public CharacterController PlayerControllerScript; 
    public Transform Car;
    public Transform Player;

    [Header("Cameras")]
    public GameObject PlayerCam;
    public GameObject CarCam;

    bool CanDrive = true; 

    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CanDrive)
        {
            EnterVehicle();
            CanDrive = false; 
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ExitVehicle();
        }
    }

    void EnterVehicle()
    {
        CarController.enabled = true;

        // Parent the player to the car
        Player.SetParent(Car);
        Player.gameObject.SetActive(false);

        // Enable car camera and disable player camera
        PlayerCam.SetActive(false);
        CarCam.SetActive(true);

        PlayerControllerScript.enabled = false; 
    }

    void ExitVehicle()
{
    CarController.enabled = false;

    // Calculate the right exit position
    Vector3 exitOffset = Car.right * 2.7f; 
    Vector3 exitPosition = Car.position + exitOffset;

    // Cast a ray from the exit position downward to find the ground
    RaycastHit hit;
    if (Physics.Raycast(exitPosition, Vector3.down, out hit))
    {
        // Ensure the player is above the ground
        exitPosition.y = hit.point.y + 0.1f; 
    }
    else
    {
        // If no ground is found, just use the car's position
        exitPosition = Car.position + Vector3.up * 0.1f + exitOffset;
    }

    // Unparent the player from the car and position to the right
    Player.SetParent(null);
    Player.position = exitPosition;
    Player.gameObject.SetActive(true);

    // Enable player camera and disable car camera
    PlayerCam.SetActive(true);
    CarCam.SetActive(false);

    PlayerControllerScript.enabled = true; 
}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CanDrive = true;
        }
    }
}
