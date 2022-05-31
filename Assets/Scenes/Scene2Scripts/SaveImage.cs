using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class SaveImage : MonoBehaviour
{
    [Tooltip("½ØÍ¼´æ´¢Â·¾¶")]
    public string path;
    public bool capture;
    public static int cnt = 0;

    public RawImage rawImage;
    Texture2D tex;

    private struct ImageDate
    {
        public long timestamp;
        public byte[] data;
    }

    // Start is called before the first frame update
    void Start()
    {
        capture = false;
        path = Application.dataPath + "/Saved_Picture/";
        //path = Application.streamingAssetsPath + "/Pictures/";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            InputSave();
    }

    public void InputSave()
    {
        if (!capture)
        {
            capture = true;
            StartCoroutine(CaptrueAndSave());
            cnt++;
            Debug.Log("good");
        }
    }

    public IEnumerator CaptrueAndSave()
    {
        if(capture)
        {
            yield return new WaitForEndOfFrame();
            tex = TextureToTexture2D(rawImage.texture);
            Debug.Log(rawImage.texture.name);

            tex.Apply();
            ImageDate pack;
            pack.data = tex.EncodeToPNG();

            pack.timestamp = cnt;
            FileStream fs = File.Create(path + pack.timestamp + ".tif");
            fs.Write(pack.data, 0, pack.data.Length);
            fs.Close();
            capture = false;

        }
    }

    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture2D;
    }
}
