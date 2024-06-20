using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class MovementController : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private GameObject snakeHead;
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private float canMove = 0;
    private float targetAngle;
    private Vector3 movementDirection;
    private bool isJoystick;
    private bool started;
    private Rigidbody rb;

    public static MovementController instance;
    private void Awake()
    {
        instance = this;
        rb = snakeHead.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        started = false;
        movementDirection = Vector3.zero;
        EnableJoystickInput();
    }
    private void Update()
    {
        if (isJoystick)
        {
            if (canMove > 0)
            {
                canMove -= Time.deltaTime;
            }
            else canMove = 0;
            if (joystick.Direction.sqrMagnitude > 0.01f && canMove == 0)
            {
                movementDirection = new Vector3(0,0,1);
                targetAngle = Mathf.Atan2(joystick.Direction.x, joystick.Direction.y) * Mathf.Rad2Deg;
                //Debug.Log(joystick.Direction.x + ", " + joystick.Direction.y);
            }
            rb.transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
            rb.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetAngle, 0), 180f);
        }
    }
    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }
    public void Collide()
    {
        targetAngle += 180;
        Debug.Log(targetAngle);
        canMove = 0.5f;
    }
}
