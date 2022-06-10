using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;
    public GameObject player;
    public FloatingTextManager floatingTextManager;
    public List<GameObject> prizes = new List<GameObject>();
    public int maxHealth;

    private int weaponDamage;
    private int coins;
    private int health_of_player;
    private Player playerScript;
    private Menu menuScript;
    private Health health;

    private const string saveKey = "SaveData_0";

    private void Update() {
        if (player == null) {
            player = GameObject.Find("Player");
            Load();
        }
        menuScript.ChangeCoinsInMenu(coins);
        if (health == null) {
            health = GameObject.Find("Anchor_Hearts").GetComponent<Health>();
        }
        health.ChangeSprite(health_of_player, maxHealth);
        if (floatingTextManager == null) {
            floatingTextManager = GameObject.Find("Anchor_Hearts").GetComponent<FloatingTextManager>();
        }
    }



    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        health_of_player = maxHealth;
        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        //Invoke("Load", 1f);
        Load();
        playerScript = player.GetComponent<Player>();
        health = GameObject.Find("Anchor_Hearts").GetComponent<Health>();
        menuScript = GameObject.Find("Menu").GetComponent<Menu>();
        weaponDamage = playerScript.weapon.GetComponent<Weapon>().damage;
        
    }

    public void Save() {
        SaveManager.Save(saveKey, GetSave());
    }

    public void Load() {
        var data = SaveManager.Load<SaveData.PlayerEquipmentData>(saveKey);
        coins = data._coins;
        //Debug.Log("coins: " + coins + ", and health: " + data._health);
        if (data._coins > 0)
            health_of_player = data._health;
        /*if (menuScript.cellsForExtra == null) {
            Debug.Log("menuScript.cellsForExtra == null");
        }
        if (data._cellsForExtra.Capacity != 0)
            menuScript.cellsForExtra = data._cellsForExtra;
        if (data._cellsForWeapon.Capacity != 0)
            menuScript.cellsForWeapon = data._cellsForWeapon; */
    }

    private SaveData.PlayerEquipmentData GetSave() {
        var data = new SaveData.PlayerEquipmentData() {
            _coins = this.coins,
            _health = this.health_of_player,
            //_cellsForWeapon = menuScript.cellsForWeapon,
            //_cellsForExtra = menuScript.cellsForExtra
        };
        return data;
    }

   
    /*public void SaveState() {
        //Debug.Log("Save state");

        string s = "";
        s += coins.ToString() + "|";
        s += playerScript.health.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        coins = int.Parse(data[0]);
        playerScript.health = int.Parse(data[1]);
        //SceneManager.sceneLoaded -= LoadState;
        Debug.Log("Load state");
    } */

    public string GetNameOfMainWeapon() {
        return playerScript.weapon.transform.name;
    }

    public int GetWeaponDamage() {
        return weaponDamage;
    }

    public string GetMainWeaponName() {
        return menuScript.mainWeaponSprite.GetComponent<Image>().sprite.name;
    }

    public void ChangeWeapon(string weaponName) {

        if (player == null) {
            player = GameObject.Find("Player");
            playerScript = player.GetComponent<Player>();
        }

        if (playerScript.weapon != null) {
            Destroy(playerScript.weapon.gameObject);
        }
        playerScript.weapon = Instantiate(Items.items.GetWeaponItem(weaponName));
        
        playerScript.weapon.transform.SetParent(player.transform, false);
        weaponDamage = playerScript.weapon.GetComponent<Weapon>().damage;
        menuScript.ChangeDamageAmountInMenu(weaponDamage);
        //playerScript.weapon.transform.localScale = playerScript.weaponPosition;
    }

    public bool GetWeapon(string name) {
        return menuScript.SetItem(name);
    }

    public bool GetExtra(string name) {
        return menuScript.SetItemExtra(name);
    }

    public bool GetTaskItem(string name) {
        return menuScript.SetTaskItem(name);
    }

    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    public void ChangeCoins(int amount) {
        coins += amount;
        menuScript.ChangeCoinsInMenu(coins);
    }

    public void GetPrize(Transform transform) {
        Vector3 pos = transform.position;
        int value = Random.Range(0, prizes.Count);
        GameObject obj = Instantiate(prizes[value]);
        obj.transform.position = pos;
    }
    public void ChangeHealth(int points) {
        health_of_player += points;
        //Debug.Log("playerScript.health: " + playerScript.health + " + points: " + points);
        if (health_of_player >= maxHealth) {
            //Debug.Log("148: playerScript.health = playerScript.maxHealth;");
            health_of_player = maxHealth;
        }
        if (health_of_player <= 0) {
            //instance.SaveState();
            //Invoke("RestartLevel", 2f);
            RestartLevel();
            
            return;
        }
        health.ChangeSprite(health_of_player, maxHealth);
        //Debug.Log("healths: " + playerScript.health);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
