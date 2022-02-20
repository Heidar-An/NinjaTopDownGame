using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBoxAppear : MonoBehaviour
{
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void SetActive(){
        active = !active;
        gameObject.GetComponent<Image>().enabled = active;
    }
}
