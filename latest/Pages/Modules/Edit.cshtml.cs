using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static latest.Pages.Modules.IndexModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;

namespace latest.Pages.Modules
{
  
    public class EditModel : PageModel
    {
        public ModuleInfo moduleInfo = new ModuleInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=myTimer;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM TimerData WHERE Id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                moduleInfo.Id = "" + reader.GetInt32(0);
                                moduleInfo.Module_Name =  reader.GetString(1);
                                moduleInfo.Module_Code = "" + reader.GetString(2);
                                moduleInfo.Number_Of_Credits = "" + reader.GetInt32(3);
                                moduleInfo.Class_Hours_Per_Week = "" + reader.GetInt32(4); 
                                moduleInfo.Number_Of_Weeks_In_Semester = "" + reader.GetInt32(5);


                            }
                        }
                    }

                }




            }
            catch(Exception ex) 
            {
            errorMessage = ex.Message;
            
            }


        }

        public void OnPost() 
        {
            moduleInfo.Id = Request.Form["id"];
            moduleInfo.Module_Name = Request.Form["module_name"];
            moduleInfo.Module_Code = Request.Form["module_code"];
            moduleInfo.Number_Of_Credits = Request.Form["number_of_credits"];
            moduleInfo.Class_Hours_Per_Week = Request.Form["class_hours_per_week"];
            moduleInfo.Number_Of_Weeks_In_Semester = Request.Form["number_of_weeks_in_semester"];

            if ( moduleInfo.Id.Length == 0 ||  moduleInfo.Module_Name.Length == 0 || moduleInfo.Module_Code.Length == 0 ||
              moduleInfo.Number_Of_Credits.Length == 0 || moduleInfo.Class_Hours_Per_Week.Length == 0
              || moduleInfo.Number_Of_Weeks_In_Semester.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=myTimer;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "UPDATE TimerData" +
                        "SET Module_Name=@module_name, Module_Code=@module_code,Number_Of_Credits=@number_of_credits, Class_Hours_Per_Week=@class_hours_per_week, Number_Of_Weeks_In_Semester=@number_of_weeks_in_semester " +
                        "WHERE Id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@module_name", moduleInfo.Module_Name);
                        command.Parameters.AddWithValue("@module_code", moduleInfo.Module_Code);
                        command.Parameters.AddWithValue("@number_of_credits", moduleInfo.Number_Of_Credits);
                        command.Parameters.AddWithValue("@class_hours_per_week", moduleInfo.Class_Hours_Per_Week);
                        command.Parameters.AddWithValue("@number_of_weeks_in_semester", moduleInfo.Number_Of_Weeks_In_Semester);
                        command.Parameters.AddWithValue("@id", moduleInfo.Id);

                        command.ExecuteNonQuery();

                    }


                }






            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Modules/Index");

        }


    }
}
