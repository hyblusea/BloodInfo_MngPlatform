﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Reflection;
using System.ComponentModel;    
using System.Threading;
using System.IO;

namespace SerialPortComm
{
    /// <summary>
    /// Manager for serial port data
    /// </summary>
    public class SerialPortManager : IDisposable
    {
        public SerialPortManager()
        {
            // 遍历所有的串口, 并初始化可用的波特率
            _currentSerialSettings.PortNameCollection = SerialPort.GetPortNames();
            _currentSerialSettings.UpdateBaudRateCollection(421887);
        }

        
        ~SerialPortManager()
        {
            Dispose(false);
        }


        #region Fields
        private SerialPort _serialPort;
        private SerialSettings _currentSerialSettings = new SerialSettings();
        private string _latestRecieved = String.Empty;
        public event EventHandler<SerialDataEventArgs> NewSerialDataRecieved; 

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current serial port settings
        /// </summary>
        public SerialSettings CurrentSerialSettings
        {
            get { return _currentSerialSettings; }
            set { _currentSerialSettings = value; }
        }

        #endregion

        #region Event handlers

        void _currentSerialSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // if serial port is changed, a new baud query is issued
            if (e.PropertyName.Equals("PortName"))
                UpdateBaudRateCollection();
        }

        
        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int dataLength = _serialPort.BytesToRead;
            byte[] data = new byte[dataLength];
            int nbrDataRead = _serialPort.Read(data, 0, dataLength);
            if (nbrDataRead == 0)
                return;
            
            // Send data to whom ever interested
            if (NewSerialDataRecieved != null)
                NewSerialDataRecieved(this, new SerialDataEventArgs(data));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects to a serial port defined through the current settings
        /// </summary>
        public void StartListening(string pName, int pBaudRate, Parity pParity, int pDataBits, StopBits pStopBits)
        {
            // Closing serial port if it is open
            if (_serialPort != null && _serialPort.IsOpen)
                    _serialPort.Close();

            // Setting serial port settings
            _serialPort = new SerialPort(pName, pBaudRate, pParity, pDataBits, pStopBits);

            // Subscribe to event and open serial port for data
            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            _serialPort.Open();
        }

        public void SendMsg(byte[] byMsg)
        {
            if( _serialPort.IsOpen)
                _serialPort.Write(byMsg, 0, byMsg.Length);
        }

        public void SendDataRequest()
        {
            if (_serialPort.IsOpen)
                _serialPort.WriteLine("K" + "\r\n");
        }

        public void SendDataRequest2()
        {
            _serialPort.WriteLine("K");
        }

        public string ReadData()
        {
            return _serialPort.ReadExisting();
        }

        /// <summary>
        /// Closes the serial port
        /// </summary>
        public void StopListening()
        {
            _serialPort.Close();
        }


        /// <summary>
        /// Retrieves the current selected device's COMMPROP structure, and extracts the dwSettableBaud property
        /// </summary>
        private void UpdateBaudRateCollection()
        {
            _serialPort = new SerialPort(_currentSerialSettings.PortName);
            _serialPort.Open();
            object p = _serialPort.BaseStream.GetType().GetField("commProp", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_serialPort.BaseStream);
            Int32 dwSettableBaud = (Int32)p.GetType().GetField("dwSettableBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(p);

            _serialPort.Close();
            _currentSerialSettings.UpdateBaudRateCollection(dwSettableBaud);
        }

        // Call to release serial port
        public void Dispose()
        {
            Dispose(true);
        }

        // Part of basic design pattern for implementing Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                 if (_serialPort != null)
                    _serialPort.DataReceived -= new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            }
            // Releasing serial port (and other unmanaged objects)
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                _serialPort.Dispose();
            }
        }


        #endregion

    }

    /// <summary>
    /// EventArgs used to send bytes recieved on serial port
    /// </summary>
    public class SerialDataEventArgs : EventArgs
    {
        public SerialDataEventArgs(byte[] dataInByteArray)
        {
            Data = dataInByteArray;
        }

        /// <summary>
        /// Byte array containing data from serial port
        /// </summary>
        public byte[] Data;
    }

    public class SerialSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string _portName = "";
        string[] _portNameCollection;
        int _baudRate = 4800;
        BindingList<int> _baudRateCollection = new BindingList<int>();
        Parity _parity = Parity.None;
        int _dataBits = 8;
        int[] _dataBitsCollection = new int[] { 5, 6, 7, 8 };
        StopBits _stopBits = StopBits.One;

        #region Properties
        /// <summary>
        /// The port to use (for example, COM1).
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set
            {
                if (!_portName.Equals(value))
                {
                    _portName = value;
                    SendPropertyChangedEvent("PortName");
                }
            }
        }
        /// <summary>
        /// The baud rate.
        /// </summary>
        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                if (_baudRate != value)
                {
                    _baudRate = value;
                    SendPropertyChangedEvent("BaudRate");
                }
            }
        }

        /// <summary>
        /// One of the Parity values.
        /// </summary>
        public Parity Parity
        {
            get { return _parity; }
            set
            {
                if (_parity != value)
                {
                    _parity = value;
                    SendPropertyChangedEvent("Parity");
                }
            }
        }
        /// <summary>
        /// The data bits value.
        /// </summary>
        public int DataBits
        {
            get { return _dataBits; }
            set
            {
                if (_dataBits != value)
                {
                    _dataBits = value;
                    SendPropertyChangedEvent("DataBits");
                }
            }
        }
        /// <summary>
        /// One of the StopBits values.
        /// </summary>
        public StopBits StopBits
        {
            get { return _stopBits; }
            set
            {
                if (_stopBits != value)
                {
                    _stopBits = value;
                    SendPropertyChangedEvent("StopBits");
                }
            }
        }

        /// <summary>
        /// Available ports on the computer
        /// </summary>
        public string[] PortNameCollection
        {
            get { return _portNameCollection; }
            set { _portNameCollection = value; }
        }

        /// <summary>
        /// Available baud rates for current serial port
        /// </summary>
        public BindingList<int> BaudRateCollection
        {
            get { return _baudRateCollection; }
        }

        /// <summary>
        /// Available databits setting
        /// </summary>
        public int[] DataBitsCollection
        {
            get { return _dataBitsCollection; }
            set { _dataBitsCollection = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Updates the range of possible baud rates for device
        /// </summary>
        /// <param name="possibleBaudRates">dwSettableBaud parameter from the COMMPROP Structure</param>
        /// <returns>An updated list of values</returns>
        public void UpdateBaudRateCollection(int possibleBaudRates)
        {
            const int BAUD_075 = 0x00000001;
            const int BAUD_110 = 0x00000002;
            const int BAUD_150 = 0x00000008;
            const int BAUD_300 = 0x00000010;
            const int BAUD_600 = 0x00000020;
            const int BAUD_1200 = 0x00000040;
            const int BAUD_1800 = 0x00000080;
            const int BAUD_2400 = 0x00000100;
            const int BAUD_4800 = 0x00000200;
            const int BAUD_7200 = 0x00000400;
            const int BAUD_9600 = 0x00000800;
            const int BAUD_14400 = 0x00001000;
            const int BAUD_19200 = 0x00002000;
            const int BAUD_38400 = 0x00004000;
            const int BAUD_56K = 0x00008000;
            const int BAUD_57600 = 0x00040000;
            const int BAUD_115200 = 0x00020000;
            const int BAUD_128K = 0x00010000;

            _baudRateCollection.Clear();

            if ((possibleBaudRates & BAUD_075) > 0)
                _baudRateCollection.Add(75);
            if ((possibleBaudRates & BAUD_110) > 0)
                _baudRateCollection.Add(110);
            if ((possibleBaudRates & BAUD_150) > 0)
                _baudRateCollection.Add(150);
            if ((possibleBaudRates & BAUD_300) > 0)
                _baudRateCollection.Add(300);
            if ((possibleBaudRates & BAUD_600) > 0)
                _baudRateCollection.Add(600);
            if ((possibleBaudRates & BAUD_1200) > 0)
                _baudRateCollection.Add(1200);
            if ((possibleBaudRates & BAUD_1800) > 0)
                _baudRateCollection.Add(1800);
            if ((possibleBaudRates & BAUD_2400) > 0)
                _baudRateCollection.Add(2400);
            if ((possibleBaudRates & BAUD_4800) > 0)
                _baudRateCollection.Add(4800);
            if ((possibleBaudRates & BAUD_7200) > 0)
                _baudRateCollection.Add(7200);
            if ((possibleBaudRates & BAUD_9600) > 0)
                _baudRateCollection.Add(9600);
            if ((possibleBaudRates & BAUD_14400) > 0)
                _baudRateCollection.Add(14400);
            if ((possibleBaudRates & BAUD_19200) > 0)
                _baudRateCollection.Add(19200);
            if ((possibleBaudRates & BAUD_38400) > 0)
                _baudRateCollection.Add(38400);
            if ((possibleBaudRates & BAUD_56K) > 0)
                _baudRateCollection.Add(56000);
            if ((possibleBaudRates & BAUD_57600) > 0)
                _baudRateCollection.Add(57600);
            if ((possibleBaudRates & BAUD_115200) > 0)
                _baudRateCollection.Add(115200);
            if ((possibleBaudRates & BAUD_128K) > 0)
                _baudRateCollection.Add(128000);

            SendPropertyChangedEvent("BaudRateCollection");
        }

        /// <summary>
        /// Send a PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of changed property</param>
        private void SendPropertyChangedEvent(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
