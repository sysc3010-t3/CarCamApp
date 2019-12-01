using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;

using Android.Hardware.Input;
using Android.Content;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace CarCamApp.Droid
{
    [Activity(Label = "CarCamApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, InputManager.IInputDeviceListener
    {
        private InputManager input_manager;
        private ConcurrentQueue<string> cq;
        private int current_device_id = -1;

        public static Axis AXIS_X = MotionEvent.AxisFromString("AXIS_X");
        public static Axis AXIS_Y = MotionEvent.AxisFromString("AXIS_Y");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            cq = new ConcurrentQueue<string>();

            var task = new Task(() => StartUdpWorker(),
                    TaskCreationOptions.LongRunning);
            task.Start();

            input_manager = (InputManager)GetSystemService(Context.InputService);
            CheckGameControllers();
        }

        private void StartUdpWorker()
        {
            UdpClient client = new UdpClient();
            client.Connect("192.168.0.25", 5005);
            Byte[] currentMessage = null;
            string s;
            while (true) {
                if (!cq.TryDequeue(out s))
                {
                    if (currentMessage != null)
                    {
                        client.Send(currentMessage, currentMessage.Length);
                    }
                }
                else
                {
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(s);
                    client.Send(sendBytes, sendBytes.Length);
                    currentMessage = sendBytes;
                    cq = new ConcurrentQueue<string>();
                }
                Thread.Sleep(100);
            }
        }

        //Check for any connected game controllers
        private void CheckGameControllers()
        {
            int[] deviceIds = input_manager.GetInputDeviceIds();
            foreach (int deviceId in deviceIds)
            {
                InputDevice dev = InputDevice.GetDevice(deviceId);
                int sources = (int)dev.Sources;

                if (((sources & (int)InputSourceType.Gamepad) == (int)InputSourceType.Gamepad) ||
                    ((sources & (int)InputSourceType.Joystick) == (int)InputSourceType.Joystick))
                {
                    if (current_device_id != deviceId)
                    {
                        current_device_id = deviceId;
                    }
                }
            }
        }

        public void OnInputDeviceAdded(int deviceId)
        {
        }

        public void OnInputDeviceRemoved(int deviceId)
        {
        }

        public void OnInputDeviceChanged(int deviceId)
        {
        }

        public override bool OnGenericMotionEvent(MotionEvent e)
        {
            InputDevice device = e.Device;
            if (device != null && device.Id == current_device_id)
            {
                int x = Convert.ToInt32(e.GetX(0) * 511.5 + 511.5);
                int y = Convert.ToInt32(-1 * e.GetY(0) * 511.5 + 511.5);
                cq.Enqueue(String.Format("{{\"type\":8,\"x\":{0},\"y\":{1}}}", x, y));
                return true;
            }
            return base.OnGenericMotionEvent(e);
        }

        private bool IsGamepad(InputDevice device)
        {
            if ((device.Sources & InputSourceType.Gamepad) == InputSourceType.Gamepad ||
               (device.Sources & InputSourceType.ClassJoystick) == InputSourceType.Joystick)
            {
                return true;
            }
            return false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}