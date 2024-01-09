using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public Animator anim;
    public int enemyDyingPoint = 1;

    private void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        StartCoroutine(GiveData());
        anim.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }

    IEnumerator GiveData()
    {
       WWWForm form = new WWWForm();
        form.AddField("unity", "kayit");
        form.AddField("karakter_puan", enemyDyingPoint);

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
