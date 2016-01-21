using UnityEngine;
using System.Collections;

public class DoNotDestroy : MonoBehaviour {

    public GameObject doNotDestroyThis;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(doNotDestroyThis);
	}

}
