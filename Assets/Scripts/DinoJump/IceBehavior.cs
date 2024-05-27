using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBehavior : MonoBehaviour
{
  [SerializeField]  private float iceSpeed;
    void Update()
    {

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + -iceSpeed * Time.deltaTime, 0);

    }
}
// this code was written with the help of copilot.