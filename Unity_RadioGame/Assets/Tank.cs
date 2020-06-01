using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Rigidbody tankRig;


    private void Start()
    {
        tankRig = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tankRig.AddForce(Vector3.back*5000*Time.deltaTime, ForceMode.Force);
    }
}
