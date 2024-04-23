using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

class Target
{
    public GameObject target;
    public bool isTarget;
    public Target(GameObject target)
    {
        this.target = target;
        isTarget = false;
    }
}
public class TargetBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    public GameObject prefab;
    private List<Target> targets = new List<Target>();
    public List<Transform> targetLocation = new List<Transform>();
    public int count = 10;
  

    private void Start()
    {
        
        for (int i = 0; i < count; i++)
        {
            
            GameObject target = Instantiate(prefab, targetLocation[i]);
           
            target.transform.parent = transform;
            targets.Add(new Target(target));
         

        }
        targets[Random.Range(0, targets.Count)].isTarget = true;
      
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Mouse is clicked");
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Target"))
                {
                    Debug.Log("Target is clicked");
                    foreach (var target in targets)
                    {
                        if (target.target == hit.collider.gameObject)
                        {
                           Animator anim = target.target.GetComponent<Animator>();
                            Debug.Log("Target is found");
                            if(target.isTarget == true)
                            {
                                target.isTarget = false;
                                targets.Remove(target);
                                Debug.Log("Target is hit");

                                
                               anim.SetBool("isHit", true);

                                var random = Random.Range(0, targets.Count);
                                targets[random].isTarget = true;
                                //call animation
                               Debug.Log("New target is set");
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
