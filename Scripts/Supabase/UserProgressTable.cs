using System;
using System.Collections.Generic;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("user_progress")]
public class UserProgressTable : BaseModel
{
    [PrimaryKey("id", false)]
    public string Id {get; set;}
    
    [Column("user_id")]
    public string UserId {get; set;}
    
    [Column("course_id")]
    public string CourseId {get; set;}
    
    [Column("completed_lessons")]
    public List<int> CompletedLessons {get; set;}
    
    [Column("updated_at")]
    public DateTime UpdatedAt {get;}
}