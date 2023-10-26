using UnityEngine;
using TMPro;

public class UIBehavior : MonoBehaviour
{
    public static UIBehavior InstanceUIBehavior;

    [SerializeField] private TextMeshProUGUI _textPlayerHP;
    [SerializeField] private TextMeshProUGUI _killedEnemy;

    [SerializeField] private GameObject _showLossPanel;
    [SerializeField] private GameObject _showWinPanel;
    [SerializeField] private GameObject _aimImg;
    [SerializeField] private GameObject _hitFolder;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _openBoxX;
    [SerializeField] private TextMeshProUGUI _amountBullet;
    [SerializeField] private GameObject _minimap;
    [SerializeField] private GameObject _playerHP;
    [SerializeField] private GameObject _amountBulletObj;
    [SerializeField] private GameObject _mouse;

    private TasksGame _tasksGame;
    private MoveCameraWithMouse _moveCameraWithMouse;
    private ParticleSystemWin _particle;

    private PlayerBehavior _playerBehavior;
    private SimpleShoot _simpleShoot;
    [HideInInspector] public int _killed = 0;

    private void Awake()
    {
        InstanceUIBehavior = this;
    }

    private void Start()
    {
        GlobalEventManager.OnEnemyKilled += EnemyKilled;

        _playerBehavior = PlayerBehavior.PlayerBehaviorInstance;
        _particle = ParticleSystemWin.ParticleSystemWinInstance;
        _moveCameraWithMouse = MoveCameraWithMouse.MoveCameraInstance;
        _simpleShoot = SimpleShoot.SimpleShootInstance;
        _tasksGame = TasksGame.InstanceTask;
    }

    private void Update()
    {
        PlayerHP();
        EnemyKilled();
        _tasksGame.Item();
        AmountBullet();
    }

    private void PlayerHP()
    {
        _textPlayerHP.text = _playerBehavior.PlayerHealth.ToString();
    }

    private void AmountBullet()
    {
        _amountBullet.text = _simpleShoot._amountBullet.ToString();
    }
    private void EnemyKilled()
    {
        _killedEnemy.text = "Killed enemy: " + _killed;
    }

    public void ScreenWin()
    {
        _showWinPanel.SetActive(true);
        _playerBehavior.EndAnimation();
        _particle.PlayParticle();
        _playerBehavior.GetComponent<PlayerBehavior>().enabled = false;
        _simpleShoot.GetComponent<SimpleShoot>().enabled = false;

    }

    public void ScreenLoss()
    {
        _showLossPanel.SetActive(true);
        _aimImg.SetActive(false);
        _playerBehavior.EndAnimation();
        _minimap.SetActive(false);
        //_playerHP.SetActive(false);
        _amountBulletObj.SetActive(false);
        _mouse.SetActive(false);

        _moveCameraWithMouse.GetComponent<MoveCameraWithMouse>().enabled = false;
        _playerBehavior.GetComponent<PlayerBehavior>().enabled = false;
        _simpleShoot.GetComponent<SimpleShoot>().enabled = false;
    }

    public void WarringHealth()
    {
        _hitFolder.SetActive(true);
    }

    public void OffWarringHealth()
    {
        _hitFolder.SetActive(false);
    }

    public void ActiveInventory()
    {
        if (_inventory.activeSelf)
        {
            _inventory.SetActive(false);
            _simpleShoot.GetComponent<SimpleShoot>().enabled = true;
            _moveCameraWithMouse.GetComponent<MoveCameraWithMouse>().enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            _inventory.SetActive(true);
            _simpleShoot.GetComponent<SimpleShoot>().enabled = false;
            _moveCameraWithMouse.GetComponent<MoveCameraWithMouse>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

    public void VisibleBoxLetter_Key()
    {
        _openBoxX.SetActive(true);
    }

    public void InvisibleBoxLettert_Key()
    {
        _openBoxX.SetActive(false);
    }
}
