using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardsDealInput : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int dealCount = 5;
    [SerializeField] private Vector3 cardsOffset = new Vector3(0.3f, 0);
    [Inject] private CardsDeal cardsDeal;
    private List<GameObject> objects = new List<GameObject>();
    private Bounds bounds;

    public int DealLast { get { return objects.Count; } }
    public DealCard LastCard { get { return objects.LastOrDefault().GetComponent<DealCard>(); } }
    
    private void Start()
    {
        for (int i = 0; i < dealCount; i++)
        {
            GameObject obj = Instantiate(prefab, transform.position - cardsOffset * i, prefab.transform.rotation, transform);
            obj.GetComponent<Renderer>().sortingOrder = i - dealCount;
            obj.GetComponent<DealCard>().CardTransform.SetPosition(obj.transform.position);
            objects.Add(obj);
        }

        UpdateColliderBounds();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && bounds.Contains(GetMousePosition()))
        {
            Deal();
            UpdateColliderBounds();
        }
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Deal()
    {
        GameObject lastObject = objects.Last();

        objects.Remove(lastObject);
        cardsDeal.Deal(lastObject.transform.position);
        Destroy(lastObject);
    }

    private void UpdateColliderBounds()
    {
        IEnumerable<Renderer> renderers = objects.Select(x => x.GetComponent<Renderer>());

        bounds = renderers.FirstOrDefault()?.bounds ?? new Bounds();

        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
    }
}