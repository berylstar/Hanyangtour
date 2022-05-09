using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARHnyangFace : MonoBehaviour
{
    ARFaceManager arFaceManager;

    public Material faceMaterial;

    void Start()
    {
        arFaceManager = GetComponent<ARFaceManager>();
        arFaceManager.facePrefab.GetComponent<MeshRenderer>().material = faceMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
