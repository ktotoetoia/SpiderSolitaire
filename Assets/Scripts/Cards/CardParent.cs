using UnityEngine;

[DefaultExecutionOrder(1)]
public class CardParent : MonoBehaviour
{
    [SerializeField] private Vector3 offsetPercentageNormalized;
    [SerializeField] private float cameraOffset;
    private Bounds bounds;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        SetBounds();
        SetCameraTransform();
    }

    private void SetBounds()
    {
        var renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer render in renderers)
        {
            bounds.Encapsulate(render.bounds);
        }
    }

    private void SetCameraTransform()
    {
        SetCameraSize();
        SetCameraPosition();
    }

    private void SetCameraSize()
    {
        camera.orthographicSize = bounds.size.x / (2f * camera.aspect) + cameraOffset;
    }

    private void SetCameraPosition()
    {
        float x = bounds.center.x;
        float y = bounds.center.y - camera.orthographicSize + bounds.extents.y;
        float z = camera.transform.position.z;
        Vector2 safeAreaSizeInUnits = GetSafeAreaSize();
        float sizeDifferenceY = safeAreaSizeInUnits.y - (camera.orthographicSize * 2f);
        Vector2 position = new Vector3(x, y - sizeDifferenceY, z) + offsetPercentageNormalized * safeAreaSizeInUnits.y;

        camera.transform.position = position;
    }

    private Vector2 GetSafeAreaSize()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 safeAreaSizeInUnits = new Vector2(
            0,
            camera.ScreenToWorldPoint(new Vector3(0f, safeArea.height, 0f)).y -
            camera.ScreenToWorldPoint(new Vector3(0f, 0, 0f)).y
        );

        return safeAreaSizeInUnits;
    }
}