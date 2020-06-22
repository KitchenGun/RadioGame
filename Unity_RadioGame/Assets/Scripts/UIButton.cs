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
    private GameObject canvas2;

    public void UIButtonClick()
    {//버튼 클릭시 실행함수
        //현재 클릭한 오브젝트 이름
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        //현재 클릭버튼의 부모 오브젝트는 다이얼 부모 이미지
        DialParent = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        //string 문으로 함수 실행 
        SendMessage(buttonName);
    }


    #region MainLobby
    private void Tutorial()
    {
        SceneManager.LoadScene(1);
    }

    private void Field()
    {
        SceneManager.LoadScene(2);
        if (num !=null)
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

        Tank = GameObject.Find("Tank");
    }

    #endregion

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
        }
        else
        {//파워켜짐
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            num.SetActive(true);
            isPowerOn = true;
        }
    }

    private void FunctionDial()
    {
        switch (funcOption)
        {//현재 상태에서 버튼 입력시 다음단계로 전환
            case FunctionOption.Normal:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 60);
                funcOption++;
                break;
            case FunctionOption.Signal:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 30);
                funcOption++;
                break;
            case FunctionOption.Input:
                DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
                funcOption =FunctionOption.Normal;
                break;
            default:
                break;
        }
    }

    private void WayDial()
    {//방식이 도약일 경우와 아닐경우 체크
        if(isJumping)
        {
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            isJumping = false;
        }
        else
        {
            DialParent.transform.rotation = Quaternion.Euler(0, 0, 40);
            isJumping = true;
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
