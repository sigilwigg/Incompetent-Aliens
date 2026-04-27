using UnityEngine;

public static class FindTransform
{
    public static Transform FindChildNamed(this Transform transform, string name)
    {
        if (transform.name == name) return transform;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform foundChild = transform.GetChild(i).FindChildNamed(name);
            if (foundChild != null) return foundChild;
        }
        return null;
    }
}