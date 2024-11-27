using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkTimeHandler : MonoBehaviour
{
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(boom());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator boom()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(self);
    }
}
