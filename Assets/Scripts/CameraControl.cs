using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = target.transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(target == null)
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
        else
            transform.position = new Vector3(targetTransform.position.x, 0, transform.position.z);
    }
}
