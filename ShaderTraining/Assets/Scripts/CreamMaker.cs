using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamMaker : MonoBehaviour
{
    [SerializeField] private bool isMouseDown;
    private GameObject _recentMakedGameObject;
    void Start()
    {
        isMouseDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 처음 누른지 몇 초 후에 누를 수 있도록함.
        if (Input.GetMouseButtonDown(0) && isMouseDown )
        {
            // 처음 입력했을 때 물체 감지, 오브젝트 생성
            DetectObject();
        }
        if (Input.GetMouseButton(0))
        {
            isMouseDown = true;
            // 누르는 동안 케이크 스케일 증가
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            // 생성중이던 케이크 스케일 증가 멈춤
        }
    }

    void DetectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //_recentMakedGameObject = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform);
        }
    }
}
