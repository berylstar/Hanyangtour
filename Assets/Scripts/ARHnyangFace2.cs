using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.Collections;

public class ARHnyangFace2 : MonoBehaviour
{
    public GameObject nosePrefab;

    ARFaceManager arFaceManager;
    ARSessionOrigin sessionOrigin;

    NativeArray<ARCoreFaceRegionData> faceRegions;

    GameObject noseObject;

    void Start()
    {
        arFaceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        ARCoreFaceSubsystem subsystem = (ARCoreFaceSubsystem)arFaceManager.subsystem;

        foreach(ARFace face in arFaceManager.trackables)
        {
            subsystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegions);

            foreach (ARCoreFaceRegionData faceRegion in faceRegions)
            {
                ARCoreFaceRegion regionType = faceRegion.region;

                if(regionType == ARCoreFaceRegion.NoseTip)
                {
                    if(!noseObject)
                    {
                        noseObject = Instantiate(nosePrefab, sessionOrigin.trackablesParent);
                    }

                    noseObject.transform.localPosition = faceRegion.pose.position;
                    noseObject.transform.localRotation = faceRegion.pose.rotation;
                }
            }
        }
    }


    public void CaptureScreenForMobile(string fileName)
    {
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        // do something with texture
        string albumName = "HnyangTOUR";
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        // cleanup
        Object.Destroy(texture);
    }
}
