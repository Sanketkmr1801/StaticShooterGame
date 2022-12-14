using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _health = 100f, _speed = 4f, _damage = 25f, _score = 10f;
    private static float _number = 0f;
    [SerializeField] private float _enemyNumber;
    void Start()
    {
        if(Cache.Contains("Player")) {
            _player = Cache.Get("Player");
        } else {
            _player = GameObject.Find("Player").transform;
            Cache.Put("Player", _player);
        }
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
            _player.GetComponent<Player>().addScore(_score);
            Destroy(this.gameObject);
        }
    }

    void move() {
        if(_player != null) transform.LookAt(_player);
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
            other.GetComponent<MeshRenderer>().enabled = false;
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
    }
}
