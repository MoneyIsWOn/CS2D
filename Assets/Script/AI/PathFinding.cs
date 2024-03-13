using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Grid grid;
    public List<Node> npcpath;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        if(npcpath != null)
        {
            npcpath = null;
            Debug.Log("���� �־��� ��θ� �����մϴ�.");
        }

        Node startNode = grid.NodeFromWorldPoint(startPos); //���� ��� 
        //Debug.Log($"���� ��ġ ���:({startNode.gridX},{startNode.gridY})");

        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        //Debug.Log($"���� ��ġ ���:({targetNode.gridX},{targetNode.gridY})");

        List<Node> openSet = new List<Node>(); //�湮�ؾ��� ����
        List<Node> visited = new List<Node>(); //�湮�� ����

        openSet.Add(startNode); //�湮�ؾ��� ��帮��Ʈ �� ��ó���� ���� ��带 ����.

        while (openSet.Count > 0)
        {
            Node node = openSet[0]; //�湮�ؾ��� ��� �� ���� �� ó�� ��岨��.

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                { //���� ����� F�� < ���¸���Ʈ�� ��� ����� �� �Ǵ� ���� ����� f�� == ���¸���Ʈ�� f���� �����ϸ�
                    if (openSet[i].hCost < node.hCost)
                    { 
                        node = openSet[i];
                    }
                }
            }
            openSet.Remove(node);
            visited.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return; //��θ� �� ã�� ������ ����.

            }

            foreach (Node neighbour in grid.GetNeighbors(node)) //�̿� ��带 ���� Ȯ���ϸ鼭
            {
                //Debug.Log($"{node.gridX},{node.gridY}�� �̿� ��� {neighber.gridX},{neighber.gridY} �˻���");

                if (!neighbour.walkable || visited.Contains(neighbour)) //���� �̿� ��尡 �̵��� �� ���ų�, �̹� �湮�� ����� ����
                {
                    //Debug.Log($"{neighber.gridX},{neighber.gridY}�� ���� ����");
                    continue;
                }
                //Debug.Log($"�˻� ���� �̿����{neighber}���� G����: {node.gCost}");

                int newCostToNeighbor = node.gCost + GetDistance(node,neighbour);
                //Debug.Log($"���� ����� G�� :{node.gCost}");
                // Debug.Log($"���� �˻����� �̿����{neighber.gridX},{neighber.gridY}���� �Ÿ��� {GetDistance(node, neighber)}");
                
                if (newCostToNeighbor < neighbour.gCost || !openSet.Contains(neighbour))
                { //������ ���� Ž���� ���´�.. �̸��׸� 1�� ���⿡ ������ �ִµ� 7�� ���� ��带 Ž���� ������ ����.

                    neighbour.gCost = newCostToNeighbor;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;
                    //Debug.Log($"���� �˻����� �̿����({neighbour.gridX},{neighbour.gridY})���� ��ǥ�������� �Ÿ���� : {neighbour.gCost} + {neighbour.hCost}");
                    //Debug.Log($"���� �˻����� �̿������ �θ���� {neighbour.parent.gridX},{neighbour.parent.gridY}");

                   if(!openSet.Contains(neighbour)) //å���� �̺κ��� �����ִ�.
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    int GetDistance(Node A, Node B) //���� ���� ��ǥ ��尣�� �޸���ƽ
    {
        int dstX = Mathf.Abs(A.gridX - B.gridX);
        int dstY = Mathf.Abs(A.gridY - B.gridY);
        if (dstX > dstY)
        {
            //Debug.Log($"���� ���� �Ÿ� ����� �� {14 * dstY + 10 * (dstX - dstY)}");
            return 14 * dstY + 10 * (dstX - dstY);
        }
        // Debug.Log($"���� ���� �Ÿ� ����� �� {14 * dstX + 10 * (dstY - dstX)}");
        return 14 * dstX + 10 * (dstY - dstX);
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        npcpath = path;
    }
}
