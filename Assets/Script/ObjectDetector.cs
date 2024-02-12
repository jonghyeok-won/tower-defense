using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;

    [SerializeField]
    private TowerDateViewer towerDataViewer;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject()==true)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            // 카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성
            //ray.origin : 광선의 시작 위치(=카메라 위치)
            //ray.directio : 광선의 진행방향
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            // 2D 모니터를 통해 3D월드의 오브젝트를 마우스로 선택하는 방법
            // 광선에 부딪히는 오브젝트를 검출해서 hit에 저장
            if(Physics.Raycast(ray, out hit, Mathf.Infinity) ) 
            {
                hitTransform = hit.transform;

                if(hit.transform.CompareTag("Tile"))
                {
                    towerSpawner.SpawnTower(hit.transform);
                }

                else if(hit.transform.CompareTag("Tower"))
                {
                    towerDataViewer.OnPanel(hit.transform);  
                }
            }  
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hitTransform == null || hitTransform.CompareTag("Tower") == false)
            {
                towerDataViewer.OffPanel();
            }
            hitTransform = null;
        }
    }
}
