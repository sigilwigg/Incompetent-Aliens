using UnityEngine;
using System.Collections.Generic;
using System;

public class WallCutout : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera groupCamera;

    private int counter = 0;

    private void Awake()
    {
        groupCamera = GetComponent<Camera>();
    }


    private void Update()
    {

        Vector2 cutoutPos = groupCamera.WorldToViewportPoint(targetObject.position);

        cutoutPos.y /= (Screen.width / Screen.height);


        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            counter++;
            Debug.Log(counter);
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }
}
