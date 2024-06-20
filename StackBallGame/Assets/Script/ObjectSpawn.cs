using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    //오브젝트 정보를 담고 있을 공간 필요
    public ObjectData[] allStackObect;


    //TODO: 오브젝트가 생성 구조 및 위치
    //오브젝트 생성될 때 레벨에 따라 난이도 추가
    public int SpawnObject()
    {
        var stackObjects = SelectStackObject();

        var objectCount = SetupStackObjectCount();

        return objectCount;
    }
    

    //랜덤으로  오브젝트 유형 선택
    public Transform[] SelectStackObject()
    {
        int objectIndex = Random.Range(0, allStackObect.Length);

        Transform[] selectStackObjectp = new Transform[allStackObect[objectIndex].objectStack.Length];

        for (int i = 0; i < allStackObect[objectIndex].objectStack.Length; i++)
        {
            selectStackObjectp[i] = allStackObect[objectIndex].objectStack[i];
        }

        return selectStackObjectp;
    }



    //레벨에 따라 생성될 오브젝트 생성 제어

    public int SetupStackObjectCount()
    {
        int level = PlayerPrefs.GetInt("level");

        int baseCount = 10;

        if (level % 5 == 0)
        {
            baseCount += 5;
        }

        int StackObjectCount = baseCount + level;

        return StackObjectCount;
    }





    // 구조체 // 생성될 오브젝트 데이터
    public struct ObjectData
    {
        public Transform[] objectStack;
    }

}
