using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChasing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera mainCamera;
    public 
    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit)){
            transform.position = raycastHit.point;
            // Debug.Log(raycastHit.point);
            // transform.position = transform.position + new Vector3(-1.381747,1f,-1);
        }
    }
}
