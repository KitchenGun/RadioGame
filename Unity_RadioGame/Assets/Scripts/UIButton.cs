using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum FunctionOption
{//기능
    Normal,
    Signal,
    Input,
}


public class UIButton : MonoBehaviour
{

    //기능 상태
    private FunctionOption funcOption;
    //버튼 이름
    private string buttonName;
    //채널
    private int channelNum;
    //사운드
    private bool isPowerOn;
    //방식
    private bool isJumping;
    //다이얼 버튼의 부모 (이미지 오브젝트)
    private GameObject DialParent;
    //숫자 텍스트
    [SerializeField]
    private GameObject num;
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private Text OfficerSpeak;
    [SerializeField]
    private Text Timer;
    [SerializeField]
    private GameObject canvas2;
    [SerializeField]
    private GameObject EndZone;

    private float distance;

    public void UIButtonClick()
    {//버튼 클릭시 실행함수
        //현재 클릭한 오브젝트 이름
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        //현재 클릭버튼의 부모 오브젝트는 다이얼 부모 이미지
        DialParent = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        //string 문으로 함수 실행 
        SendMessage(buttonName);
    }


    private void Start()
    {
        if (num != null)
        {
            num.SetActive(false);
        }
        if (canvas2 != null)
        {
            canvas2.SetActive(false);
        }
        isPowerOn = false;
        isJumping = false;
        channelNum = 0;

    }

    private void Update()
    {
            
            
            distance = Vector3.Distance(EndZone.transform.position, Tank.transform.position);
            Debug.Log(distance);
            Timer.text = "거리 :"+(distance-8).ToString()+"M";
            
    }

    #region RadioButton
    private void ChannelDial()
    {//채널 다이얼 클릭
        switch (channelNum)
        {//현재 다이얼을 이용해서 다음 다이얼 설정
            case 0:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 130);
                channelNum=1;
                break;
            case 1:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 105);
                channelNum=2;
                break;
            case 2:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 85);
                channelNum=3;
                break;
            case 3:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 65);
                channelNum=4;
                break;
            case 4:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 35);
                channelNum=5;
                break;
            case 5:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
                channelNum=6;
                break;
            case 6://다이얼6이 마지막임으로 초기화
                DialParent.transform.rotation = Quaternion.Euler(0, 0, -35);
                channelNum =0;
                break;
            default:
                break;
        }
    }

    private void SoundDial()
    {
        if(isPowerOn)
        {//파워꺼짐
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 140);
            num.SetActive(false);
            isPowerOn = false;
            OfficerSpeak.text = ("중대장 : \n뭐하는거야 전원 켜!");
        }
        else
        {//파워켜짐
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            num.SetActive(true);
            isPowerOn = true;
            OfficerSpeak.text = ("중대장 : \n기능을 정상으로 맞춰!");
        }
    }

    private void FunctionDial()
    {
        switch (funcOption)
        {//현재 상태에서 버튼 입력시 다음단계로 전환
            case FunctionOption.Normal:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 60);
                funcOption++;
                OfficerSpeak.text = ("중대장 : \n방식은 도약!");
                break;
            case FunctionOption.Signal:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 30);
                funcOption++;
                OfficerSpeak.text = ("중대장 : \n정상으로 맞추라고 정상!");
                break;
            case FunctionOption.Input:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
                funcOption =FunctionOption.Normal;
                OfficerSpeak.text = ("중대장 : \n정상으로 맞추라고 정상!"); 
                break;
            default:
                break;
        }
    }

    private void WayDial()
    {//방식이 도약일 경우와 아닐경우 체크
        if(isJumping)
        {
            OfficerSpeak.text = ("중대장 : \n방식은 도약!");
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            isJumping = false;
        }
        else
        {
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 40);
            isJumping = true;
            OfficerSpeak.text = ("중대장 : \n1채널!");
        }
    }
    #endregion


    private void CheckWin()
    {
        if(isPowerOn)
        {
            if (isJumping)
            {
                if(channelNum>=1)
                {
                    OfficerSpeak.text = ("중대장 : 두루미 두루미 여기는 휘파람 측이고 표적좌표 송신하겠음 \n52SDH4227358688 일대 적 전차 1대 식별 타격명령 하달하겠음!");
                    Debug.Log("작동성공");
                    
                    Tank.GetComponent<Tank>().Destroy();
                    canvas2.SetActive(true);
                    this.gameObject.SetActive(false);
                    return;
                }
            }
        }
        OfficerSpeak.text=("중대장 : \n무전기가 작동하지 않아 빨리 다시 작업해!");

    }


}
