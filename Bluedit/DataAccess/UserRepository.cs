
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace bluedit.DataAccess
{
    public class UserRepository : Repository<EfModels.User, Dbo.User>, Interfaces.IUserRepository
    {
        static readonly HashAlgorithm sha = SHA256.Create();
        public UserRepository(EfModels.BlueditContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
		}
		public Dbo.User? GetByName(string name)
        {
            EfModels.User? user = _context.Users.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<Dbo.User>(user);
        }

        public Dbo.User? GetById(long id)
        {
            EfModels.User? user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<Dbo.User>(user);
        }

		public static bool IsValidBase64String(string base64String)
		{
			// Check if the string is null or empty
			if (string.IsNullOrEmpty(base64String))
			{
				return false;
			}
			try
			{
				// Try to convert the Base-64 string to a byte array
				Encoding.UTF8.GetBytes(base64String);
				return true;
			}
			catch (FormatException e)
			{
                Console.WriteLine(e.ToString());
				// The string is not a valid Base-64 string
				return false;
			}
		}
		public string Hash(string password)
		{
			if (!IsValidBase64String(password))
			{
                throw new FormatException("need base64 format");
			}
			return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public async Task<Dbo.User?> SignUp(string pseudo, string password, string mail)
        {
			if (!IsValidBase64String(password))
            {
			// 	Console.WriteLine("fail 64");
				return null;
            }
			// Console.WriteLine("Password of user from " +password+" to "+Hash(password));
            // Console.WriteLine(Hash("password"));
			var user = new Dbo.User
            {
                Name = pseudo.Trim(),
                Password = Hash(password),
                Mail = mail,
                Description = ""
            };
            try
            {
                return await Create(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SignUp fail à cause de :"+ex.ToString());
                return null;
            }
        }

        public Dbo.User? SignIn(string pseudo, string password)
        {
			Dbo.User? user = GetByName(pseudo);
            // Console.WriteLine("input :" + pseudo);
			// Console.WriteLine("input :" + password);
			// Console.WriteLine("User est" + user?.Name);
            // Console.WriteLine(user?.Password + " équivaut? " + Hash(password));
			if (user == null || user.Password != Hash(password))
            {
                return null;
            }
            return user;
        }
    }
}
