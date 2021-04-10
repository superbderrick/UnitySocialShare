using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class SHARE_INFO
{
	public static string SUBJECT = "This is Unity native shareing sample ";
	public static string TEXT = "What a good day";
	public static string TARGETURL = "https://www.linkedin.com/in/sensational/";
}

public class ClickHandler : MonoBehaviour
{

    
    void Start()
    {

    }

    void Update()
    {
        
            
    }

    public void ShareSomething()
    {
		StartCoroutine(TakeScreenshotAndShare());
	}

	private string MakeSampleScreenShotImage()
    {
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		Destroy(ss);

		return filePath;

	}

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();


		string filePath = MakeSampleScreenShotImage();

		new NativeShare().AddFile(filePath)
			.SetSubject(SHARE_INFO.SUBJECT).SetText(SHARE_INFO.TEXT).SetUrl(SHARE_INFO.TARGETURL)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
	}


}
