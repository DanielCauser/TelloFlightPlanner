using System;

namespace FlightPlanner
{
    public class GlobalExceptionHandler: IObserver<Exception>
    {
//        readonly IUserDialogs dialogs;
//        public GlobalExceptionHandler(IUserDialogs dialogs) => this.dialogs = dialogs;

        public void OnCompleted() { }
        public void OnError(Exception error) { }


        public void OnNext(Exception value)
        {
            Console.WriteLine(value);
//            this.dialogs.Alert(value.ToString(), "ERROR");
        }
    }
}