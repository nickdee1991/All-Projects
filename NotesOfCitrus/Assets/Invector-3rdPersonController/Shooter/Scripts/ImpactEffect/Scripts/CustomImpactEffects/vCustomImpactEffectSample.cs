using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vShooter
{
    [CreateAssetMenu(menuName = "Invector/Effects/New  Custom ImpactEffect", fileName = "CustomImpactEffect@")]
    public class vCustomImpactEffectSample : vImpactEffectBase
    {
        public float lineLenght=1;
        public float lineLifeSeconds = 2f;
        public float lineStartWidht = 0.1f;
        public float lineEndWidht = 0.01f;
        public Material material;
        public override void DoImpactEffect(Vector3 position, Quaternion rotation, GameObject sender, GameObject receiver)
        {
            GameObject go = new GameObject();
            go.transform.position = position;
            go.transform.rotation =Quaternion.LookRotation(sender.transform.forward);
            var lineRender = go.AddComponent<LineRenderer>();
            lineRender.startWidth = lineStartWidht;
            lineRender.endWidth = lineEndWidht;
            lineRender.useWorldSpace = false;
            lineRender.material = material;
            lineRender.numCapVertices = 10;
            lineRender.SetPositions(new Vector3[] { Vector3.zero, -Vector3.forward *lineLenght});
            Destroy(go, lineLifeSeconds);
            go.transform.SetParent(vObjectContainer.root,true);
           
        }
    }
}