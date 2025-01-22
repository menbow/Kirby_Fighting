using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private const float k_maxLength = 1f;
    //画像データを取得：Unity専用命令っぽい
    private const string k_propName = "_MainTex";

    [SerializeField]
    private Vector2 m_offsetSpeed;

    [SerializeField]
    private GameObject mainCamera;

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
        //プレイヤーの座標取得
        Vector2 mainCameraPosition = mainCamera.transform.position;
        Debug.Log("MainCameraPosition:"+ mainCameraPosition);

        if(m_material)
        {
            //xとyの値が0～1でリピートするようにする
            //var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
            //var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);

            //プレイヤーが動いた分だけ背景スクロール
            float x = mainCameraPosition.x / 60;
            float y = 0;

            Vector2 offset = new Vector2(x, y);
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
