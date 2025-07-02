using UnityEngine;

public class FaceReceiver : MonoBehaviour
{
    [SerializeField] private Renderer headRenderer;
    [SerializeField] private Renderer eyesRenderer;

    private void Reset()
    {
        headRenderer = GameObject.Find("Head")?.GetComponent<Renderer>();
        eyesRenderer = GameObject.Find("Eyes")?.GetComponent<Renderer>();
    }

    public void ApplyFaceTexture(Texture texture)
    {
        var mat = new Material(Shader.Find("Unlit/Texture"));
        mat.mainTexture = texture;
        headRenderer.material = mat;
    }

    public void ApplyEyesTexture(Texture texture)
    {
        var mat = new Material(Shader.Find("Unlit/Texture"));
        mat.mainTexture = texture;
        eyesRenderer.material = mat;
    }
}
