using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinData.Domain
{
    public class UserRoutine
    {
        public int Id { get; set; }                  
        public int UserId { get; set; }              
        public string Task { get; set; }             
        public string Time { get; set; }            
        public string WhyWeShouldDoIt { get; set; }
    }
}
