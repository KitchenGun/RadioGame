using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    private string buttonName;
    public void UIButtonClick()
    {//버튼 클릭시 실행함수
        buttonName = EventSystem.current.currentSelectedGameObject.name;//현재 클릭한 오브젝트 이름
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
        
    }

    private void Exit()
    {
        Application.Quit();
    }

    #endregion
    private void ReturnMain()
    {
        SceneManager.LoadScene(0);
    }
}
