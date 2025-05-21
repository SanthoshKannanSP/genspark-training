namespace assignment_1.Exceptions
{
    internal class DuplicateEntityException : Exception
    {
        private string _message = "Duplicate Entity has been found";

        public DuplicateEntityException(string message) 
        { 
            _message = message;
        }

        public override string Message => _message;
    }
}
