using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : MonoBehaviour
{
    public int time;
    void Start()
    {
        StartCoroutine(TimeProgressor());
    }

    private void Update()
    {
        if (time == 10)
        {
            SendTimeData();
        }
    }

    IEnumerator TimeProgressor()
    {
        time += 1;
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(TimeProgressor());
    }

    public void CallSendTime()
    {
        StartCoroutine(SendTimeData());
    }

    IEnumerator SendTimeData()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "sureKaydet");
        form.AddField("oynadýgý_sure", time);

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
