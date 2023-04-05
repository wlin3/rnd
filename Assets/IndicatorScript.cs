using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject Target;

    Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rd.isVisible == false)
        {
            if(Indicator.activeSelf == false)
            {
                Indicator.SetActive(true);
            }

            Vector2 direction = Target.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

            if(ray.collider != null)
            {
                Indicator.transform.position = ray.point;
            }

            else
            {
                if (Indicator.activeSelf == true)
                {
                    Indicator.SetActive(false);
                }
            }
        }
    }
}
