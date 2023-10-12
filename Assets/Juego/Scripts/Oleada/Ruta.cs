using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class Ruta : MMPath
{
    //Para que esta clase funcione, la ruta debe tener cycle option only once
    public bool FinRuta(){
        return _endReached;
    }
}
