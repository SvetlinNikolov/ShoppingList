namespace ShoppingList.Views.Shared
{
    public class ErrorModel
    {
        public ErrorModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; } // should log error 
    }
}