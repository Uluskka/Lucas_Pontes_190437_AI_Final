using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{ // movimentacao do personagem.

    public Transform [] respawnsObjects;//posição que o player vai respawanar
    float speed = 20.0F; //velociade em que se move
    float rotationSpeed = 120.0F; //velociade da rotacao.
    public GameObject bulletPrefab; //prefeb da bala
    public Transform bulletSpawn; //metodo para a bala spawn
    public Slider healthBar;   //barra de vida.
    float health = 100.0f;      //vida

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed; //velociade da rotacao na vertical.
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed; //velocidade da rotacao na horinzontal.
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown("space")) //metodo para disparar as balas e colidir com o inimigo.
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 2000);
        }

        //informa o valor da barra de vida.
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
        healthBar.value = (int)health;
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);

        if(health<=0)
        Respawn();
    }

    void Respawn()
    {
        transform.position = respawnsObjects[Random.RandomRange(0,respawnsObjects.Length)].position;
        health = 100;
    }

    void OnCollisionEnter(Collision col) //metodo de colidir a bala no inimigo e fazer ele perde vida.
    {
        if (col.gameObject.tag == "bullet")
        {
            health -= 10;
        }
    }
}
