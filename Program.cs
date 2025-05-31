using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //TrimFile("Luigi's Mansion (Europe) (En,Fr,De,Es,It).3ds");
        if (args.Length > 0)
            File.WriteAllBytes(args[0] + ".trmd.3ds", TrimTrailingFF(File.ReadAllBytes(args[0])));
        else
            Console.WriteLine("Tool to trim .3ds-files created by efimandreev0.\nUSAGE: tool.exe file.3ds");
    }
    public static byte[] TrimTrailingFF(byte[] data)
    {
        int newLength = data.Length;
        if (data[0x100] == 'N' && data[0x101] == 'C' && data[0x102] == 'S' && data[0x103] == 'D')
        {
            // Going from end, counting all 0xFF bytes
            while (newLength > 0 && data[newLength - 1] == 0xFF)
            {
                newLength--;
            }

            // If file doesn't contains 0xFF then returning original
            if (newLength == data.Length)
                return data;

            // Creating new massive without 0xFF end-bytes
            byte[] trimmed = new byte[newLength];
            Buffer.BlockCopy(data, 0, trimmed, 0, newLength);
            return trimmed;
        }
        else
        {
            Console.WriteLine("Don't valid .3DS-file.");
            return data;
        }
    }
}