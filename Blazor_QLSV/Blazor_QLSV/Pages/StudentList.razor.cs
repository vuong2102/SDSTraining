using AntDesign;
using Blazor_QLSV.Models.Dto;
using Blazor_QLSV.Models.Entity;
using Blazor_QLSV.Models.Mapper;
using Blazor_QLSV.Service.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Drawing.Printing;
using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.JSInterop;
using System.Net.NetworkInformation;
using System.Reflection;
using OneOf.Types;

namespace Blazor_QLSV.Pages
{
    public partial class StudentList
    {
        [Inject] IStudentService StudentService { get; set; }
        [Inject] IClassService ClassService { get; set; }
        [Inject] StudentMapper StudentMapper { get; set; }
        [Inject] IMessageService MessageService { get; set; }


        public List<Student> ListStudent = new();
        public Student Student = new Student();

        protected StudentFilter StudentFilter = new StudentFilter();
        private PageView<Student> PagedData = new PageView<Student>();
        private List<StudentViewDto> StudentViews = new List<StudentViewDto>();
        private List<Class> Classes = new List<Class>();
        public List<Student> Students2 = new List<Student>();

        StudentForm StudentForm;
        private EditForm _formSearchList;
        ITable table;

        int pageIndex = 1;
        int pageSize = 5;
        int total = 0;
        protected override async Task OnInitializedAsync()
        {
            Classes = ClassService.GetAllClasses();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            PagedData = await StudentService.GetDataPageAsync(pageIndex, pageSize, StudentFilter);
            Students2 = PagedData.Data ?? new List<Student>();
            foreach (Student student in Students2)
            {
                var correspondingClass = Classes.FirstOrDefault(c => c.ID == student.Class.ID);
                student.Class.Subject = correspondingClass.Subject;
            }
            StudentViews = StudentMapper.MapListToListViewDtoWithIndex(Students2, pageSize * (pageIndex - 1));
            total = PagedData.PageCount;
            StateHasChanged();
        }

        private async void OnFinishSearchAsync(EditContext editContext)
        {
            pageIndex = 1;
            await LoadDataAsync();
            Console.WriteLine("Enter");
        }

        private void OnFinishFailedSearch(EditContext editContext)
        {
            StudentFilter = new StudentFilter();
        }

        public async Task OnPagingAsync()
        {
            await LoadDataAsync();
        }

        private async void ReloadListAsync()
        {
            pageIndex = 1;
            await LoadDataAsync();
        }

        public void Clear()
        {
            Student = new Student();
            StudentForm.Student = new Student();
            StudentFilter = new StudentFilter();
            ReloadListAsync();
        }

        private void UpdateStudent(StudentViewDto studentView)
        {
            Student = Students2?.FirstOrDefault(c => c.ID == studentView.ID);
            StudentForm.Student = Student;
            StudentForm.OpenUpdate();
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
        private async Task Notificationn1(NotificationType type, string mess, string des)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = mess,
                Description = des,
                NotificationType = type
            });
        }
        private void DeleteStudent(StudentViewDto studentView)
        {
            var result = false;
            Student = Students2?.FirstOrDefault(c => c.ID == studentView.ID);
            result = StudentService.DeleteStudent(Student);
            if (result == true)
            {
                _ = Notificationn1(NotificationType.Success, "DELETED", "Delete SuccessFully");
            }
            else
            {
                _ = Notificationn1(NotificationType.Warning, "FAILED", "Cannot Delete");
            }
            ReloadListAsync();
        }

    }
}
