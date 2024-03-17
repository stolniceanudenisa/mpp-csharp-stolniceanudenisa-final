namespace mpp_csharp_stolniceanudenisa_final.model
{
    public class User : Entity<long>
    { 
        public string Username { get; set; }
        public string Password { get; set; }

         public User(long id, string username, string password): base(id)
         {
             Username = username;
             Password = password;
         }
     
         public override string ToString()
         {
             return "User:" +
                    " username: " + Username + '\'' +
                    ", password: " + Password + '\'';
         }
        
    }
}