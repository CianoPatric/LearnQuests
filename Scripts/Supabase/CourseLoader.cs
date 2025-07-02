using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class CourseLoader : MonoBehaviour
{
    public async Task<CourseData> LoadCourseData(string courseId)
    {
        var results = await SupabaseManager.Client
            .From<CoursesTable>()
            .Select("course_data")
            .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, courseId)
            .Single();
        var jsonString = results.ToString();
        CourseData courseData = JsonConvert.DeserializeObject<CourseData>(jsonString);
        Debug.Log("Курс загружен");
        return courseData;
    }
}