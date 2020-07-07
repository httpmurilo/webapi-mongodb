using System.Collections.Generic;

namespace Source.Domain.Shared
{
    public class ErrorViewModel
    {
        public ErrorViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public List<string> Errors { get; set; }
    }
}