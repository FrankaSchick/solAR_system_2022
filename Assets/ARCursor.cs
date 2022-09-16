using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{

    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;
    public bool isPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursor);

    }

    // Update is called once per frame
    void Update()
    { 

        if (isPlaced)
        {
            cursorChildObject.SetActive(false);
        }
        else
        {

            if (useCursor)
            {
                UpdateCursor();
            }
        }
       
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isPlaced==false)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position + new Vector3(0f, 0f, 0f), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
                isPlaced = true;
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
        
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
