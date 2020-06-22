using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGun : MonoBehaviour
{
    [SerializeField]
    private GameObject ex;
    [SerializeField]
    private GameObject Fire;
    [SerializeField]
    private GameObject Panel;


    private bool isfire;

    private void Start()
    {
        ex.SetActive(false);
        Fire.SetActive(false);
        Panel.SetActive(false);
        isfire = false;
    }

    // Start is called before the first frame update
    public void TurretTurn()
    {
        Vector3 gunPos = this.transform.rotation.eulerAngles;
        if (gunPos.y <= 240f)
        {//40도 이상이면 회전 멈추기
            gunPos.y += 1f;
            Debug.Log(gunPos);
            this.transform.rotation = Quaternion.Euler(gunPos);
        }
        if (isfire == false)
        {
            if (gunPos.y >= 240f)
            {
                if (ex == null)
                {
                    return;

                }
                else
                {
                    Fire.SetActive(true);
                    Invoke("exOn", 1f);
                    isfire = true;
                }
            }
        }
    }


    private void exOn()
    {
        ex.SetActive(true);
        Panel.SetActive(true);
    }
}
