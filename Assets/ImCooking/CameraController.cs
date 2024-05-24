using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The following script has been made with assistance from GitHub Copilot
public class CameraController : MonoBehaviour
{
    public float speed = 10.0f; // The speed at which the camera moves
    public float minX = -10.0f; // The minimum x-coordinate the camera can move to
    public float maxX = 10.0f; // The maximum x-coordinate the camera can move to
    public float rotationThreshold = -5.0f; // The x-coordinate at which the camera rotation changes
    public Vector3 rotatedPosition = new Vector3(0, 45, 0); // The rotated position of the camera
    public Vector3 originalPosition = new Vector3(0, 0, 0); // The original position of the camera
    public float rotationSpeed = 1.0f; // The speed at which the camera rotates

    private bool isMovingLeft = false; // Whether the left button is pressed
    private bool isMovingRight = false; // Whether the right button is pressed

    private void Update()
    {
        if (isMovingLeft)
        {
            MoveLeft();
        }
        else if (isMovingRight)
        {
            MoveRight();
        }
    }

    public void StartMovingRight()
    {
        isMovingRight = true;
    }

    public void StopMovingRight()
    {
        isMovingRight = false;
    }

    public void StartMovingLeft()
    {
        isMovingLeft = true;
    }

    public void StopMovingLeft()
    {
        isMovingLeft = false;
    }

    private void MoveRight()
    {
        Vector3 newPosition = transform.position;
        newPosition.x -= speed * Time.deltaTime;
        newPosition.x = Mathf.Max(newPosition.x, minX);
        transform.position = newPosition;

        // Smoothly rotate the camera when it reaches a certain x-coordinate
        if (transform.position.x <= rotationThreshold)
        {
            Quaternion targetRotation = Quaternion.Euler(rotatedPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void MoveLeft()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += speed * Time.deltaTime;
        newPosition.x = Mathf.Min(newPosition.x, maxX);
        transform.position = newPosition;

        // Smoothly rotate the camera back to its original rotation
        if (transform.position.x > rotationThreshold)
        {
            Quaternion originalRotation = Quaternion.Euler(originalPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
        }
    }


    //public void MoveRight()
    //{
    //    Vector3 newPosition = transform.position;
    //    newPosition.x -= speed * Time.deltaTime;
    //    newPosition.x = Mathf.Max(newPosition.x, minX);
    //    transform.position = newPosition;

    //    if (transform.position.x < 45) // Replace -5 with the x-coordinate at which you want to change the rotation
    //    {
    //        Quaternion originalRotation = Quaternion.Euler(0, 180, 0); // Replace (0, 0, 0) with the original rotation
    //        float rotationSpeed = 5.0f; // The speed at which the camera rotates
    //        transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
    //    }

    //}

    //public void MoveLeft()
    //{
    //    Vector3 newPosition = transform.position;
    //    newPosition.x += speed * Time.deltaTime;
    //    newPosition.x = Mathf.Min(newPosition.x, maxX);
    //    transform.position = newPosition;

    //    if (transform.position.x > 45) // Replace -5 with the desired x-coordinate
    //    {
    //        Quaternion targetRotation = Quaternion.Euler(0, 100, 0); // Replace (0, 45, 0) with the desired rotation
    //        float rotationSpeed = 5.0f; // The speed at which the camera rotates
    //        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //    }

    //}
}