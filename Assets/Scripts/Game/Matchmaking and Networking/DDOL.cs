using UnityEngine;

public class DDOL : MonoBehaviour
{
    //To make the gameobject not to be destroyed.
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
