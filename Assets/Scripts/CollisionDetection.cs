using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (_shouldGetSmaller)
        //{
        //    _scaleSand = Vector3.Lerp(_scaleSand, new Vector3(0, 0, 0), 0.1f);

        //}
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sand")
        {
   

            StartCoroutine(SuckSand(other.transform, other.gameObject.transform.localScale,other.gameObject));

            StartCoroutine(SmoothMovement(other.transform, other.gameObject.transform.position));

      

        }
    

    }
    private IEnumerator SuckSand(Transform target, Vector3 startScale,GameObject targetObject)
    {
        //smooth animation to throw the zurks away


        float duration = 2f;
        float elapsed = 0f;



        while (elapsed < duration)
        {
            float t = elapsed / duration;


            target.localScale = startScale * Mathf.Lerp(1, 0, t);
     


            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = Vector3.zero;
        Destroy(targetObject);
    }
    private IEnumerator ThrowSand(Transform target, Vector3 startScale,GameObject targetObject)
    {
        //smooth animation to throw the zurks away


        float duration = 2f;
        float elapsed = 0f;



        while (elapsed < duration)
        {
            float t = elapsed / duration;


            target.localScale = startScale * Mathf.Lerp(1, 0, t);
     


            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = Vector3.zero;
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

            target.transform.position =  Vector3.MoveTowards(startPosition,transform.position,t*2);

            Debug.Log(target.localScale);


            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = Vector3.zero;

    }
}
