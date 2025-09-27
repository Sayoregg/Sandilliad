using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    [SerializeField]
    private int _movementSpeedMultiplier;
    
    [SerializeField]
    private int _shrinkingSpeedMultiplier;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sand" && ValueManager.IsSucking)
        {
            //create vector from parent to sand
            //if vector.Distance is smaller than 2
            //start courutine
            Vector3 distanceSand = transform.parent.position - other.transform.position;
            if (distanceSand.magnitude <= 1.2f)
            {
                StartCoroutine(SizeChange(other.transform, other.gameObject.transform.localScale, other.gameObject));
            }
            StartCoroutine(SmoothMovement(other.transform, other.gameObject.transform.position));
        }

    }

    private IEnumerator SizeChange(Transform target, Vector3 startScale, GameObject targetObject)
    {
        //smooth animation to throw the zurks away


        float duration = 1*_shrinkingSpeedMultiplier;
        float elapsed = 0f;



        while (elapsed < duration)
        {
            float t = elapsed / duration;


            target.localScale = startScale * Mathf.Lerp(1, 0, t);



            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = Vector3.zero;

        _player.GetComponent<PlayerController>().AddSand(20f);
        Destroy(targetObject);
    }

    private IEnumerator SmoothMovement(Transform target, Vector3 startPosition)
    {
        //smooth animation to throw the zurks away


        float duration = 2f;
        float elapsed = 0f;



        while (elapsed < duration)
        {
            float t = elapsed / duration;

            target.transform.position = Vector3.MoveTowards(startPosition, transform.parent.transform.position, t * _movementSpeedMultiplier);


            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = Vector3.zero;

    }
}
