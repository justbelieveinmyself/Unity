using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Collections;
public class Shooting : MonoBehaviour
{
    public Image reloadCircle;
    private float reload = 5;
    private int intReload;
    public GameObject gun;
    public GameObject turret;
    public GameObject bull;
    public GameObject target;
    private GameObject[] bullet;
    private static Vector3 inputSpeed = new Vector3(0,0,0);
    private Vector3 accel = Vector3.zero;
    private Vector3 speed = Vector3.zero;
    private float determSpeed = 1000f;
    private bool lkm = false;
    private bool start = false;
    private bool fstart = false;
    public float coordinat_y = 0;
    private float coordinat_x = 0, coordinat_z = 0, scale_xz_to_y = 130;
    public AudioClip shootRightNow;
    public Vector3 set_input_for_x_y(Vector3 inputV){ // передаю направление башни
        coordinat_x = Mathf.Sin(inputV.y * Mathf.PI / 180.0f) ; //перевожу это в значения от -1 до 1
        if (gun.transform.eulerAngles.x >= 330) coordinat_y =  ((360 - gun.transform.eulerAngles.x) + 10) / 6f;
        else coordinat_y = (gun.transform.eulerAngles.x - 10) / 80f;
        coordinat_z = Mathf.Cos(inputV.y * Mathf.PI / 180.0f) ; //для этого считаю радианы
        return inputSpeed = new Vector3(coordinat_x * scale_xz_to_y, coordinat_y, coordinat_z * scale_xz_to_y); //создаю начальный вектор скорости и передаю его
    }
    private void Start() {
         reloadCircle.enabled = false;
    }
    private void FixedUpdate()
    {
        reload+= Time.fixedDeltaTime; //перезарядка
        if(Input.GetKeyDown(KeyCode.Mouse0) && reload >=5) lkm = true; //отслеживания готовности танка стрелять при лкм
        else lkm = false; 
        if(lkm && fstart == false){ //проверяет первый ли старт и нажата ли лкм
            lkm = false; //выключаем для следующего выстрела
            start = true; //включаем для совершения выстрела
            fstart = true; //включаем чтоб пользователь не стрельнул пока еще идут расчеты для предыдущего
            reload = 0; //обнуляем перезарядку
            StartCoroutine(Reload()); //куратина с перезарядкой (показывает игроку состояние перезарядки
            Instantiate(bull, target.transform.position, target.transform.rotation); //создает снаряд
            AudioSource.PlayClipAtPoint(shootRightNow, target.transform.position); //проигрывает звук выстрела
        }
        if (start){ //начался выстрел
            if (fstart) { //для выстрела нужна начальная скорость
                speed = set_input_for_x_y(turret.transform.eulerAngles); //вызываем функцию, передаем направление в которое смотрит дуло
                fstart = false; //больше не нужна начальная скорость
            }
            bullet = GameObject.FindGameObjectsWithTag("bullet"); //нахожу объекты с тэгом снаряд и помещаю в массив
            accel = Physics.gravity - ((speed.magnitude * Physics.gravity.magnitude) / Mathf.Pow(determSpeed, 2)) * speed; //считаю ускорение по нашей формуле
            speed = speed + accel * Time.fixedDeltaTime; //считаю скорость
            for (int i = 0; i < bullet.Length; i++){ //для просчета позиции каждой пули
            Destroy(bullet[i], 4f);   //удаляем пулю через 4 секунды
            bullet[i].transform.position = bullet[i].transform.position + speed * Time.fixedDeltaTime; //считаем позицию
            }  
        }
    }
    private IEnumerator Reload(){
        reloadCircle.enabled = true;
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(reload <= 5) {
            reloadCircle.fillAmount = reload / 5;
            yield return null;
        }
        reloadCircle.enabled = false;
   }
}
