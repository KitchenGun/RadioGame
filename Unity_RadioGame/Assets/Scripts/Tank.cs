using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    private Rigidbody tankRig;
    private GameObject mainGun;
    private bool isStop=false;
    [SerializeField]
    private Material destroyTank;
    [SerializeField]
    private GameObject particleGroup;

    public AudioSource audio;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag== "EndingZone")
        {//충돌하면 회전 실행
            audio.Play();
            isStop = true;
        }
        if (collision.transform.gameObject.tag == "FieldEndingZone")
        {//충돌하면 회전 실행
            SceneManager.LoadScene(3);
        }
    }


    private void Start()
    {
        //초기화
        tankRig = this.gameObject.GetComponent<Rigidbody>();
        mainGun = this.gameObject.transform.GetChild(2).gameObject;
        particleGroup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {// 멈추라는 명령어에 따라서
            tankRig.AddForce(Vector3.back * 5000 * Time.deltaTime, ForceMode.Force);
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                mainGun.GetComponent<MainGun>().TurretTurn();
            }
        }
    }

    public void Destroy()
    {
        Debug.Log("destroy");
        this.gameObject.GetComponent<MeshRenderer>().material=destroyTank;
        for(int i = 0;i<4;i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = destroyTank;
        }
        mainGun.GetComponent<Rigidbody>().AddExplosionForce(10000f, this.transform.position, 3f, 10000f);
        audio.Stop();
        isStop = true;
        particleGroup.SetActive(true);
    }
}
