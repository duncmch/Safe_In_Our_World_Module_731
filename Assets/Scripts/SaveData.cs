using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct SaveData
{
    public static SaveData Instance;

    //map stuff
    public HashSet<string> sceneNames;

    //bench stuff
    public string benchSceneName;
    public Vector2 benchPos;

    //player stuff
    public int playerHealth;
    public float playerMana;
    public Vector2 playerPosition;
    public string lastScene;

    public bool playerUnlockedWallJump, playerUnlockedDash, playerUnlockedVarJump;

    public bool playerUnlockedSideCast, playerUnlockedUpCast, playerUnlockedDownCast;

    public void Initialize()
    {
        if(!File.Exists(Application.persistentDataPath + "/save.bench.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.bench.data"));
        }

        if (!File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.player.data"));
        }

        if (sceneNames == null)
        {
            sceneNames = new HashSet<string>();
        }
    }

    public void SaveBench()
    {
        using(BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.bench.data")))
        {
            writer.Write(benchSceneName);
            writer.Write(benchPos.x);
            writer.Write(benchPos.y);
        }
    }

    public void LoadBench()
    {
        if(File.Exists(Application.persistentDataPath + "/save.bench.data"))
        {
            using(BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.bench.data")))
            {
                benchSceneName = reader.ReadString();
                benchPos.x = reader.ReadSingle();
                benchPos.y = reader.ReadSingle();
            }
        }
    }

    public void SavePlayerData()
    {
        using(BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.player.data")))
        {
            playerHealth = PlayerController.Instance.Health;
            writer.Write(playerHealth);
            playerMana = PlayerController.Instance.Mana;
            writer.Write(playerMana);

            playerUnlockedWallJump = PlayerController.Instance.unlockWallJump;
            writer.Write(playerUnlockedWallJump);
            playerUnlockedDash = PlayerController.Instance.unlockDash;
            writer.Write(playerUnlockedDash);
            playerUnlockedVarJump = PlayerController.Instance.unlockVarJump;
            writer.Write(playerUnlockedVarJump);

            playerUnlockedSideCast = PlayerController.Instance.unlockSideCast;
            writer.Write(playerUnlockedSideCast);
            playerUnlockedUpCast = PlayerController.Instance.unlockUpCast;
            writer.Write(playerUnlockedUpCast);
            playerUnlockedDownCast = PlayerController.Instance.unlockDownCast;
            writer.Write(playerUnlockedDownCast);

            playerPosition = PlayerController.Instance.transform.position;
            writer.Write(playerPosition.x);
            writer.Write(playerPosition.y);

            lastScene = SceneManager.GetActiveScene().name;
            writer.Write(lastScene);
        }
    }

    public void LoadPlayerData()
    {
        if(File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.player.data")))
            {
                playerHealth = reader.ReadInt32();
                playerMana = reader.ReadSingle();

                playerUnlockedWallJump = reader.ReadBoolean();
                playerUnlockedDash= reader.ReadBoolean();
                playerUnlockedVarJump= reader.ReadBoolean();

                playerUnlockedSideCast = reader.ReadBoolean();
                playerUnlockedUpCast = reader.ReadBoolean();
                playerUnlockedDownCast = reader.ReadBoolean();

                playerPosition.x = reader.ReadSingle();
                playerPosition.y = reader.ReadSingle();
                
                lastScene = reader.ReadString();

                SceneManager.LoadScene(lastScene);
                PlayerController.Instance.transform.position = playerPosition;
                PlayerController.Instance.Health = playerHealth;
                PlayerController.Instance.Mana = playerMana;

                PlayerController.Instance.unlockWallJump = playerUnlockedWallJump;
                PlayerController.Instance.unlockDash = playerUnlockedDash;
                PlayerController.Instance.unlockVarJump = playerUnlockedVarJump;

                PlayerController.Instance.unlockSideCast = playerUnlockedSideCast;
                PlayerController.Instance.unlockUpCast = playerUnlockedUpCast;
                PlayerController.Instance.unlockDownCast = playerUnlockedDownCast;
            }
        }
        else
        {
            Debug.Log("File doesnt exist");
            PlayerController.Instance.Health = PlayerController.Instance.maxHealth;
            PlayerController.Instance.Mana = 0.5f;

            PlayerController.Instance.unlockWallJump = false;
            PlayerController.Instance.unlockDash = false;
            PlayerController.Instance.unlockVarJump = false;
        }
    }
}
