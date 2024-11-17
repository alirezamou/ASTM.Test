using System.Threading;
using System.IO.Ports;

namespace Connection;

public class SerialConnection
{
    /**
     * Establish Serial connection
     * Send data
     * Receive data
     */

    private SerialPort _port;

    public SerialConnection()
    {
        _port = new SerialPort
        {
            PortName = "COM2",
            BaudRate = 9600,
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One,
            Handshake = Handshake.None,
        };

        _port.DataReceived += OnReceivedData;
    }

    public void Connect()
    {
        _port.Open();
    }

    public void SendData(string data)
    {
        _port.Write(data);
    }

    public void OnReceivedData(object sender, SerialDataReceivedEventArgs e)
    {
        Thread.Sleep(100);
        Console.WriteLine("Data Received");
        string data = _port.ReadExisting();
        Console.WriteLine("Data: ", data);
    }
}
