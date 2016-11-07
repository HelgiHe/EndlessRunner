using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class Extensions {

	public static void SetTransparency(this UnityEngine.UI.Image p_image, float p_transparency)
	{
		if (p_image != null)
		{
			UnityEngine.Color __alpha = p_image.color;
			__alpha.a = p_transparency;
			p_image.color = __alpha;
		}
	}
}
