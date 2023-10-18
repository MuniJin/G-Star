using UnityEngine;

public class animationctr : MonoBehaviour
{
    public GameObject[] blues;
    public GameObject[] reds;

    public Animator BlueObj;
    public Animator RedObj;

    private AlKKAGIManager AlKKAGIManager;
    public GameObject CrashObjR; //빨강 충돌한 기물
    public GameObject CrashObjB; //파랑 충돌한 기물

    private void Awake()
    {
        AlKKAGIManager = FindObjectOfType<AlKKAGIManager>();

        if (AlKKAGIManager != null)
        {
            // AlKKAGIManager 스크립트의 CrashObjR과 CrashObjB를 가져와서 할당합니다.
            CrashObjR = AlKKAGIManager.CrashObjR;
            CrashObjB = AlKKAGIManager.CrashObjB;

            Debug.Log("CrashObjR: " + CrashObjR); // Debug.Log를 추가하여 올바른 값이 할당되었는지 확인합니다.
            Debug.Log("CrashObjB: " + CrashObjB);

        }
        else
        {
            Debug.LogError("AlKKAGIManager 스크립트를 찾을 수 없습니다.");
        }

        // 타임라인 실행
        BlueObj.enabled = true;
        RedObj.enabled = true;

        ActiveObj(CrashObjB, CrashObjR);

        if (CrashObjR != null && CrashObjB != null)
        {
            Debug.Log("CrashObjR and CrashObjB are assigned correctly.");
        }
        else
        {
            Debug.LogError("CrashObjR or CrashObjB is null.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ActiveObj(GameObject CrashObjB, GameObject CrashObjR)
    {
        Debug.Log("CrashObjB active: " + CrashObjB.activeSelf);
        Debug.Log("CrashObjR active: " + CrashObjR.activeSelf);

        if (CrashObjB != null && CrashObjR != null)
        {
            // 모든 요소를 비활성화
            for (int i = 0; i < blues.Length; i++)
            {
                blues[i].SetActive(false);
                reds[i].SetActive(false);


            }
            // 전달받은 오브젝트를 활성화
            CrashObjB.SetActive(true);
            CrashObjR.SetActive(true);


        }
        else
        {
            Debug.LogError("CrashObjB or CrashObjR is null.");
        }
    }
}



