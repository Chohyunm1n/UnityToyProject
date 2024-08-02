using UnityEngine;

public class Pole : MonoBehaviour
{

    //기둥 임의로 설정된 색을 좀 더 옅은 색으로 보이게 하는데
    //임의의 색상을  GameController.Awake 에서 설정하기 때문에
    // 임의 색상으로 변경한 이후인 Start 메소드에서 추가 설정



    private void Start()
    {
        GetComponent<MeshRenderer>().material.color += Color.gray;
    }
}
