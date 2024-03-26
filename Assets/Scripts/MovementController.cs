using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private GameObject snakeHead;
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 2f;

    private bool isJoystick;
    private void Start()
    {
        EnableJoystickInput();
    }
    private void Update()
    {
        if (isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x , 0.0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * movementSpeed);

            if (movementDirection.magnitude <= 0f)
            {
                return;
            }
            else
            {
                var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);
                controller.transform.rotation = Quaternion.LookRotation(targetDirection);
            }
        }
    }
    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }
}
