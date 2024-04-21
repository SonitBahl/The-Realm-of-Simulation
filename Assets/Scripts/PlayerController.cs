using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MonoBehaviour CarController;
    public Transform vehicle;
    public Transform Player;

    [Header("Cameras")]
    public Camera PlayerCam;
    public Camera CarCam;

    bool CanDrive;

    [SerializeField]
    float maxDistance = 5f;

    void Start()
    {
        CarController.enabled = false;
        SetPlayerCameraActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CanDrive && Vector3.Distance(Player.position, vehicle.position) <= maxDistance)
        {
            EnterVehicle();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ExitVehicle();
        }
    }

    void EnterVehicle()
    {
        CarController.enabled = true;
        Player.transform.SetParent(vehicle);
        Player.gameObject.SetActive(false);
        SetPlayerCameraActive(false);
        SetCarCameraActive(true);
    }

    void ExitVehicle()
    {
        CarController.enabled = false;
        Player.transform.SetParent(null);
        Player.gameObject.SetActive(true);
        SetPlayerCameraActive(true);
        SetCarCameraActive(false);
    }

    void SetPlayerCameraActive(bool active)
    {
        PlayerCam.enabled = active;
    }

    void SetCarCameraActive(bool active)
    {
        CarCam.enabled = active;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("vehicle"))
        {
            CanDrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("vehicle"))
        {
            CanDrive = false;
        }
    }
}
