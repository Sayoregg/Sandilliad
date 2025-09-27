using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CollisionDetection : MonoBehaviour
{


    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sand" && ValueManager.IsSucking)
        {
            //Ray ray = new Ray(transform.position, other.transform.position - transform.position);

            //Debug.DrawRay(ray.origin, ray.direction);
            //if (Physics.Raycast(ray, out RaycastHit hit, 1000, 3))
            //{
            //    Debug.Log(hit.collider.gameObject.layer);

            //    return;
            //}


            if (other.gameObject != null)
            {
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.parent.transform.position, 0.2f);
            }
            if (other.gameObject != null)
            {
                other.transform.localScale = other.transform.localScale * (Mathf.Lerp(1, 0, 0.1f));

            }


            //create vector from parent to sand
            //if vector.Distance is smaller than 2
            //start courutine
            //Vector3 distanceSand = transform.parent.position - other.transform.position;

            //if (distanceSand.magnitude <= 2)
            //{
            //StartCoroutine(SizeChange(other.transform, other.gameObject.transform.localScale, other.gameObject));
            //}
            //if (other.IsDestroyed()) { return; }
            //else
            //{
            //    StartCoroutine(SmoothMovement(other.transform, other.gameObject.transform.position));
            //}


        }

    }
    private void Update()
    {
    }

    //private IEnumerator SizeChange(Transform target, Vector3 startScale, GameObject targetObject)
    //{


    //    float duration = 1 * _shrinkingSpeedMultiplier;
    //    float elapsed = 0f;



    //    while (elapsed < duration)
    //    {
    //        float t = elapsed / duration;


    //        target.localScale = startScale * Mathf.Lerp(1, 0, t);



    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    Destroy(targetObject);


    //    target.localScale = Vector3.zero;
    //    _player.GetComponent<PlayerController>().AddSand(20f);


    //}

    //private IEnumerator SmoothMovement(Transform target, Vector3 startPosition)
    //{



    //    float duration = 1f;
    //    float elapsed = 0f;



    //    while (elapsed < duration)
    //    {
    //        float t = elapsed / duration;

    //        if (target != null)
    //        {
    //            target.transform.position = Vector3.MoveTowards(startPosition, transform.parent.transform.position, t * _movementSpeedMultiplier);
    //        }

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }




    //}



}
