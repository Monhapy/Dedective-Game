using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapSystem : MonoBehaviour
{
    public static MapSystem Instance;
    [SerializeField] private Camera mapCamera;
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> targets;
    [SerializeField] private RectTransform indicatorPrefab;
    [SerializeField] private RawImage mapImage;
    private List<RectTransform> _indicators = new List<RectTransform>();
    
    private void Awake()
    {
        gameObject.SetActive(true);
        gameObject.SetActive(false);
        Instance = this;
        foreach (Transform target in targets)
        {
            RectTransform indicator = Instantiate(indicatorPrefab, mapImage.transform);
            _indicators.Add(indicator);
            
        }
        
    }

    void Update()
    {
        MapCameraPosition();
        WayPointIndicator();
    }

    private void WayPointIndicator()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 screenPosition = mapCamera.WorldToViewportPoint(targets[i].position);
            bool isOnScreen = screenPosition.x > 0 && screenPosition.x < 1 && screenPosition.y > 0 &&
                              screenPosition.y < 1 && screenPosition.z > 0;
            Vector3 dir = (targets[i].position - player.position).normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            _indicators[i].transform.localEulerAngles = new Vector3(0, 0, angle);
        
            if (isOnScreen)
            {
                _indicators[i].DOScale(0.5f, 1);
                _indicators[i].GetComponent<Image>().color = Color.blue;
                Vector2 mapSize = mapImage.rectTransform.rect.size;
                Vector2 localPosition = new Vector2(
                    (screenPosition.x - 0.5f) * mapSize.x,
                    (screenPosition.y - 0.5f) * mapSize.y
                );
                _indicators[i].anchoredPosition = localPosition;
            }
            else
            {
                _indicators[i].DOScale(0.2f, 1);
                _indicators[i].GetComponent<Image>().color = Color.red;
                Vector2 mapSize = mapImage.rectTransform.rect.size;
                Vector2 localPosition = new Vector2(
                    Mathf.Clamp(screenPosition.x, 0, 1) * mapSize.x - mapSize.x / 2,
                    Mathf.Clamp(screenPosition.y, 0, 1) * mapSize.y - mapSize.y / 2
                );
                _indicators[i].anchoredPosition = localPosition;
            }
        }
    }

    public void GetTalkingNPC(int talkIndex)
    {
        _indicators[talkIndex].gameObject.SetActive(false);
    }
    private void MapCameraPosition()
    {
        Vector3 newPosition = player.position;
        newPosition.y = mapCamera.transform.position.y;
        mapCamera.transform.position = newPosition;
        mapCamera.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}