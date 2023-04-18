using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPrefab : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // rotate the arrow to point towards the target
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
