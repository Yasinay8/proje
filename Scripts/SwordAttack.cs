using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SwordAttack : MonoBehaviour
{
    public string[] gelenVeri;
    public string toplamZararVerisi;
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            StartCoroutine(VeriVer());
            StartCoroutine(GiveData());
        }
    }
    private void FixedUpdate()
    {
        StartCoroutine(VeriCek());
    }

    IEnumerator GiveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "zarar");
        form.AddField("givenDamage", damage);

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

    IEnumerator VeriCek()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "yakinSaldiriCekme");
        form.AddField("id", 1);

        string url = "http://localhost/townHero/data.php"; // API'nin URL'si

        WWW w = new WWW(url, form);
        yield return w;
        gelenVeri = w.text.Split(";");
        toplamZararVerisi = gelenVeri[0];
    }

    IEnumerator VeriVer()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "yakinSaldiriGuncelle");
        form.AddField("id", 1);
        form.AddField("melee_damage", damage);

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

}
