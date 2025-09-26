using System.Collections;
using UnityEngine;

public class Guzzling : MonoBehaviour
{
    Ray _guzzlingRay = new Ray();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _guzzlingRay.direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject sandObject;
        RaycastHit sandHit;
        if (Physics.Raycast(_guzzlingRay,out sandHit,100,~3))
        {
            sandObject = sandHit.collider.gameObject;


            Vector3 localScaleSand = sandObject.transform.localScale;
            localScaleSand = localScaleSand * 0.99f;


            sandHit.collider.gameObject.transform.localScale = localScaleSand;
            sandHit.collider.gameObject.transform.position -= (sandHit.transform.position - transform.parent.gameObject.transform.position) * 0.01f ;
        
            

        }
        Debug.DrawRay(_guzzlingRay.origin,_guzzlingRay.direction);
    }

}
