using UnityEngine;

public class FakeException : MonoBehaviour
{
   public void ThrowException()
   {
      throw new System.NotImplementedException();
   }
}
