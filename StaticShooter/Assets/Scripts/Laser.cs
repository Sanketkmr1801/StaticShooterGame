using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(transform.position.x > 12f || transform.position.x < -12f || transform.position.y > 8f || transform.position.y < -8f) {
            Destroy(this.gameObject);
        }
    }
}