using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour
{

	public void OpenFeedbackForm()
	{
		#if !UNITY_EDITOR
		openWindow("https://forms.gle/dytJxvUPhsKFwJA97");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}