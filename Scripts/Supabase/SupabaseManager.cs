using System;
using System.Threading.Tasks;
using Supabase;
using TMPro;
using UnityEngine;

public class SupabaseManager: MonoBehaviour
{
    //Общие параметры для Supabase
    public static Client Client;
    public static string url = ClientInfo.CLIENT_URL;
    public static string key = ClientInfo.CLIENT_KEY;

    //Параметры для регистрации
    public GameObject UserNameField;
    public GameObject RPasswordField;
    public GameObject Password2Field;
    public GameObject REmailField;
    public string StandartJsonAvatarData;
    
    //Параметры для авторизации
    public GameObject APasswordField;
    public GameObject AEmailField;

    async void Awake()
    {
        var options = new SupabaseOptions()
        {
            AutoConnectRealtime = true
        };
        
        Client = new Client(url, key, options);
        await Client.InitializeAsync();
        Debug.Log("✅ Supabase initialized");
        DontDestroyOnLoad(gameObject);
    }

    //Регистрация пользователя
    public async void RegUser()
    {
        try
        {
            var email = REmailField.GetComponent<TextMeshProUGUI>().text;
            var password = RPasswordField.GetComponent<TextMeshProUGUI>().text;
            var password2 = Password2Field.GetComponent<TextMeshProUGUI>().text;
            if (!IsValidEmail(email))
            {
                if (!IsStrongPassword(password))
                {
                    if (!ArePasswordsMatching(password, password2))
                    {
                        var session = await Client.Auth.SignUp(email, password);
                        if (session != null && session.User != null)
                        {
                            UsersTable user = new()
                            {
                                Id = session.User.Id,
                                Username = UserNameField.GetComponent<TextMeshProUGUI>().text,
                                AvatarData = StandartJsonAvatarData
                            };
                            var create = await UsersTable.CreateUser(Client, user);
                            if (create != true) {Debug.Log("Регистрация прошла с ошибкой");}
                        }
                        else {Debug.Log("Регистрация не удалась, сервер не доступен");}   
                    }
                    else {Debug.Log("Пароль должен совпадать");}
                }
                else {Debug.Log("Пароль должен быть не менее 8 символов и содержать хотя бы 1 строчную и 1 заглавную букву, 1 цифру и 1 спецсимвол");}
            }
            else {Debug.Log("Введите корректный email");}
        }
        catch (Exception ex)
        {
            Debug.Log("Непредвиденная ситуация при регистрации, код ошибки: " + ex.Message);
        }
    }
    
    //Авторизация пользователя
    public async void AuthUser()
    {
        try
        {
            var email = AEmailField.GetComponent<TextMeshProUGUI>().text;
            var password = APasswordField.GetComponent<TextMeshProUGUI>().text;
            var session = await Client.Auth.SignIn(email, password);
            if (session != null)
            {
                Debug.Log("Авторизация прошла успешно");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Непредвиденная ситуация при авторизации, код ошибки: " + ex.Message);
        }
    }
    
    //Проверка на безопасность пароля (минимум 8 символов, хотя бы 1 строчная, 1 заглавная буква, 1 спецсимвол, 1 цифра)
    public static bool IsStrongPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var regex = new System.Text.RegularExpressions.Regex(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
        );

        return regex.IsMatch(password);
    }
    //Проверка на корректность повтороного введения пароля
    public static bool ArePasswordsMatching(string password, string confirmPassword)
    {
        return password == confirmPassword;
    }
    //Проверка на корректность электронной почты (проверка на домен)
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        
        var regex = new System.Text.RegularExpressions.Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );
        return regex.IsMatch(email);
    }
}