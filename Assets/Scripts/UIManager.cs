using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject UICanvas;
    public GameObject UIPanel;
    private GameObject tempPanel;
    public List <GameObject> UIPanelList;
    public GameObject shape;
    Image orderImage;
    Sprite shapeSprite;


    void Awake()
    {
        Instance = this;
    }

    // Needs to take in a new order. From that order, need to activate a new panel, use the number of edges and colour to select a sprite of the shape and colour required. Then sort out the instructions.
    public void AddOrderToUI(Order newOrder)
    {
        var rectTransform = (RectTransform) UICanvas.transform;
        // Create new UI panel for the order
        tempPanel = Instantiate(UIPanel);
        tempPanel.transform.SetParent(UICanvas.transform, false);
        tempPanel.transform.localPosition = new Vector3(-rectTransform.rect.width / 2f + 55f + (UIPanelList.Count * 100), rectTransform.rect.height / 2f - 30f, 0);
        UIPanelList.Add(tempPanel);
        
        tempPanel.GetComponent<UIOrderPanel>().Init(null, null, null, null, newOrder);
    }

    public void RemoveOrderFromUI() // Needs to remove the first order from the UI and move the rest along.
    {
        Destroy(UIPanelList[0]);
        UIPanelList.RemoveAt(0);
        for (int i = 0; i < UIPanelList.Count; i++)
        {
            UIPanelList[i].transform.localPosition = new Vector3(UIPanelList[i].transform.localPosition.x - 100, UIPanelList[i].transform.localPosition.y, UIPanelList[i].transform.localPosition.z);
        }
    }
}
