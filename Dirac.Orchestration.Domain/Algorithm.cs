using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dirac.Orchestration.Domain
{
    public class Algorithm:ValueObject
    {
        public static class AlgorithmFactory
        {
            public static Algorithm Create(string name, string location, string version)
            {
                return new Algorithm()
                {
                    Name = name,
                    Location = location,
                    Version = version
                };
            }

            public static Algorithm Get(string id)
            {
                return new Algorithm();
                

            }


        }

        protected Algorithm()
        {

        }
        [Required(AllowEmptyStrings =false, ErrorMessage = "Algorithm Name is a required attribute")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Algorithm location is a required attribute")]
        public string Location { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Algorithm version is a required attribute")]
        public string Version { get; set; }

        public LOB LOB { get; set; }

        public AlgorithmCategory Category { get; set; }

        }
}