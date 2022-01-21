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

    private void Update()
    {
    }

    private void LateUpdate()
    {
        Move();
    }
    private void Move()
    {

        if(target == null)
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
        else
            transform.position = new Vector3(targetTransform.position.x, 0, transform.position.z);
    }
}
