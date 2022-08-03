using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _tripleLaserDuration = 5f, _speedDuration = 5f, _speedMultiplier = 1.75f, _shieldDuration = 2f;
    [SerializeField] private float _damageAbsorbMultiplier = 0.5f;
    [SerializeField] private string _powerupType = "TripleLaser";
    private Transform _player;
    void Start()
    {
        if(Cache.Contains("Player")) {
            _player = Cache.Get("Player");
        } else {
            _player = GameObject.Find("Player").transform;
            Cache.Put("Player", _player);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(_player != null) {
                Player player = _player.GetComponent<Player>();
                if(_player != null) {
                    if(_powerupType == "TripleLaser") activateTripleLaserPowerup(player);
                    if(_powerupType == "Speed") activateSpeedPowerup(player);
                    if(_powerupType == "Shield") activateShieldPowerup(player);
                }
            }   
        }
    }

    void activateShieldPowerup(Player player) {
        if(!player.isShieldActive()) {
            player.activateShield(_shieldDuration, _damageAbsorbMultiplier);
            Destroy(this.gameObject);
        }
    }

    void activateSpeedPowerup(Player player) {
        if(!player.isSpeedActive()) {
            player.activateSpeed(_speedDuration, _speedMultiplier);
            Destroy(this.gameObject);
        }
    }

    void activateTripleLaserPowerup(Player player) {
        if(!player.isTripleLaserActive()) {
            player.activateTripleLaser(_tripleLaserDuration);
            Destroy(this.gameObject);
        }
    }
}
