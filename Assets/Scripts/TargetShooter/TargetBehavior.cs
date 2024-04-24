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

    public GameObject prefab;
    private List<Target> targets = new List<Target>();
    public List<Transform> targetLocation = new List<Transform>();
    public int count = 10;
    public GameObject weapon;
    private float firerate = 0.05f;
    
  

    private void Start()
    {
        
        for (int i = 0; i < count; i++)
        {
            
            GameObject target = Instantiate(prefab, targetLocation[i]);
           
            target.transform.parent = transform;
            targets.Add(new Target(target));
         

        }
        var _target = targets[Random.Range(0, targets.Count)];
        _target.isTarget = true;
        _target.target.GetComponent<Animator>().SetBool("openUp", true);
      
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shoot());
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Mouse is clicked");

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Target"))
                {
                    FindObjectOfType<AudioManager>().Play("Hitmarker");
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

                                var _target = targets[Random.Range(0, targets.Count)];
                                _target.isTarget = true;
                                _target.target.GetComponent<Animator>().SetBool("openUp", true);
                                Debug.Log("New target is set");
                            }
                            break;
                        }
                    }
                }
            }
        }
        if (IsAllTargetsHit())
        {
            Debug.Log("All targets are down");
            //play sound
            //maybe display win message
            //change to next level after coroutine
        }
    }
    // this method checks if all targets are hit
    public bool IsAllTargetsHit()
    {
        foreach (var target in targets)
        {
            if (target.isTarget == true)
            {
                return false;
            }
        }
        return true;
    }
    // enumerator courantine that waits for a few seconds
    IEnumerator Shoot()
    {
        //play shooting animation
        weapon.GetComponent<Animator>().SetBool("isShoot", true);
        FindObjectOfType<AudioManager>().Play("Shoot");
        yield return new WaitForSeconds(firerate);
        weapon.GetComponent<Animator>().SetBool("isShoot", false);
    }
}
