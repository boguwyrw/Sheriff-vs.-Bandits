using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField]
    private Joystick horizontalJoystick = null;

    private float horizontalMove = 0.0f;
    private float playerSpeed = 0.0f;
    private float speed = 12.0f;

    private void Start()
    {
        playerSpeed = speed;
    }

    private void FixedUpdate()
    {
        horizontalMove = horizontalJoystick.Horizontal * playerSpeed * Time.deltaTime;
        transform.parent.Translate(horizontalMove, 0, 0);
    }
}
