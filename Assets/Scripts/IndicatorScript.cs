using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public GameObject target; // the target to track
    public GameObject indicatorPrefab; // the arrow indicator prefab

    private GameObject indicator; // the instantiated arrow indicator
    private Camera cam; // reference to the camera
    private Vector2 screenBounds; // the screen boundaries
    private Vector2 targetPos; // the target's position
    private Bounds targetBounds; // the target's bounds

    // Start is called before the first frame update
    void Start()
    {
        // get reference to the camera
        cam = Camera.main;

        // calculate the screen boundaries
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        // instantiate the arrow indicator
        indicator = Instantiate(indicatorPrefab, transform);

        // get the target's bounds
        targetBounds = target.GetComponent<Collider2D>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        // get the target's position
        targetPos = target.transform.position;

        // calculate the arrow indicator's position
        float arrowPosX = Mathf.Clamp(targetPos.x, screenBounds.x, screenBounds.x * -1);
        float arrowPosY = Mathf.Clamp(targetPos.y, screenBounds.y, screenBounds.y * -1);
        Vector3 arrowPos = new Vector3(arrowPosX, arrowPosY, 0);
        Vector3 camPos = cam.transform.position;
        Vector3 direction = (arrowPos - camPos).normalized;

        // set the arrow indicator's position and rotation
        indicator.transform.position = camPos + direction * 5f;
        indicator.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

}
