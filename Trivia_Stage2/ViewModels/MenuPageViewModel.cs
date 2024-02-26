using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage2.Services;

namespace Trivia_Stage2.ViewModels
{
    public class MenuPageViewModel : ViewModel
    {
        private Service service;
        public MenuPageViewModel(Service service_)
        {
            service = service_;
        }
    }
}
