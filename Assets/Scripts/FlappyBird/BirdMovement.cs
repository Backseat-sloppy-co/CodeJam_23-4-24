using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravity = 1f;

    public TMP_Text text;

    private void Update()
    {
        Vector3 dir = Vector3.zero;

        // we assume that device is held parallel to the ground
        // and Home button is in the right hand

        // remap device acceleration axis to game coordinates:
        //  1) XY plane of the device is mapped onto XZ plane
        //  2) rotated 90 degrees around Y axis
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        // clamp acceleration vector to unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * jumpForce);
        text.text = "X: " + dir.x + " Y: " + dir.y + " Z: " + dir.z;
    }

    private void Jump()
    {
        //bird.position += Vector3.up * jumpForce;
    }
}
