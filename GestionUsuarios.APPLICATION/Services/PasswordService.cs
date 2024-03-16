using System.Text;
namespace GestionUsuarios.APPLICATION.Services;

public class PasswordService
{
    public string EncodePasswordToBase64(string password)
    {
        // Codificar la contraseña a Base64
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(password);
        string base64Password = Convert.ToBase64String(bytesToEncode);
        return base64Password;
    }

    public string DecodeBase64ToPassword(string base64Password)
    {
        // Decodificar la contraseña de Base64
        byte[] bytesToDecode = Convert.FromBase64String(base64Password);
        string password = Encoding.UTF8.GetString(bytesToDecode);
        return password;
    }
}
