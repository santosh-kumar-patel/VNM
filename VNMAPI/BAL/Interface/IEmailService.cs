namespace BAL.Interface
{
    public interface IEmailService
    {
        void SendEmail(String ToEmail, string cc, string bcc, String Subj, string Message);
    }
}
