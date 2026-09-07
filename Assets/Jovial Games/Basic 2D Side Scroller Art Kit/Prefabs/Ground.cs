using UnityEngine;


public class Ground : MonoBehaviour
{
    [SerializeField] private Transform[] groundPieces;
    [SerializeField] private Transform bird;
    [SerializeField] private float recycleOffset = 5f; 

    private float pieceWidth;

    private void Start()
    {
        if (groundPieces.Length < 2) return;
        pieceWidth = Mathf.Abs(groundPieces[1].position.x - groundPieces[0].position.x);
    }

    private void Update()
    {
        foreach (Transform piece in groundPieces)
        {
            if (piece.position.x < bird.position.x - recycleOffset)
            {
                piece.position = new Vector3(GetFurthestX() + pieceWidth, piece.position.y, piece.position.z);
            }
        }
    }

    private float GetFurthestX()
    {
        float max = float.MinValue;
        foreach (Transform piece in groundPieces)
        {
            if (piece.position.x > max) max = piece.position.x;
        }
        return max;
    }
}