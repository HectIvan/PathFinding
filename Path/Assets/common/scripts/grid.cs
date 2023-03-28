using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public GameObject m_gridBox;
    public int m_X;
    public int m_Y;

    public int m_objectiveX;
    public int m_objectiveZ;

    public int m_originX;
    public int m_originZ;

    //materials
    public Material m_originMat;
    public Material m_objectiveMat;
    public Material m_impassableMat;
    public Material m_routeMat;
    public Material m_neutralMat;

    //list
    public static List<GameObject> m_gridBoxInstances = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGrid();
        Search();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateGrid( )
    {
        int counter = 0;
        for (int i = 0; i < m_Y; ++i)
        {
            int xCounter = 0;
            for (int j = 0; j < m_X; ++j)
            {
                if (m_objectiveX == j && m_objectiveZ == i)
                {

                    GameObject gridMesh = Instantiate(m_gridBox, new Vector3(transform.position.x + xCounter, transform.position.y, transform.position.z + counter), Quaternion.identity);
                    gridMesh.GetComponent<MeshRenderer>().material = m_objectiveMat;
                    m_gridBoxInstances.Add(gridMesh);
                }
                else if (m_originX == j && m_originZ == i)
                {
                    GameObject gridMesh = Instantiate(m_gridBox, new Vector3(transform.position.x + xCounter, transform.position.y, transform.position.z + counter), Quaternion.identity);
                    gridMesh.GetComponent<MeshRenderer>().material = m_originMat;
                    m_gridBoxInstances.Add(gridMesh);
                }
                else
                {
                    GameObject gridMesh = Instantiate(m_gridBox, new Vector3(transform.position.x + xCounter, transform.position.y, transform.position.z + counter), Quaternion.identity);
                    gridMesh.GetComponent<MeshRenderer>().material = m_neutralMat;
                    m_gridBoxInstances.Add(gridMesh);
                }
                xCounter += 1;
            }
            counter += 1;
        }
    }

    void Search()
    {
        int searchX = 0;
        int searchZ = 0;
        bool foundX = false;
        bool foundZ = false;
        for (int i = 0; i < m_gridBoxInstances.Count; ++i)
        {
            if (searchX == m_originX && !foundX)
            {
                foundX = true;
                Debug.Log("X position: " + searchX);
            }
            if (searchZ == m_originZ && !foundZ)
            {
                foundZ = true;
                Debug.Log("Z position: " + searchZ);
            }
            ++searchX;
            if (searchX > m_X - 1)
            {
                searchX = 0;
                searchZ += 1;
            }
        }
    }
}
