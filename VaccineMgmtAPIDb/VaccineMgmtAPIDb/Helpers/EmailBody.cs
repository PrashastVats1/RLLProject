namespace VaccineMgmtAPIDb.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<html>
<head>
</head>
<body style=""margin:0;padding:0;font-family:Arial, Helvetica, sans-serif;"">
<div style=""height:auto; background:linear-gradient(to top,#c9c9ff 50%, #6e6ef6 90%) no-repeat; width:400px;padding:30px"">
  <div>
    <div>
      <h1>Reset your Password</h1>
      <hr>
      <p>You are recieving this mail, because you requested a password reset for your Vaccine Management Account.<br>
      This link will expire in 30 minutes</p>
      <p>Please tap the button below to choose a new password.</p>
      <a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"" style=""background:#0d6efc; padding:10px;border:none;
      color:white;border-radius:4px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none"">Reset Password</a><br>
      <p>With Regards<br><br>
      Prashast Vats</p>
      </div>
    </div>
  </div>
</body>
</html>";
        }
    }
}
