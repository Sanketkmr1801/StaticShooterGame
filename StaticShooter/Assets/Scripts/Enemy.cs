using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float _health = 100f, _speed = 4f, _damage = 25f;
    private static float _number = 0f;
    [SerializeField] private float _enemyNumber;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        _number++;
        _enemyNumber = _number;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void damageEnemy(float damage) {
        if(damage < _health) {
            _health -= damage;
        } else {
            Destroy(this.gameObject);
        }
        Debug.Log(_health);
    }

    void move() {
        if(player != null) transform.LookAt(player);
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            // damagePlayer();
            other.GetComponent<Player>().damagePlayer(_damage);
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser") {
            Laser incomingLaser = other.GetComponent<Laser>();
            damageEnemy(incomingLaser.getDamage());
            Destroy(other.gameObject);
        }

        if(other.tag == "Enemy") {
            Enemy collidedEnemy = other.GetComponent<Enemy>();
            if(this._enemyNumber < collidedEnemy._enemyNumber) {
                getStronger(collidedEnemy._speed, collidedEnemy._damage, collidedEnemy._health);
                Destroy(other.gameObject);
            }
        }
    }

    float getNumber() {
        return _enemyNumber;
    }
    void getStronger(float speed, float damage, float health) {
        _speed += speed * 0.5f;
        _damage += damage;
        _health += health;
        Debug.Log(_health);
    }
}
