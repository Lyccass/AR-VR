using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeBlink : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
     Mesh skinnedMesh;
     int blendShapeCount;

     int playindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
        skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
        blendShapeCount = skinnedMesh.blendShapeCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(playindex >0)  skinnedMeshRenderer.SetBlendShapeWeight (playindex -1, 0f);
          if(playindex == 0) skinnedMeshRenderer.SetBlendShapeWeight (blendShapeCount -1, 0f);
        skinnedMeshRenderer.SetBlendShapeWeight (playindex, 100f);
        playindex++;
        if(playindex > blendShapeCount -1) playindex =0;
    }
}
