using System.ComponentModel.DataAnnotations;
using Fluent = FluentValidation.Results;

namespace BookFinder
{
    public class Result<T> where T : class
    {
        public T Value { get; set; }
        public Fluent.ValidationResult ValidationResult { get; set; } = new Fluent.ValidationResult();

        public void AddValidationFailure(string propertyName, string errorMessage)
        {
            var failures = this.ValidationResult.Errors ?? new List<Fluent.ValidationFailure>();
            failures.Add(new Fluent.ValidationFailure(propertyName, errorMessage));
            ValidationResult = new Fluent.ValidationResult(failures);
        }
    }
}
