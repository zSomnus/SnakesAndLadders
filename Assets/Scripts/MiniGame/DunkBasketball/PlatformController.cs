using UnityEngine;

namespace Assets
{
    public class PlatformController : MonoBehaviour
    {
        public bool GyroEnabled;
        public Gyroscope Gyro;

        private float xRotation;
        private float yRotation;

        // Start is called before the first frame update
        void Start()
        {
            GyroEnabled = EnableGyro();
        }

        private bool EnableGyro()
        {
            if (SystemInfo.supportsGyroscope)
            {
                Gyro = Input.gyro;
                Gyro.enabled = true;
                return true;
            }
            else
            {
                print("it doesn't support gyroscope");
            }

            return false;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward, -Input.GetAxis("Horizontal")*40*Time.deltaTime);
            transform.Rotate(Vector3.right, Input.GetAxis("Vertical")*40*Time.deltaTime);

            if (GyroEnabled)
            {
                yRotation += -Input.gyro.rotationRateUnbiased.y;
                xRotation += -Input.gyro.rotationRateUnbiased.x;
                transform.rotation = Quaternion.Euler(xRotation, 0, yRotation);
            }

            if (Input.anyKey)
            {
                ResetGyroscope();
            }
        }

        public void ResetGyroscope()
        {
            xRotation = 0;
            yRotation = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
