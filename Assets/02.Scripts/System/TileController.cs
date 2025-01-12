using UnityEngine;

/// <summary>
/// Player 이동에 따라 타일(3x3) 상하좌우로 위치 이동
/// </summary>
public class TileController : MonoBehaviour
{
    /// <summary>
    /// <br>타일 배열</br>
    /// <br>0 3 6</br>
    /// <br>1 4 7</br>
    /// <br>2 5 8</br>
    /// </summary>
    [SerializeField] GameObject[] tiles = new GameObject[9];
    /// <summary>
    /// 타일 한 칸 크기 (x, y축 동일한 크기)
    /// </summary>
    [SerializeField] int tileSize = 20;

    GameObject CenterTile;

    private void Awake()
    {
        CenterTile = tiles[4];
    }

    private void Update()
    {
        if (GameManager.Instance.IsPause) return;

        Vector3 CenterTilePos = CenterTile.transform.position;
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        float diffX = Mathf.Abs(CenterTilePos.x - playerPos.x);
        float diffY = Mathf.Abs(CenterTilePos.y - playerPos.y);

        if(diffX * 1.3 >= tileSize)
        {
            if(CenterTilePos.x < playerPos.x)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        if(diffY * 1.3 >= tileSize)
        {
            if(CenterTilePos.y < playerPos.y)
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }
    }

    /// <summary>
    /// 타일 왼쪽으로 이동
    /// <br>X 0 3 6 => 6 0 3 X</br>
    /// <br>X 1 4 7 => 7 1 4 X</br>
    /// <br>X 2 5 8 => 8 2 5 X</br>
    /// </summary>
    private void MoveLeft()
    {
        // 6, 7, 8 타일 왼쪽으로 이동
        for (int i = 6; i < 9; ++i)
        {
            Vector3 tilePos = tiles[i].transform.position;
            tiles[i].transform.position = new Vector3 (tilePos.x - tileSize * 3, tilePos.y, tilePos.z);
        }

        //타일 위치에 맞춰서 배열 순서 재설정
        GameObject temp0 = tiles[0], temp1 = tiles[1], temp2 = tiles[2];
        GameObject temp3 = tiles[3], temp4 = tiles[4], temp5 = tiles[5];
        GameObject temp6 = tiles[6], temp7 = tiles[7], temp8 = tiles[8];

        tiles[0] = temp6; tiles[3] = temp0; tiles[6] = temp3;
        tiles[1] = temp7; tiles[4] = temp1; tiles[7] = temp4;
        tiles[2] = temp8; tiles[5] = temp2; tiles[8] = temp5;

        CenterTile = tiles[4];
    }

    /// <summary>
    /// 타일 오른쪽으로 이동
    /// <br>0 3 6 X => X 3 6 0</br>
    /// <br>1 4 7 X => X 4 7 1</br>
    /// <br>2 5 8 X => X 5 8 2</br>
    /// </summary>
    private void MoveRight()
    {
        // 0, 1, 2 타일 오른쪽으로 이동
        for (int i = 0; i < 3; ++i)
        {
            Vector3 tilePos = tiles[i].transform.position;
            tiles[i].transform.position = new Vector3(tilePos.x + tileSize * 3, tilePos.y, tilePos.z);
        }

        //타일 위치에 맞춰서 배열 순서 재설정
        GameObject temp0 = tiles[0], temp1 = tiles[1], temp2 = tiles[2];
        GameObject temp3 = tiles[3], temp4 = tiles[4], temp5 = tiles[5];
        GameObject temp6 = tiles[6], temp7 = tiles[7], temp8 = tiles[8];

        tiles[0] = temp3; tiles[3] = temp6; tiles[6] = temp0;
        tiles[1] = temp4; tiles[4] = temp7; tiles[7] = temp1;
        tiles[2] = temp5; tiles[5] = temp8; tiles[8] = temp2;

        CenterTile = tiles[4];
    }

    /// <summary>
    /// 타일 위으로 이동
    /// <br>X X X => 2 5 8</br>
    /// <br>0 3 6 => 0 3 6</br>
    /// <br>1 4 7 => 1 4 7</br>
    /// <br>2 5 8 => X X X</br>
    /// </summary>
    private void MoveUp()
    {
        // 2, 5, 8 타일 위로 이동
        for (int i = 0; i < 3; ++i)
        {
            Vector3 tilePos = tiles[i * 3 + 2].transform.position;
            tiles[i * 3 + 2].transform.position = new Vector3(tilePos.x, tilePos.y + tileSize * 3, tilePos.z);
        }

        //타일 위치에 맞춰서 배열 순서 재설정
        GameObject temp0 = tiles[0], temp1 = tiles[1], temp2 = tiles[2];
        GameObject temp3 = tiles[3], temp4 = tiles[4], temp5 = tiles[5];
        GameObject temp6 = tiles[6], temp7 = tiles[7], temp8 = tiles[8];

        tiles[0] = temp2; tiles[3] = temp5; tiles[6] = temp8;
        tiles[1] = temp0; tiles[4] = temp3; tiles[7] = temp6;
        tiles[2] = temp1; tiles[5] = temp4; tiles[8] = temp7;

        CenterTile = tiles[4];
    }

    /// <summary>
    /// 타일 밑으로 이동
    /// <br>0 3 6 => X X X</br>
    /// <br>1 4 7 => 1 4 7</br>
    /// <br>2 5 8 => 2 5 8</br>
    /// <br>X X X => 0 3 6</br>
    /// </summary>
    private void MoveDown()
    {
        // 0, 3, 6 타일 아래로 이동
        for (int i = 0; i < 3; ++i)
        {
            Vector3 tilePos = tiles[i * 3].transform.position;
            tiles[i * 3].transform.position = new Vector3(tilePos.x, tilePos.y - tileSize * 3, tilePos.z);
        }

        //타일 위치에 맞춰서 배열 순서 재설정
        GameObject temp0 = tiles[0], temp1 = tiles[1], temp2 = tiles[2];
        GameObject temp3 = tiles[3], temp4 = tiles[4], temp5 = tiles[5];
        GameObject temp6 = tiles[6], temp7 = tiles[7], temp8 = tiles[8];

        tiles[0] = temp1; tiles[3] = temp4; tiles[6] = temp7;
        tiles[1] = temp2; tiles[4] = temp5; tiles[7] = temp8;
        tiles[2] = temp0; tiles[5] = temp3; tiles[8] = temp6;

        CenterTile = tiles[4];
    }
}
