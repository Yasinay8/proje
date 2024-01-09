using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlyingEyeAttack : MonoBehaviour
{
    public string[] gelenVeriler;
    public string idNumber;
    public int damage = 10;
    public string enemyName = "Goz";
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
        if (idNumber != "2")
        {
            StartCoroutine(SaveStats());
        }
    }

    IEnumerator GiveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "adVer");
        form.AddField("id", 1);
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
        form.AddField("id", 2);

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
