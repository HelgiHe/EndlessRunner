using UnityEngine;
using System.Collections;

public class Sett : MonoBehaviour {

	public static Sett sett;
	// ef það er fleiri objectar með Singleton scriptuna er þeim eytt;

	public bool mute;

	void Awake ()
	{
		if(sett == null)
		{
			DontDestroyOnLoad(gameObject);
			sett = this;
		}
		else if(sett != this)
		{
			Destroy(gameObject);
		}
	}

	public void toggle () {
		mute = !mute;
	}
}
