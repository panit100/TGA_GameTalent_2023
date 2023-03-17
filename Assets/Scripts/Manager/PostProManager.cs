using System.Collections;
using System.Collections.Generic;
using CCB.Utility;
using UnityEngine;

public class PostProManager : Singleton<PostProManager>
{
    [SerializeField] private Material ScreenSpaceMat;
 

    private int _nameID;
   protected override void InitAfterAwake()
           {
               _nameID = Shader.PropertyToID("_Size");
               ScreenSpaceMat.SetFloat(_nameID,0);
           }

   public void TransitionIn(float duration)
   {
       StartCoroutine(DoTransistionIn(duration));
   }
   public void TransitionOut(float duration)
   {
       StartCoroutine(DoTransistionOut(duration));
   }


   IEnumerator DoTransistionIn(float duration)
   {
       duration = Mathf.Clamp(duration*0.25f,1f,10);
       var t = 0f;
       var tempfloat = ScreenSpaceMat.GetFloat("_Size");
      
       while ((t/duration)<duration)
       {
           ScreenSpaceMat.SetFloat(_nameID,Mathf.Lerp(tempfloat,40f,t/duration));
           t += Time.deltaTime;
           yield return null;
       }
       yield return null;
   }
   IEnumerator DoTransistionOut(float duration)
   {
       duration = Mathf.Clamp(duration*0.1f,1f,4f);
       var t = 0f;
       var tempfloat = ScreenSpaceMat.GetFloat("_Size");
      
       while ((t/duration)<duration)
       {
           ScreenSpaceMat.SetFloat(_nameID,Mathf.Lerp(tempfloat,0,Mathf.Clamp01(t/duration)));
           t += Time.deltaTime;
           yield return null;
       }

       ScreenSpaceMat.SetFloat(_nameID, 0);
       yield return null;
   }
}
