using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject baratinhaPrefab;
    private SpriteRenderer spawnSprite;
    public float spawnRate;
    public int quantidade = 0;
    public GameObject instancia;

    public float nextSpawn = 0f;
 
    void Start()
    {
        spawnSprite = GetComponent<SpriteRenderer>();
        instancia = Instantiate(baratinhaPrefab, transform.position, spawnSprite.transform.rotation);
        instancia.transform.SetParent(spawnSprite.transform);
        quantidade += 1;
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time); // Espera o tempo definido   
        instancia = Instantiate(baratinhaPrefab, transform.position, spawnSprite.transform.rotation);
        instancia.transform.SetParent(spawnSprite.transform);
        //nextSpawn = Time.time + spawnRate;
        quantidade += 1;
        //Debug.Log("O comprimento da lista é: " + quantidade);
        
    }
    
    public void Spawn()
    {
        if (quantidade == 0)
        {
            StartCoroutine(Wait(spawnRate));
        }
    }
}
