using UnityEngine;

public class animationctr : MonoBehaviour
{
    public GameObject[] blues;
    public GameObject[] reds;

    public Animator BlueObj;
    public Animator RedObj;

    private AlKKAGIManager AlKKAGIManager;
    public GameObject CrashObjR; //���� �浹�� �⹰
    public GameObject CrashObjB; //�Ķ� �浹�� �⹰

    private void Awake()
    {
        AlKKAGIManager = FindObjectOfType<AlKKAGIManager>();

        if (AlKKAGIManager != null)
        {
            // AlKKAGIManager ��ũ��Ʈ�� CrashObjR�� CrashObjB�� �����ͼ� �Ҵ��մϴ�.
            CrashObjR = AlKKAGIManager.CrashObjR;
            CrashObjB = AlKKAGIManager.CrashObjB;

            Debug.Log("CrashObjR: " + CrashObjR); // Debug.Log�� �߰��Ͽ� �ùٸ� ���� �Ҵ�Ǿ����� Ȯ���մϴ�.
            Debug.Log("CrashObjB: " + CrashObjB);

        }
        else
        {
            Debug.LogError("AlKKAGIManager ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }

        // Ÿ�Ӷ��� ����
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
            // ��� ��Ҹ� ��Ȱ��ȭ
            for (int i = 0; i < blues.Length; i++)
            {
                blues[i].SetActive(false);
                reds[i].SetActive(false);


            }
            // ���޹��� ������Ʈ�� Ȱ��ȭ
            CrashObjB.SetActive(true);
            CrashObjR.SetActive(true);


        }
        else
        {
            Debug.LogError("CrashObjB or CrashObjR is null.");
        }
    }
}



