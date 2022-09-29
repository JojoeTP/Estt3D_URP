using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Image healthBar;
    public float health;
    public float startHealth = 100;

    void Start()
    {
        health = startHealth;

        healthBar.fillAmount = health/startHealth;
    }

    void Update()
    {
        
    }

    [PunRPC]
    public void TakeDamage(float _damage){
        health -= _damage;
        Debug.Log(health);

        healthBar.fillAmount = health / startHealth;
        if(health <= 0f){
            Die();
        }
    }

    void Die(){
        if(photonView.IsMine){
            PixelGunGameManager.instance.LeaveRoom();
        }
    }
}
