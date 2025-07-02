using System;
using System.Threading.Tasks;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using UnityEngine;

[Table("users")]
public class UsersTable : BaseModel
{
    [PrimaryKey("id", false)]
    public string Id {get; set;}
    
    [Column("username")]
    public string Username {get; set;}
    
    [Column("avatar_data")]
    public string AvatarData {get; set;}
    
    [Column("created_at")]
    public DateTime CreatedAt {get;}

    public UsersTable(){}

    public UsersTable(string userid, string userName, string userData)
    {
        Id = userid;
        Username = userName;
        AvatarData = userData;
    }
    //Создание пользователя в таблице users
    public static async Task<bool> CreateUser(Client client, UsersTable user)
    {
        try
        {
            var response = await client
                .From<UsersTable>()
                .Insert(user);
            if (response.Models.Count > 0)
            {
                return true;
                Debug.Log("Успешное создание профиля");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Создание профиля не удалось, код ошибки: " + ex.Message);
        }
        return false;
    }
}
