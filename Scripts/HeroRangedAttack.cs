using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeroRangedAttack : MonoBehaviour
{
    public string[] gelenVeri;
    public string toplamZararVerisi;
    public int zararInt;
    public int speed;
    public int damage;
    public int dir;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GoWithDirection(dir);
    }

    public void GoWithDirection(int dir)
    {
        rb.velocity = new Vector2 (speed * dir, 0);
    }

    private void FixedUpdate()
    {
        StartCoroutine(VeriCek());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            StartCoroutine(VeriVer());
            Destroy(this.gameObject);
        }
    }

    IEnumerator VeriCek()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "uzakSaldiriCekme");
        form.AddField("id", 1);

        string url = "http://localhost/townHero/data.php"; // API'nin URL'si

        WWW w = new WWW(url, form);
        yield return w;
        gelenVeri = w.text.Split(";");
        toplamZararVerisi = gelenVeri[0];
        zararInt = Convert.ToInt32(toplamZararVerisi);
    }

    IEnumerator VeriVer()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "uzakSaldiriGuncelle");
        form.AddField("id", 1);
        form.AddField("ranged_damage", damage + zararInt);

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
