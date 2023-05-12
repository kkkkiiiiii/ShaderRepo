using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float lineWidth = 0.1f;
    [SerializeField] private GameObject _linePrefab;
    private GameObject _recentLineObject;
    private LineRenderer _lineRenderer;
    private int _vertexCount = 0;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _recentLineObject = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform);
                // 충돌한 오브젝트에 접근하여 처리 또는 조작 
                
                Debug.Log(hit.collider.gameObject.name +"hit.point " + hit.point);
                //Debug.Log("hit.distance " +hit.distance);
                //Debug.Log("hit.transform.position s"+hit.transform.position);
            }
        }

        else if (Input.GetMouseButton(0)) // 가장 최근 오브젝트에 addPoint
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 오브젝트에 접근하여 처리 또는 조작 
                GameObject hitObject = hit.collider.gameObject;
                //_recentLineObject.transform.position = hitObject.transform.position;
                Debug.Log(hitObject.name +"hit.point " + hit.point);
                
                AddPoint(hit.point);
                //Debug.Log("hit.distance " +hit.distance);
                //Debug.Log("hit.transform.position s"+hit.transform.position);
            }
        }
    }
    
    void AddPoint(Vector3 point)
    {
        LineRenderer lineRenderer = _recentLineObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = _vertexCount+1;
        lineRenderer.SetPosition(_vertexCount, point);
        _vertexCount++;
    }
}
