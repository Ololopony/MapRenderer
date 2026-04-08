using System.Collections;
using UnityEngine;

public class UIShowCorutine : MonoBehaviour
{
    public IEnumerator ShowIU(GameObject UIToShow, int timeToShow)
    {
        UIToShow.SetActive(true);
        yield return new WaitForSeconds(timeToShow);
        UIToShow.SetActive(false);
    }
}
