using System;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using WindowsInput;

namespace Arduino2Key
{
    class Arduino2Key
    {
        // Global vars
        static bool _blnContinue;
        static SerialPort _serialPort;
        static int intX1 = 0;
        static int intY1 = 0;
        static int intZ1 = 0;
        static int intX2 = 0;
        static int intY2 = 0;
        static int intZ2 = 0;

        static void Main(string[] args)
        {
            // Local vars
            string strCom = "COM6";
            string strRate = "9600";
            string strStop;
            string strTemp;
            Thread readThread = new Thread(ReadSerial);
            _serialPort = new SerialPort();

            // Set a default title and ask for a better one
            Console.Title = "Arduino2Key - #GGJ17";
            Console.Write("Give this thing a name: ");
            strTemp = Console.ReadLine();
            if (!string.IsNullOrEmpty(strTemp))
            {
                Console.Title = strTemp;
            }

            // Ask for port and baud
            Console.Write("Select active COM (COM6): ");
            strTemp = Console.ReadLine();
            if (!string.IsNullOrEmpty(strTemp))
            {
                strCom = strTemp;
            }
            Console.Write("Select baudrate (9600): ");
            strTemp = Console.ReadLine();
            if (!string.IsNullOrEmpty(strTemp))
            {
                strRate = strTemp;
            }

            // Try to open port
            Console.WriteLine("Opening " + strCom + " at " + strRate + " baud...");
            try
            {
                // Set serial settings
                _serialPort.PortName = strCom;
                _serialPort.BaudRate = int.Parse(strRate);
                _serialPort.StopBits = StopBits.One;
                _serialPort.Parity = Parity.None;
                _serialPort.Handshake = Handshake.None;

                // Open port
                _serialPort.Open();

                // Start reading in new thread
                _blnContinue = true;
                readThread.Start();

                // Give the possibility to quit the program
                Console.Write("Type STOP to exit: ");
                while (_blnContinue)
                {
                    strStop = Console.ReadLine();
                    if (strStop == "STOP")
                    {
                        _blnContinue = false;
                    }
                    else
                    {
                        HandleOtherInput(strStop);
                        Console.Write("Type STOP to exit: ");
                    }
                }
            }
            catch (Exception e)
            {
                // Oh noes =(
                Console.WriteLine("Error: " + e.Message);
            }

            // Cleanup before end of program
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            if (readThread.IsAlive)
            {
                readThread.Join();
            }
            Console.WriteLine("kthxbye");
            Console.ReadLine();
        }

        public static void ReadSerial()
        {
            while (_blnContinue)
            {
                // Try reading what comes in
                try
                {
                    string strMessage = _serialPort.ReadLine();
                    HandleInput(Regex.Replace(strMessage, @"\t|\n|\r", ""));
                }
                catch (Exception) {
                    // Ignore errors, because FY!
                }
            }
        }

        public static void HandleInput(string input)
        {
            // Coin inserted
            if (input == "TIGGER")
            {
                InputSimulator.SimulateKeyDown(VirtualKeyCode.VK_X);
                Thread.Sleep(50);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.VK_X);
            }
            else
            {
                // Input is formed liked X1:VALUE;Y1:VALUE;Z1:VALUE
                // Split at ; to get the pairs
                string[] arrStrMessages = input.Split(';');
                foreach (string strMessage in arrStrMessages)
                {
                    // Split at : to get key and value
                    string[] strMessageValue = strMessage.Split(':');
                    int intNewValue = int.Parse(strMessageValue[1]);
                    switch (strMessageValue[0])
                    {
                        case "X1":
                            SimulateInput(intX1, intNewValue, 10, VirtualKeyCode.VK_D, VirtualKeyCode.VK_A);
                            intX1 = intNewValue;
                            break;
                        case "Y1":
                            SimulateInput(intY1, intNewValue, 10, VirtualKeyCode.VK_S, VirtualKeyCode.VK_W);
                            intY1 = intNewValue;
                            break;
                        case "Z1":
                            SimuateKey(intZ1, intNewValue, 100, VirtualKeyCode.VK_F, VirtualKeyCode.VK_G);
                            intZ1 = intNewValue;
                            break;
                        case "X2":
                            SimulateInput(intX2, intNewValue, 10, VirtualKeyCode.LEFT, VirtualKeyCode.RIGHT);
                            intX2 = intNewValue;
                            break;
                        case "Y2":
                            SimulateInput(intY2, intNewValue, 10, VirtualKeyCode.UP, VirtualKeyCode.DOWN);
                            intY2 = intNewValue;
                            break;
                        case "Z2":
                            SimuateKey(intZ2, intNewValue, 100, VirtualKeyCode.VK_1, VirtualKeyCode.VK_2);
                            intZ2 = intNewValue;
                            break;
                    }
                }
            }
        }

        public static void SimulateInput(int intValue, int intNewValue, int intTolerance, VirtualKeyCode keyOne, VirtualKeyCode keyTwo)
        {
            // Press a key
            if (intValue < intNewValue - intTolerance)
            {
                InputSimulator.SimulateKeyUp(keyOne);
                InputSimulator.SimulateKeyDown(keyTwo);
            }
            else
            {
                if (intValue > intNewValue + intTolerance)
                {
                    InputSimulator.SimulateKeyUp(keyTwo);
                    InputSimulator.SimulateKeyDown(keyOne);
                }
            }
        }

        public static void SimuateKey(int intValue, int intNewValue, int intTolerance, VirtualKeyCode keyOne, VirtualKeyCode keyTwo)
        {
            // Press two keys
            if (intValue < intNewValue - intTolerance || intValue > intNewValue + intTolerance)
            {
                InputSimulator.SimulateKeyDown(keyOne);
                Thread.Sleep(50);
                InputSimulator.SimulateKeyUp(keyOne);
                InputSimulator.SimulateKeyDown(keyTwo);
                Thread.Sleep(50);
                InputSimulator.SimulateKeyUp(keyTwo);
            }
        }

        public static void HandleOtherInput(string strInput)
        {
            // trollface.jpg
            switch (strInput)
            {
                case "nyan":
                    Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
                    Console.WriteLine("░░░░░░░░░░▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄░░░░░░░░░");
                    Console.WriteLine("░░░░░░░░▄▀░░░░░░░░░░░░▄░░░░░░░▀▄░░░░░░░");
                    Console.WriteLine("░░░░░░░░█░░▄░░░░▄░░░░░░░░░░░░░░█░░░░░░░");
                    Console.WriteLine("░░░░░░░░█░░░░░░░░░░░░▄█▄▄░░▄░░░█░▄▄▄░░░");
                    Console.WriteLine("░▄▄▄▄▄░░█░░░░░░▀░░░░▀█░░▀▄░░░░░█▀▀░██░░");
                    Console.WriteLine("░██▄▀██▄█░░░▄░░░░░░░██░░░░▀▀▀▀▀░░░░██░░");
                    Console.WriteLine("░░▀██▄▀██░░░░░░░░▀░██▀░░░░░░░░░░░░░▀██░");
                    Console.WriteLine("░░░░▀████░▀░░░░▄░░░██░░░▄█░░░░▄░▄█░░██░");
                    Console.WriteLine("░░░░░░░▀█░░░░▄░░░░░██░░░░▄░░░▄░░▄░░░██░");
                    Console.WriteLine("░░░░░░░▄█▄░░░░░░░░░░░▀▄░░▀▀▀▀▀▀▀▀░░▄▀░░");
                    Console.WriteLine("░░░░░░█▀▀█████████▀▀▀▀████████████▀░░░░");
                    Console.WriteLine("░░░░░░████▀░░███▀░░░░░░▀███░░▀██▀░░░░░░");
                    Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
                    break;
                case "Open the pod bay doors":
                    Console.WriteLine("I'm sorry, Dave. I'm afraid I can't do that.");
                    break;
            }
        }
    }
}
