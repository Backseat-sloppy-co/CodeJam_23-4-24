using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class Target
    // this class is used to store the target gameobject and a bool that is used to check if the target is the target that is currently set
{
    public GameObject target;
    public bool isTarget;

    // this constructor is used to set the target gameobject and set the isTarget bool to false
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
    private float firerate = 0f;
    private float time;
    private bool isWin = false;
    private float nextSceneTime = 2f;
    public TMP_Text timeText;
    public SpriteRenderer tryk;

    private void Start()
    {
        // for as many as the count variable, instantiate a prefab at a location from the targetLocation list and add it to the targets list
        for (int i = 0; i < count; i++)
        {
            
            GameObject target = Instantiate(prefab, targetLocation[i]);
           
            target.transform.parent = transform;
            targets.Add(new Target(target));
        }
        // set a random target from the targets list to be the target, and set isTarget to true and play the openUp animation and souunds
        var _target = targets[UnityEngine.Random.Range(0, targets.Count)];
        _target.isTarget = true;
        _target.target.GetComponent<Animator>().SetBool("openUp", true);
       AudioManager.instance.Play("Beep");
        AudioManager.instance.Play("Flyby");
    }
    private void Update()
    {
        // if iswin is true, return out of update
        if (isWin)
        {
            return;
        }
        // time is increased by the time since the last frame
        time += Time.deltaTime;

        // on mouse click, start the shoot coroutine, destroy the "tryk" sprite, and check if the raycast hits an object tagged as "Enemy"
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shoot());
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Mouse is clicked");
            Destroy(tryk);

            // if the raycast hits an object tagged as "Enemy", play the hitmarker sound, and check if it is the target that is currently set
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Enemy"))
                {
                    AudioManager.instance.Play("Hitmarker");
                    Debug.Log("Target is clicked");
                    foreach (var target in targets)
                    {
                        // 
                        if (target.target == hit.collider.gameObject)
                        {
                           Animator anim = target.target.GetComponent<Animator>();
                            Debug.Log("Target is found");
                            // if the target is the target that is currently set, set isTarget to false, remove it from the targets list, play the hit animation, and set a new target
                            if(target.isTarget == true)
                            {
                                target.isTarget = false;
                                targets.Remove(target);
                                Debug.Log("Target is hit");
                                anim.SetBool("isHit", true);

                                var _target = targets[UnityEngine.Random.Range(0, targets.Count)];
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
        // this wincondition is called if all targets are hit
        if (IsAllTargetsHit())
        {
           AudioManager.instance.Play("Beep");
            isWin = true;
            timeText.text = "Tid: " + time;
            Debug.Log("All targets have been hit in " + time + " secounds");
           GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));
        }
    }
    // this method checks if all targets are hit
    public bool IsAllTargetsHit()
    {
        // this method checks if all bools in the targets list are false, as this means that all targets are hit
        foreach (var target in targets)
        {
            if (target.isTarget == true)
            {
                return false;
            }
        }
        return true;
    }
    // this coroutine is called when the player shoots and plays the shooting animation and sound effect
    IEnumerator Shoot()
    {
        //play shooting animation
        weapon.GetComponent<Animator>().SetBool("isShoot", true);
        AudioManager.instance.Play("Shoot");
       yield return new WaitForSeconds(firerate);
        weapon.GetComponent<Animator>().SetBool("isShoot", false);
    }
}
