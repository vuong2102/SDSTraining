using AntDesign;
using Blazor_QLSV.Models.Dto;
using Blazor_QLSV.Models.Entity;
using Blazor_QLSV.Models.Mapper;
using Blazor_QLSV.Service.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NHibernate.Util;
using OneOf.Types;
using System.Linq.Expressions;
using static NHibernate.Engine.Query.CallableParser;

namespace Blazor_QLSV.Pages
{
    public partial class StudentForm
    {
        [Inject] StudentMapper StudentMapper { get; set; }
        [Inject] IStudentService StudentService { get; set; }
        [Inject] IClassService ClassService { get; set; }
        [Parameter] public EventCallback EventCallback { get; set; }
        [Parameter] public Action Clear { get; set; }

        private List<Class> Classes = new List<Class>();
        public Student Student = new Student();
        public StudentDto StudentDto = new StudentDto();

        private EditForm editForm;
        bool visible = false;
        string title;


        protected override void OnInitialized()
        {
            Classes = ClassService.GetAllClasses();
            base.OnInitialized();
        }

        //create new student
        private async void Submit()
        {
            try
            {
                int check;
                bool result = false;
                Student = StudentMapper.MapDtoToEntity(StudentDto);
                if (Student.ID == 0)
                {
                    result = StudentService.AddNewStudent(Student);
                    if (result)
                    {
                        await OnSuccessClick("COMPLETED", "Added New Students Successfully");
                        SubmitTrue();
                    }
                    else await OnSuccessClick("FAILED", "Cannot Add New Student");
                }
                else
                {
                    result = StudentService.UpdateStudent(Student);
                    if(result)
                    {
                        await OnSuccessClick("COMPLETED", "Updated Student successfully");
                        SubmitTrue();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private async Task Notification(NotificationType type, string mess, string des)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = mess,
                Description = des,
                NotificationType = type
            });
        }
        private void SubmitFalse()
        {
            //Error();
        }
        public void Open()
        {
            title = "Add New Student";
            Student = new Student();
            StudentDto = StudentMapper.MapEntityToDto(Student);
            visible = true;
        }
        public void OpenUpdate()
        {
            title = "Edit Student";
            if (Student.ID == 0)
            {
                Student.ID = Student.Class.ID;
            }
            StudentDto = StudentMapper.MapEntityToDto(Student);
            
            visible = true;
        }
        public void Close()
        {
            visible = false;
        }

        public void SubmitTrue()
        {
            Clear?.Invoke();
            visible = false;
        }
        private async Task Notificationn(NotificationType type, string mess, string des)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = mess,
                Description = des,
                NotificationType = type
            });
        }
        private async Task OnSuccessClick(string mess, string des)
        {
            await Notificationn(NotificationType.Success, mess, des);
        }
        private async Task OnWarningClick()
        {
            string messWar = "WARNNING!!!";
            string des = "Vui lòng điền đầy đủ thông tin";
            if (StudentDto.ClassId <= 0)
            {
                await Notificationn(NotificationType.Warning, messWar, des);
            }
        }
    }
}