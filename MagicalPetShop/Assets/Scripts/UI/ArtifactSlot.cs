using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactSlot : MonoBehaviour
{
    [SerializeField]
    private Text name;
    [SerializeField]
    public Image image;
    [SerializeField]
    private Text count;

    public void SetArtifact(InventoryArtifact artifact) {
        if (artifact == null) Debug.Log("Artifact is null");
        if (artifact.artifact == null) Debug.Log("Artifact.artifact is null");
        if (artifact.artifact.name == null) Debug.Log("Artifact.artifact.name is null");
        if (this.name == null) Debug.Log("name is null");
        this.name.text = artifact.artifact.name;
        this.image.sprite = artifact.artifact.artwork;
        this.count.text = artifact.count.ToString();
    }
}
