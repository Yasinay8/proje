using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class SkeletonAttackArea : MonoBehaviour
{
    public string[] gelenVeriler;
    public int damage = 20;
    public string idNumber;
    public string enemyName = "Iskelet";
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<HeroHealth>().TakeDamage(damage);
            StartCoroutine(GiveData());
        }
    }

    private void Start()
    {
        StartCoroutine(CheckTheStats());
        if (idNumber != "1")
        {
            StartCoroutine(SaveStats());
        }
    }

    void Update()
    {

    }

    IEnumerator GiveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "adVer");
        form.AddField("dusman", enemyName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/townHero/data.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                Debug.Log("tamam");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator SaveStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "damageKaydetDusmanlar");
        form.AddField("id", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/townHero/data.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                Debug.Log("kayit edildi");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

    IEnumerator CheckTheStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "dusmanSorgula");

        string url = "http://localhost/townHero/data.php";

        WWW w = new WWW(url, form);
        yield return w;
        gelenVeriler = w.text.Split(";");
        idNumber = gelenVeriler[0];
    }

}
