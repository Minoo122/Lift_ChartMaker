using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lift_ChartMaker.Models
{
    public class Scores
    {
        [Key]
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
