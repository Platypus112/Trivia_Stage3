using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage2.Services;

namespace Trivia_Stage2.ViewModels
{
    public class UserQuestionsPageViewModel : ViewModel
    {
        private Service service;
        public UserQuestionsPageViewModel(Service service_)
        {
            service = service_;
        }
    }
}
