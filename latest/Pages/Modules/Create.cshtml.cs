using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static latest.Pages.Modules.IndexModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;
namespace latest.Pages.Modules
{

    public class CreateModel : PageModel
    {
        public ModuleInfo moduleInfo = new ModuleInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            moduleInfo.Module_Name = Request.Form["module_name"];
            moduleInfo.Module_Code = Request.Form["module_code"];
            moduleInfo.Number_Of_Credits = Request.Form["number_of_credits"];
            moduleInfo.Class_Hours_Per_Week = Request.Form["class_hours_per_week"];
            moduleInfo.Number_Of_Weeks_In_Semester = Request.Form["number_of_weeks_in_semester"];

            if (moduleInfo.Module_Name.Length == 0 || moduleInfo.Module_Code.Length == 0 ||
                moduleInfo.Number_Of_Credits.Length == 0 || moduleInfo.Class_Hours_Per_Week.Length == 0 
                || moduleInfo.Number_Of_Weeks_In_Semester.Length == 0)
            {
               errorMessage  = "All the fields are required";
                return;
            }

            try 
            {
                
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=myTimer;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO TimerData" + "(Module_Name, Module_Code,Number_Of_Credits,Class_Hours_Per_Week,Number_Of_Weeks_In_Semester ) VALUES"
                   
                        + "(@module_name, @module_code,@number_of_credits,@class_hours_per_week,@number_of_weeks_in_semester);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@module_name", moduleInfo.Module_Name);
                        command.Parameters.AddWithValue("@module_code", moduleInfo.Module_Code);
                        command.Parameters.AddWithValue("@number_of_credits", moduleInfo.Number_Of_Credits);
                        command.Parameters.AddWithValue("@class_hours_per_week", moduleInfo.Class_Hours_Per_Week);
                        command.Parameters.AddWithValue("@number_of_weeks_in_semester", moduleInfo.Number_Of_Weeks_In_Semester);
                    
                        command.ExecuteNonQuery();
                    
                    }



                }




            }





            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }



            //save changes
            moduleInfo.Module_Name = ""; moduleInfo.Module_Code = "";moduleInfo.Number_Of_Credits = "";
            moduleInfo.Class_Hours_Per_Week = ""; moduleInfo.Number_Of_Weeks_In_Semester = "";
            successMessage = "New Module Added Correctly";


            Response.Redirect("/Modules/Index");
        }
    }
}
