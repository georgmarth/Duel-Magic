﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement Movement;
    public Camera PlayerCamera;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string HorizontalRightAxis = "Right Horizontal";
    private const string HorizontalLeftAxis = "Right Vertical";

    private void Update()
    {
        Movement.MovementInput.Horizontal = Input.GetAxis(HorizontalAxis);
        Movement.MovementInput.Vertical = Input.GetAxis(VerticalAxis);


    }
}
