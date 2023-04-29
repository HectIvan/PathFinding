using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject m_prefab;
    public MeshRenderer m_MR;
    public float m_posX = 0f;
    public float m_posZ = 0f;
    public bool m_impassable;
    public bool m_pathMember;

    public Material m_impassableMat;
    public Material m_neutralMat;
    public Material m_pathMat;

    public Vector2 m_left       ;//= new Vector2(0,0);
    public Vector2 m_right      ;//= new Vector2(0,0);
    public Vector2 m_up         ;//= new Vector2(0,0);
    public Vector2 m_down       ;//= new Vector2(0,0);

    public Box()
    {       
        m_posX = 0f;
        m_posZ = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.AddComponent<Box>();
        //m_MR = this.GetComponent<MeshRenderer>();
        m_left.x = m_posX - 1;
        m_left.y = m_posZ;

        m_right.x = m_posX + 1;
        m_right.y = m_posZ;

        m_up.x = m_posX;
        m_up.y = m_posZ + 1;

        m_down.x = m_posX;
        m_down.y = m_posZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_pathMember)
        {
            //this.GetComponent<MeshRenderer>().material = m_pathMat;
        }
        if (m_impassable)
        {
            //this.GetComponent<MeshRenderer>().material = m_impassableMat;
        }
    }
}
