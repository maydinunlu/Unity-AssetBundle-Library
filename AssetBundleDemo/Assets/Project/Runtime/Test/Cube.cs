using UnityEngine;

namespace AssetBundleDemo
{
    public class Cube : MonoBehaviour
    {
        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
        }
    }
}