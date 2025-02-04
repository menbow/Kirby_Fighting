using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    //画像データを取得：Unity専用命令っぽい
    private const string k_propName = "_MainTex";

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float mainCameraXMax = 0;
    [SerializeField] private float endOffset = 0;

    private Material m_material;

    private void Start()
    {
        //ImageコンポーネントがImage型であるか判断
        if (GetComponent<Image>()is Image i)
        {
            //m_materialにImageコンポーネントのmaterialプロパティを代入
            m_material = i.material;
        }
    }

    private void Update()
    {
        //カメラの座標取得
        Vector2 mainCameraPosition = mainCamera.transform.position;
        Vector2 offset = Vector2.zero;

        if(m_material)
        {
            //カメラが動いた分だけ背景スクロール
            float x = mainCameraPosition.x / 60;
            float y = 0;

            if (mainCameraPosition.x < 0)
            {
                x = 0;
            }
            else if(mainCameraPosition.x > mainCameraXMax)
            {
                x = endOffset;
            }
     
            //移動量
            offset = new Vector2(x, y);
            //Debug.Log("offset値：" + offset);
            m_material.SetTextureOffset(k_propName, offset);
        }
    }

    private void OnDestroy()
    {
        //ゲームをやめた後にマテリアルのOffsetを戻しておく
        if(m_material)
        {
            m_material.SetTextureOffset(k_propName, Vector2.zero);
        }
    }
}
