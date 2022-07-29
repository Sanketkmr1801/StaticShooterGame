using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    public float horizontalInput, verticalInput;
    [SerializeField] private GameObject _laser;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        move();
        fireLaser();

    }

    void fireLaser() {
            if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject newLaser = Instantiate(_laser, transform.position, transform.rotation);
            float angle = Vector3.SignedAngle(new Vector3(0, Math.Abs(transform.position.y), 0), mouseWorldPosition - transform.position, new Vector3(0, 0, 1));
            Debug.Log(angle);
            newLaser.transform.Rotate(new Vector3(0, 0, angle));
        }
    }

    void move() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * _speed * verticalInput);

        if(transform.position.y > 7f) {
            transform.position = new Vector3(transform.position.x, -7f, transform.position.z);
        }
        if(transform.position.y < -7f) {
            transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
        }
        if(transform.position.x > 11f) {
            transform.position = new Vector3(-11f, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -11f) {
            transform.position = new Vector3(11f, transform.position.y, transform.position.z);
        }
    }
}
