using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public gunScript gun;
    // Start is called before the first frame update
    public void GunScoped()
    {
        gun.Scoped();
    }
    public void reload()
    {
        gun.Reload();
    }
}
