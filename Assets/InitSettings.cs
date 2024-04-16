using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSettings : MonoBehaviour
{
    public GameObject menu;
    
    // Start is called before the first frame update
    void Start()
    {
        menu.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
}
