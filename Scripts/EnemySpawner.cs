using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : MonoBehaviour
{
    public string[] gelenVeriler;
    public string[] sureVeriler;
    private string spawnTime;
    public int time;
    public string enemyName;
    public GameObject skeleton;
    public GameObject eye;
    private GameObject spawn;

    private void Start()
    {
        StartCoroutine(GetEnemyNames());
        StartCoroutine(GetSpawnTime());
    }


    public void SpawnEnemies()
    {
        if (enemyName == "Iskelet")
        {
            spawn = skeleton;
        }
        else if (enemyName == "Goz")
        {
            spawn = eye;
        }
        /*
        else if (enemyName == null)
        {
            spawn = skeleton;
        }*/
        Instantiate(spawn, this.transform.position, Quaternion.identity);
    }

    IEnumerator GetEnemyNames()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "adCekme");
        form.AddField("id", 1);

        string url = "http://localhost/townHero/data.php"; // API'nin URL'si

        WWW w = new WWW(url, form);
        yield return w;
        gelenVeriler = w.text.Split(";");
        enemyName = gelenVeriler[0];
        SpawnEnemies();
    }

    IEnumerator GetSpawnTime()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "sureAl");
        form.AddField("id", 1);

        string url = "http://localhost/townHero/data.php"; // API'nin URL'si

        WWW w = new WWW(url, form);
        yield return w;
        sureVeriler = w.text.Split(";");
        spawnTime = sureVeriler[0];
        time = Convert.ToInt32(spawnTime);
        StartCoroutine(SpawnCo());
    }

    IEnumerator SpawnCo()
    {
        yield return new WaitForSecondsRealtime(time);
        SpawnEnemies();
    }

}

