using ConstraintThingy;
using UnityEngine;
using System.Collections;

public abstract class GUIConstraint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void Apply(FiniteDomainVariable<ContentType>[] rooms);
}
