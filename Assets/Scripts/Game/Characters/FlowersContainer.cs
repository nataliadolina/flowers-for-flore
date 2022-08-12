using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersContainer : MonoBehaviour
{
    [SerializeField] private List<GameObject> flowerMeshes = null;
    private Transform[] flowerTransforms = null;

    [SerializeField] private float maxScore = 10f;
    private List<float> scoresNeeded;

    private int lastRotInd = 1;

    private void Awake()
    {
        scoresNeeded = new List<float>();
        flowerTransforms = GetComponentsInChildren<Transform>();

        SetScoreRange();
    }

    private void SetScoreRange()
    {
        float step = maxScore / flowerMeshes.Count;

        for (int i = 0; i < flowerMeshes.Count; i++)
        {
            scoresNeeded.Add(step * i);
        }
    }

    public void SetFlowerRotation(GameObject flower)
    {
        if (lastRotInd >= flowerTransforms.Length)
        {
            lastRotInd = 1;
        }

        Transform pos = flowerTransforms[lastRotInd];

        flower.transform.rotation = pos.rotation;
        flower.transform.position = pos.position;
        lastRotInd++;
    }

    public GameObject GetMesh(float score)
    {
        for (int i = 0; i < scoresNeeded.Count - 1; i++)
        {
            if (score >= scoresNeeded[i] & score < scoresNeeded[i + 1])
            {
                return flowerMeshes[i];
            }
        }

        return flowerMeshes[flowerMeshes.Count - 1];
    }
}
