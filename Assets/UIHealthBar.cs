using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar  : MonoBehaviour //用來控制敵人血條顯示
{
    public Transform target;
    public Vector3 offset;
    public Image foregroundImage;
    public Image backgroundImage;


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;  //血條不再一直固定在鏡頭
        foregroundImage.enabled = !isBehind;
        backgroundImage.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position+offset);   
    }
    public void SetHealthBarPercentage(float percentage)               //血條效果
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
