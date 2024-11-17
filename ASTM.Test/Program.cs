using Connection;

internal class Program
{
    private static void Main(string[] args)
    {
        // create ASTM instance
        // send ENQ frame
        // wait for ACK
        var serial = new SerialConnection();

        serial.Connect();
        serial.SendData($"{5}");
    }
}