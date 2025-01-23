using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MapInteraction : MonoBehaviour
{
    [Header("Map Interaction Variables")] [SerializeField]
    private float interactionDistance = 10f;

    [SerializeField] private KeyCode interactionKey = KeyCode.X;

    [Header("Map UI")] 
    [SerializeField] private GameObject mapObject;
    [SerializeField] private Image mapPopup;
    [SerializeField] private TextMeshPro interactionText;
    

    [Header("Map Fade Variables")] [SerializeField]
    private float duration = 2f;

    private List<GameObject> mapObjects = new List<GameObject>();
    private bool _isEnabled;
    private AnimationClip _mapAnim;

    private void Awake()
    {
        _isEnabled = true;
        mapPopup.gameObject.SetActive(false);   
        
    }

    private void LateUpdate()
    {
        Interaction();
    }

    private void Update()
    {
        MapSwitch();
    }


    private void Interaction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green);
            if (hit.transform.CompareTag("Map") || hit.transform.name == "Map")
            {
                interactionText.text = "Press " + "[" + interactionKey + "]";
                interactionText.gameObject.SetActive(true);
                interactionText.transform.position = hit.point + new Vector3(0, 0, .3f);
                if (Input.GetKeyDown(interactionKey))
                {
                    mapObjects.Add(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);
                    StartCoroutine(MapPopupController());
                }
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
            
        }
        else
        {
            interactionText.gameObject.SetActive(false);
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green);
        }
    }
    
    private void MapSwitch()
    {
        if (mapObjects.Count > 0)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                _isEnabled = !_isEnabled;
                mapObject.SetActive(_isEnabled);
                mapObject.transform.DOScale(Vector3.zero,0.5f).From();
            }
            
        }
    }

    IEnumerator MapPopupController()
    {
        mapPopup.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(duration);
        mapPopup.gameObject.SetActive(false);
    }
   
}