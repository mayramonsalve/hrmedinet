using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;

namespace Medinet.Models.ViewModels
{
    public class PerformanceEvaluationViewModel
    {
        public PerformanceEvaluation performanceEvaluation { get; private set; }

        public PerformanceEvaluationViewModel(PerformanceEvaluation performanceEvaluation)
        {
            this.performanceEvaluation = performanceEvaluation;        
        }

    }
}
