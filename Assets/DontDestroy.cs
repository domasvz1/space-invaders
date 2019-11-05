using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Sup");
        Destroy(other.gameObject);
    }
}
