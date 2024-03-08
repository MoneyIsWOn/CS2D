//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Grid : MonoBehaviour
//{
//    public LayerMask unwalkableMask;
//    public Vector2 gridWorldSize;
//    public float nodeRadius;
//    Node[,] grid;
//    float nodeDiameter;
//    int gridSizeX, gridSizeY;


//    private void Update()
//    {
//        nodeDiameter = nodeRadius * 2; // ����� ���� ����
//        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); // X�� �׸��� ũ�� ����
//        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); // Y�� �׸��� ũ�� ����
//        CreateGrid(); // �׸��� ���� �Լ� ȣ��
//    }


//    void CreateGrid()
//    {
//        grid = new Node[gridSizeX, gridSizeY]; // �׸��� �迭 �ʱ�ȭ
//        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2; // �׸����� ���� �Ʒ� �𼭸� ��ǥ ���

//        for (int x = 0; x < gridSizeX; x++) // X�� �ݺ�
//        {
//            for (int y = 0; y < gridSizeY; y++) // Y�� �ݺ�
//            {
//                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius); // ���� ����� ���� ��ǥ ���

//                // ���� ��尡 �ȱ� �������� ���θ� üũ�ϰ� �׸��忡 ��� �߰�
//                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius)); // ����� �ݰ� ���� ��ֹ��� �ִ��� Ȯ��
//                grid[x, y] = new Node(walkable, worldPoint); // ��� ���� �� �׸��忡 �߰�
//            }
//        }
//    }


//    private void OnDrawGizmos()
//    {

//        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0)); // �׸��� ������ ���̾� ���������� �׸�

//        if (grid != null) // �׸��尡 �����Ǿ����� Ȯ��
//        {
//            foreach (Node n in grid) // �׸����� ��� ��忡 ���� �ݺ�
//            {
//                Gizmos.color = (n.walkable) ? Color.white : Color.red; // �ȱ� ������ ���� ���, �׷��� ������ ���������� ����
//                Gizmos.DrawWireCube(new Vector3(n.worldPosition.x, n.worldPosition.y, 0), Vector3.one * (nodeDiameter - .1f)); // ��带 ť��� �׸�
//            }
//        }
//        //{
//        //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,gridWorldSize.y,0)); // �׸��� ������ ���̾� ���������� �׸�

//        //    if (grid != null) // �׸��尡 �����Ǿ����� Ȯ��
//        //    {
//        //        foreach (Node n in grid) // �׸����� ��� ��忡 ���� �ݺ�
//        //        {
//        //            Gizmos.color = (n.walkable) ? Color.white : Color.red; // �ȱ� ������ ���� ���, �׷��� ������ ���������� ����
//        //            Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f)); // ��带 ť��� �׸�
//        //        }
//        //    }

//    }
//}
