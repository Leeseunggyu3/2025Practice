using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MaintainAspectRatio : MonoBehaviour
{
    [SerializeField]
    private float targetAspect = 16f / 9f; // ���ϴ� ȭ�� ���� (��: 16:9)

    void Start()
    {
        SetAspectRatio();
    }

    void SetAspectRatio()
    {
        // ���� ȭ���� ���� ���
        float windowAspect = (float)Screen.width / Screen.height;

        // ���� ȭ�� ������ ��ǥ ȭ�� ���� ��
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            // ���ΰ� �� �� ��: ���Ͽ� ���͹ڽ� �߰�
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // ���ΰ� �� �� ��: �¿쿡 ���͹ڽ� �߰�
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    void OnValidate()
    {
        // �ν����Ϳ��� ���� �� �ٷ� �ݿ�
        SetAspectRatio();
    }
}
