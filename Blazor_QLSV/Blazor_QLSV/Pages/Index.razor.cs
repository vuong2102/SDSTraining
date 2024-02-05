using AntDesign;
using AntDesign.Charts;
using Blazor_QLSV.Models.Entity;
using Blazor_QLSV.Service;
using Blazor_QLSV.Service.Interface;
using Microsoft.AspNetCore.Components;
namespace Blazor_QLSV.Pages
{
    public partial class Index
    {
        List<Class> classes = new List<Class>();
        List<Student> students = new List<Student>();
        public object[] data1;
        int TotalStudent;
        [Inject] IClassService classService {  get; set; }
        [Inject] IStudentService studentService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            classes = classService.GetAllClasses();
            students = studentService.GetAllStudents();
            int TotalClass = classes.Count;
            data1 = new object[TotalClass];
            for (int i = 0; i < TotalClass; i++)
            {
                int res = classes[i].ID;
                string nameData = classes[i].Subject;
                int valueData = students.Count(x => x.Class.ID == res);
                if (valueData == 0) continue;
                data1[i] = new
                {
                    name = nameData,
                    value = valueData
                };
            }
            TotalStudent = students.Count();
        } 
    }
}
