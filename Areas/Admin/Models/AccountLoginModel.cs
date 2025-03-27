namespace ASM2.Areas.Admin.Models
{
    public class AccountLoginModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int role {  get; set; }
        public DateTime CreateDate { get; set; }



        //public int AccountId { get; set; }
        //public string Username { get; set; }
        //public string Fullname { get; set; }
        //public string Email { get; set; }
        //public string Mobile { get; set; }
        //public DateTime CreateDate { get; set; }
        //public string RoleValue { get; set; }
    }
}
