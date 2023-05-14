using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamMaker : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float scaleSpeed;
    [SerializeField] private ParticleSystem creamParticle;
    public Transform startPoint;
    
    public float _timer = 0f;
    public float delayTime = 3f;
    
    private GameObject _recentMakedGameObject;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        // 처음 누른지 몇 초 후에 누를 수 있도록함.
        if (Input.GetMouseButtonDown(0) && TimeCheck())
        {
            // 처음 입력했을 때 물체 감지, 오브젝트 생성
            DetectObject();
        }
        if (Input.GetMouseButton(0))
        {
            if (_recentMakedGameObject != null)
            {
                ScaleUp();
            }
            // 누르는 동안 케이크 스케일 증가
        }
        if (Input.GetMouseButtonUp(0))
        {
            _recentMakedGameObject = null;
            _timer = 0f;
            creamParticle.Stop();
            //  _recentMakedGameObject = null;
            // 생성중이던 케이크 스케일 증가 멈춤
        }
    }

    void DetectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _recentMakedGameObject = Instantiate(_prefab, hit.point, Quaternion.Euler(270f,0f,0f));
            _recentMakedGameObject.transform.position += Vector3.up * 0.15f;
            creamParticle.Play();
            creamParticle.transform.position = startPoint.transform.position;
            creamParticle.transform.LookAt(hit.point);
        }
    }

    void ScaleUp()
    {
        if (TimeCheck())
        {
            float scaleFactor = 1.0f + scaleSpeed * Time.deltaTime;
            _recentMakedGameObject.transform.localScale *= scaleFactor;
        }
    }

    bool TimeCheck()
    {
        if (_timer >= delayTime)
        {
            return true;
        }

        return false;
    }
}
