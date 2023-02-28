using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField] public GameObject SlicedObject;
    public void DestroyObject()
    {
        GameObject newBarrel = (GameObject)Instantiate(SlicedObject);
        newBarrel.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
        
    }
}
