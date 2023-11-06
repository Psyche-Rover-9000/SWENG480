using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPOS = transform.position;
        viewPOS.x = Mathf.Clamp(viewPOS.x, screenBounds.x, screenBounds.x );
        viewPOS.y = Mathf.Clamp(viewPOS.y, screenBounds.y, screenBounds.y );
        transform.position = viewPOS;
    }
}
