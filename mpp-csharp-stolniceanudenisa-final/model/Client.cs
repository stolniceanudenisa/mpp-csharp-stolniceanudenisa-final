namespace mpp_csharp_stolniceanudenisa_final.model
{
    public class Client: Entity<long>
    {
        public string Name { get; set; }
         public string Address { get; set; }
         
         public Client(long id, string name, string address):base(id)
         {
             Name = name;
             Address = address;
         }
         
         public override string ToString()
         {
             return "Client: " +
                    "name: " + Name + '\'' +
                    " address: '" + Address + '\'';
         }
    }
}