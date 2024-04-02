using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia_Stage3.Services;

namespace Trivia_Stage3.ViewModels
{
    public class TriviaGamePageViewModel
    {
        private Service service;


        public TriviaGamePageViewModel(Service _service)
        {
            service = _service;
        }
        
    }
}
