using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    //오브젝트 정보를 담고 있을 공간 필요
    [SerializeField]
    private ObjectData[] allStackObect;
    [SerializeField]
    private Transform lastStackObject;

    public int SpawnObject()
    {
        var stackObjects = SelectStackObject();

        var objectCount = SetupStackObjectCount();

        var index = SetupStartAndEndIndex(stackObjects);

        for (int i = 0; i < objectCount; i++)
        {
            var stackObject = Instantiate(stackObjects[Random.Range(index.Item1, index.Item2)]);

            stackObject.position = new Vector3 (0, -i * 0.5f, 0);
            stackObject.eulerAngles = new Vector3(0, -i *5, 0);

            stackObject.SetParent(transform);

        }

        lastStackObject.position = new Vector3(0, -objectCount * 0.5f, 0);
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


    private (int, int) SetupStartAndEndIndex(Transform[] transform)
    {
        int level = PlayerPrefs.GetInt("level");

        float startDuraction = 0.05f;
        float endDuraction = 0.1f;


        int startIndex = Mathf.Min((int)(level * startDuraction), transform.Length-1);
        int endIndex = Mathf.Min((int)(level * endDuraction) + 2, transform.Length);

        return (startIndex, endIndex);
    }


    // 구조체 // 생성될 오브젝트 데이터
    [System.Serializable]
    public struct ObjectData
    {
        public Transform[] objectStack;
    }

}
