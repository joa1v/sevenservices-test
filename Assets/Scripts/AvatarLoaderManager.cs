using ReadyPlayerMe;
using ReadyPlayerMe.Core;
using System;
using UnityEngine;

public class AvatarLoaderManager : MonoBehaviour
{
    [SerializeField] private string avatarUrl = "COLE_SEU_URL_AQUI";
    [SerializeField] private FaceReceiver faceReceiver;

    private void Start()
    {
        var loader = new AvatarObjectLoader();
        loader.OnCompleted += OnLoad;
        loader.LoadAvatar(avatarUrl);
    }

    private void OnLoad(object sender, CompletionEventArgs args)
    {
        var avatar = args.Avatar;
        Debug.Log("Avatar carregado: " + avatar.name);
        // Adicione dentro de OnCompleted:
        foreach (var smr in avatar.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (var mat in smr.materials)
            {
                var matName = mat.name.ToLower();

                if (matName.Contains("head"))
                {
                    Debug.Log("Aplicando textura da cabeça");
                    faceReceiver.ApplyFaceTexture(mat.mainTexture);
                }
                else if (matName.Contains("eye"))
                {
                    Debug.Log("Aplicando textura dos olhos");
                    faceReceiver.ApplyEyesTexture(mat.mainTexture);
                }
            }
        }

    }
}
