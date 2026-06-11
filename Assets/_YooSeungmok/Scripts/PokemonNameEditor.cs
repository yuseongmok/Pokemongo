using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonNameEditor : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;          // 평소에 이름을 보여줄 텍스트
    public TMP_InputField nameInputField;
    public Button editButton;                // 연필 모양 버튼

    void Start()
    {
        nameInputField.gameObject.SetActive(false);
        nameText.gameObject.SetActive(true);
        
        nameInputField.text = nameText.text;

        // 인풋 필드 입력이 끝났을 때 실행될 이벤트 연결
        nameInputField.onEndEdit.AddListener(UpdateName);
    }

    //연필 버튼을 누르면 실행될 함수
    public void OnEditButtonClick()
    {
        Debug.Log("버튼 눌림!");
        //텍스트를 숨기고 입력창을 활성화
        nameText.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(true);

        //연필 버튼도 편집 중에는 잠시 숨겨둡니다.
        editButton.gameObject.SetActive(false);

        //입력창에 바로 타이핑할 수 있도록 포커스를 주고, 기존 글자를 전체 선택
        nameInputField.ActivateInputField();
        nameInputField.Select();
    }

    //이름 입력을 마치면 실행될 함수 (onEndEdit 이벤트)
    private void UpdateName(string newName)
    {
        // 공백 입력 방지 예외 처리
        if (!string.IsNullOrWhiteSpace(newName))
        {
            nameText.text = newName;
        }

        // 다시 원상태로 UI 전환
        nameInputField.gameObject.SetActive(false);
        nameText.gameObject.SetActive(true);
        editButton.gameObject.SetActive(true);
    }
}