using System;
using System.Threading.Tasks;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using UnityEngine;

[Table("courses")]
public class CoursesTable: BaseModel
{
    [PrimaryKey("id", false)]
    public string Id {get; set; }
    
    [Column("title")]
    public string Title {get; set;}
    
    [Column("language")]
    public string Language {get; set;}
    
    [Column("description")]
    public string Description {get; set;}
    
    [Column("course_data")]
    public string CoursesData {get; set;}
    
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}

    public static async Task<string> GetCoursesById(Client client, string courseId)
    {
        try
        {
            var response = await client
                .From<CoursesTable>()
                .Where(x => x.Id == courseId)
                .Single();
        } 
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return null;
    }

    public static async Task<Supabase.Postgrest.Responses.ModeledResponse<CoursesTable>> GetCoursesTitle(Client client)
    {
        try
        {
            var responce = await client
                .From<CoursesTable>()
                .Select("title")
                .Get();
            return responce;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public static async Task<Supabase.Postgrest.Responses.ModeledResponse<CoursesTable>> GetCourseJSON(Client client,
        string courseTitle)
    {
        try
        {
            var responce = await client
                .From<CoursesTable>()
                .Select("course_data")
                .Single();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }
}