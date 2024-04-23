using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCards : MonoBehaviour
{
    List<GameObject> cards = new List<GameObject>();
    [SerializeField] private int faceIndex;

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignFace(int index)
    {
        faceIndex = index;
    }
}
