using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target; // Drag your Earth object here

    [Header("Orbit Settings")]
    [SerializeField] private float distance = 10f;
    [SerializeField] private float dragSpeed = 0.4f;
    [SerializeField] private float idleSpinSpeed = 10f; // Minimum continuous spin speed
    [SerializeField] private float momentumDamping = 5f;  // How fast the momentum slows down

    [Header("Angle Limits")]
    [SerializeField] private float minVerticalAngle = -50f;
    [SerializeField] private float maxVerticalAngle = 50f;

    [Header("Zoom Settings (FOV)")]
    [SerializeField] private float zoomSpeed = 15f;
    [SerializeField] private float minFOV = 30f;
    [SerializeField] private float maxFOV = 160f;

    private Camera cam;
    private float currentX = 0f;
    private float currentY = 0f;
    private bool isDragging = false;
    private Vector3 mouseReference;

    private float velocityX = 0f;
    private float velocityY = 0f;

    private void Start()
    {
        cam = GetComponent<Camera>();

        // Calculate initial camera angles relative to target
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;

        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);
        }
    }

    private void Update()
    {
        // Handle Mouse Click and Drag
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            mouseReference = Input.mousePosition;
            velocityX = 0f;
            velocityY = 0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - mouseReference;
            mouseReference = Input.mousePosition;

            float deltaX = mouseDelta.x * dragSpeed;
            float deltaY = -mouseDelta.y * dragSpeed;

            currentX += deltaX;
            currentY += deltaY;

            // Clamp vertical rotation (pitch)
            currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);

            // Calculate flick velocity for momentum upon release
            if (Time.deltaTime > 0f)
            {
                velocityX = deltaX / Time.deltaTime;
                velocityY = deltaY / Time.deltaTime;
            }
        }
        else
        {
            // Apply momentum + minimum idle spin speed, then decay momentum towards 0
            currentX += (velocityX + idleSpinSpeed) * Time.deltaTime;
            currentY += velocityY * Time.deltaTime;
            currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);

            velocityX = Mathf.Lerp(velocityX, 0f, momentumDamping * Time.deltaTime);
            velocityY = Mathf.Lerp(velocityY, 0f, momentumDamping * Time.deltaTime);
        }

        // Handle Scroll Wheel Zoom via Field of View
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0f && cam != null)
        {
            cam.fieldOfView -= scrollData * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate rotation and position around the target
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        transform.rotation = rotation;
        transform.position = position;
    }
}