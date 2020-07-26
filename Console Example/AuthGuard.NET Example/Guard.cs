using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows;

namespace AuthGuard
{
    internal class Guard
    {
        public static bool Gay = false;

        public static bool Initialized = false;

        public static bool InitPassed = false;
        public static string Key { get; set; }
        public static string VariableKey { get; set; }
        public static string Secret { get; set; }
        public static string Salt { get; set; }
        private static string Message { get; set; }

        public static string ApiUrl = "https://api.authguard.net/";

        private static Dictionary<string, string> Vars = new Dictionary<string, string>();
        public static string Session_ID(int length)
        {
            Random random = new Random();
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static void Start_Session()
        {
            try
            {
                Key = Algorithms.SaltString(Convert.ToBase64String(Encoding.Default.GetBytes(Session_ID(32))));
                Salt = Algorithms.SaltString(Convert.ToBase64String(Encoding.Default.GetBytes(Session_ID(16))));
            }
            catch
            {
                Key = Algorithms.SaltString(Convert.ToBase64String(Encoding.Default.GetBytes(Session_ID(32))));
                Salt = Algorithms.SaltString(Convert.ToBase64String(Encoding.Default.GetBytes(Session_ID(16))));
            }
        }
        private static void LoadData(string username, string email, int level, DateTime expires, string ip)
        {
            UserInfo.Username = username;
            UserInfo.Email = email;
            UserInfo.Level = level;
            UserInfo.Expires = expires;
            UserInfo.IP = ip;
        }
        public static string Var(string Name)
        {
            try
            {
                return Algorithms.DecryptData(Vars[Name].ToString());
            }
            catch
            {
                return "Unknown variable";
            }
        }
        public static string getUniqueID()
        {
            return WindowsIdentity.GetCurrent().User.Value;
        }
        #region Events
        public static void Initialize(string appsecret, string version, string variablekey)
        {
            if (string.IsNullOrEmpty(appsecret) || string.IsNullOrEmpty(version))
            {
                MessageBox.Show("Please don't directly open the form, call to Guard.Initialize!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }
            VariableKey = variablekey;
            Secret = appsecret;
            Initialize(version);
            if (GuardSettings.Freemode)
            {
               InitPassed = true;
               Initialized = true;
            }
        }
        private static bool Initialize(string version)
        {
            Start_Session();
            if (string.IsNullOrEmpty(version))
            {
                MessageBox.Show("Please enter a version!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                if (!Security.HashCheck())
                {
                    Gay = true;
                    MessageBox.Show("Invalid Newtonsoft.Json.dll", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill();
                }
                if (!Gay)
                {
                    var request = (HttpWebRequest)WebRequest.Create(ApiUrl+"program.php");

                    var postData = $"&programtoken={Algorithms.EncryptData(Secret).Replace("+", "#")}";
                    postData += $"&session_id={Key}";
                    postData += $"&session_salt={Salt}";
                    var data = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.UserAgent = "AuthGuard";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(Algorithms.DecryptData(responseString));
                    string status = json.status;
                    if (status != "success")
                    {
                        string info = json.info;
                        MessageBox.Show(info, "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        InitPassed = true;
                        string versionn = json.version;
                        int usercount = json.clients;
                        string freemode = json.freemode;
                        string enabled = json.enabled;
                        string message = json.message;
                        string updatelink = json.downloadlink;
                        string hash = json.hash;
                        string filename = json.filename;
                        string devmode = json.devmode;
                        string hwidlock = json.hwidlock;
                        string programname = json.programname;
                        GuardSettings.HWIDLock = true;
                        GuardSettings.ProgramName = programname;
                        if (hwidlock != "1")
                        {
                            GuardSettings.HWIDLock = false;
                        }
                        GuardSettings.DeveloperMode = true;
                        if (devmode != "1")
                        {
                            GuardSettings.DeveloperMode = false;
                            try
                            {
                                if (Algorithms.CalculateMD5(filename) != hash)
                                {
                                    Gay = true;
                                    MessageBox.Show("File hash mismatch, exiting...", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                                    Process.GetCurrentProcess().Kill();
                                    return false;
                                }
                            }
                            catch
                            {
                                Gay = true;
                                MessageBox.Show("Exception caught, please ensure that you have used the hash checker to implement the right hash and exe name in your program settings!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                                Process.GetCurrentProcess().Kill();
                                return false;
                            }
                        }
                        bool bruhfreemode;
                        if (freemode == "1")
                        {
                            bruhfreemode = true;
                        }
                        else
                        {
                            bruhfreemode = false;
                        }
                        GuardSettings.Clients = usercount;
                        GuardSettings.Freemode = bruhfreemode;
                        GuardSettings.Version = version;
                        GuardSettings.Message = message;
                        if (enabled == "0")
                        {
                            Gay = true;
                            MessageBox.Show("Program disabled by owner, exiting...", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                            Process.GetCurrentProcess().Kill();
                            return false;
                        }
                        if (version != versionn)
                        {
                            MessageBox.Show("Update available!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            try
                            {
                                Process.Start(updatelink);
                            }
                            catch
                            {
                                Process.Start("https://" + updatelink);
                            }
                            Gay = true;
                            Process.GetCurrentProcess().Kill();
                            return false;
                        }
                        if (bruhfreemode == true)
                        {
                            GuardSettings.Freemode = true;
                        }
                    }
                }
                return false;
            }
        }
        public static bool GrabVariables(string secretkey, string programtoken, string username, string password)
        {
            Start_Session();
            if (!Gay)
            {
                var request = (HttpWebRequest)WebRequest.Create(ApiUrl + "variables.php");

                var postData = $"&programtoken={Algorithms.EncryptData(programtoken).Replace("+", "#")}";
                postData += $"&username={Algorithms.EncryptData(username).Replace("+", "#")}";
                postData += $"&password={Algorithms.EncryptData(password).Replace("+", "#")}";
                postData += $"&hwid={Algorithms.EncryptData(getUniqueID()).Replace("+", "#")}";
                postData += $"&key={Algorithms.EncryptData(secretkey).Replace("+", "#")}";
                postData += $"&session_id={Key}";
                postData += $"&session_salt={Salt}";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.UserAgent = "AuthGuard";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                dynamic json = JsonConvert.DeserializeObject(Algorithms.DecryptData(responseString));
                string status = json.status;
                if (status != "success")
                {
                    string info = json.info;
                    MessageBox.Show(info, "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Vars = JsonConvert.DeserializeObject<Dictionary<string, string>>(json.vars.ToString());
                    return true;
                }
            }
            return false;
        }
        public static bool Login(string username, string password, bool message = true)
        {
            Start_Session();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!Security.HashCheck())
                {
                    Gay = true;
                    MessageBox.Show("Failed hash check on Newtonsoft.Json.dll please use the one provided with the AuthGuard download.", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill();
                    return false;
                }
                if (!Gay)
                {
                    var request = (HttpWebRequest)WebRequest.Create(ApiUrl + "login.php");

                    var postData = $"username={Algorithms.EncryptData(username).Replace("+", "#")}";
                    postData += $"&password={Algorithms.EncryptData(password).Replace("+", "#")}";
                    postData += $"&hwid={Algorithms.EncryptData(getUniqueID()).Replace("+", "#")}";
                    postData += $"&programtoken={Algorithms.EncryptData(Secret).Replace("+", "#")}";
                    postData += $"&timestamp={Algorithms.EncryptData(Algorithms.Encrypt(DateTime.Now.ToString())).Replace("+", "#")}";
                    postData += $"&session_id={Key}";
                    postData += $"&session_salt={Salt}";
                    var data = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.UserAgent = "AuthGuard";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    dynamic json = JsonConvert.DeserializeObject(Algorithms.DecryptData(responseString));
                    string status = json.status;
                    if (status != "success")
                    {
                        string info = json.info;
                        if (message)
                        {
                            MessageBox.Show(info, "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return false;
                    }
                    else
                    {
                        Initialized = true;
                        string datetime = json.timestamp;
                        string hwidd = json.hwid;
                        string userr = json.username;
                        string email = json.email;
                        int level = json.level;
                        string expires = json.expires;
                        string ip = json.ip;
                        LoadData(userr, email, level, DateTime.Parse(expires), ip);
                        GrabVariables(VariableKey, Secret, userr, password);
                        if (Security.SecurityChecks(Algorithms.Decrypt(datetime), hwidd))
                        {
                            if (!string.IsNullOrEmpty(GuardSettings.Message))
                            {
                                MessageBox.Show(GuardSettings.Message, "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                            else
                            {
                                Security.ChallengesPassed = true;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool RedeemToken(string username, string password, string token, bool message = true)
        {

            Start_Session();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Please fill in all fields!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!Security.HashCheck())
                {
                    Gay = true;
                    MessageBox.Show("Failed hash check, please use all files provided in the download.", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                    Gay = true;
                    Process.GetCurrentProcess().Kill();
                    return false;
                }
                if (!Gay)
                {
                    var request = (HttpWebRequest)WebRequest.Create(ApiUrl+"redeemtoken.php");

                    var postData = $"username={Algorithms.EncryptData(username).Replace("+", "#")}";
                    postData += $"&password={Algorithms.EncryptData(password).Replace("+", "#")}";
                    postData += $"&hwid={Algorithms.EncryptData(getUniqueID()).Replace("+", "#")}";
                    postData += $"&token={Algorithms.EncryptData(token).Replace("+", "#")}";
                    postData += $"&programtoken={Algorithms.EncryptData(Secret).Replace("+", "#")}";
                    postData += $"&session_id={Key}";
                    postData += $"&session_salt={Salt}";
                    var data = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.UserAgent = "AuthGuard";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(Algorithms.DecryptData(responseString));
                    string status = json.status;
                    if (status != "success")
                    {
                        string info = json.info;
                        if (message)
                        {
                            MessageBox.Show(info, "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool Register(string username, string password, string email, string token, bool message = true)
        {

            Start_Session();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Please fill in all fields!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!Security.HashCheck())
            {
                Gay = true;
                MessageBox.Show("Invalid Newtonsoft.Json.dll please use the file provided with the download!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
                return false;
            }
            if (!Gay)
            {
                var request = (HttpWebRequest)WebRequest.Create(ApiUrl+"register.php");

                var postData = $"username={Algorithms.EncryptData(username).Replace("+", "#")}";
                postData += $"&password={Algorithms.EncryptData(password).Replace("+", "#")}";
                postData += $"&email={Algorithms.EncryptData(email).Replace("+", "#")}";
                postData += $"&hwid={Algorithms.EncryptData(getUniqueID()).Replace("+", "#")}";
                postData += $"&token={Algorithms.EncryptData(token).Replace("+", "#")}";
                postData += $"&programtoken={Algorithms.EncryptData(Secret).Replace("+", "#")}";
                postData += $"&session_id={Key}";
                postData += $"&session_salt={Salt}";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.UserAgent = "AuthGuard";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic json = JsonConvert.DeserializeObject(Algorithms.DecryptData(responseString));
                string status = json.status;
                if (status != "success")
                {
                    string info = json.info;
                    if (message)
                    {
                        MessageBox.Show(info, "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
    internal class Algorithms
    {
        public static string SaltString(string value)
        {
            value = value.Replace("a", "!");
            value = value.Replace("z", "?");
            value = value.Replace("b", "}");
            value = value.Replace("c", "{");
            value = value.Replace("d", "]");
            value = value.Replace("e", "[");
            return value;
        }
        public static string DesaltString(string value)
        {
            value = value.Replace("?", "z");
            value = value.Replace("!", "a");
            value = value.Replace("}", "b");
            value = value.Replace("{", "c");
            value = value.Replace("]", "d");
            value = value.Replace("[", "e");
            return value;
        }
        public static string DecryptData(string value)
        {
            string message = value;
            string password = Encoding.Default.GetString(Convert.FromBase64String(DesaltString(Guard.Key)));
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
            byte[] iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(DesaltString(Guard.Salt))));
            string decrypted = DecryptString(message, key, iv);
            return decrypted;
        }
        public static string EncryptData(string value)
        {
            string message = value;
            string password = Encoding.Default.GetString(Convert.FromBase64String(DesaltString(Guard.Key)));
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
            byte[] iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(DesaltString(Guard.Salt))));
            string decrypted = EncryptString(message, key, iv);
            return decrypted;
        }
        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;
        }
        public static string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            string plainText = String.Empty;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] plainBytes = memoryStream.ToArray();
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }
            return plainText;
        }
        public static string Encrypt(string clearText)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("datexd", new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("datexd", new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
    internal class Security
    {
        public static bool ChallengesPassed = true;
        private static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        public static bool HashCheck()
        {
            if (CalculateMD5("Newtonsoft.Json.dll") == "6815034209687816d8cf401877ec8133")
            {
                return true;
            }
            else
            {
                Guard.Gay = true;
                return false;
            }
        }
        public static bool DNSCheck()
        {
            string drive = Path.GetPathRoot(Environment.SystemDirectory);
            using (StreamReader sr = new StreamReader($@"{drive}Windows\System32\drivers\etc\hosts"))
            {
                string contents = sr.ReadToEnd();
                if (contents.Contains("api.authguard.net"))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool SecurityChecks(string date, string hwid)
        {
            if (hwid != Guard.getUniqueID() && GuardSettings.HWIDLock)
            {
                Guard.Gay = true;
                MessageBox.Show("HWID not recognized, exiting...", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }
            else if(DNSCheck())
            {
                Guard.Gay = true;
                MessageBox.Show("DNS redirecting has been detected!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                DateTime dt1 = DateTime.Parse(date); //time sent
                DateTime dt2 = DateTime.Now; //time received
                TimeSpan d3 = dt1 - dt2;
                if (Convert.ToInt32(d3.Seconds.ToString().Replace("-", "")) >= 5 || Convert.ToInt32(d3.Minutes.ToString().Replace("-", "")) >= 1)
                {
                    Guard.Gay = true;
                    MessageBox.Show("Possible malicious network activity, exiting...", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
    internal class Anti_Analysis
    {
        #region nativemethods
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
        #endregion
        public static void Init()
        {
            if (DetectManufacturer() || DetectDebugger() || DetectSandboxie() || IsSmallDisk() || IsXP())
            {
                Guard.Gay = true;
                Process.GetCurrentProcess().Kill();
            }
            if (Security.DNSCheck())
            {
                Guard.Gay = true;
                MessageBox.Show("DNS redirecting has been detected!", "AuthGuard", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }
        }
        private static bool IsSmallDisk()
        {
            try
            {
                long GB_60 = 61000000000;
                if (new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize <= GB_60)
                    return true;
            }
            catch { }
            return false;
        }
        private static bool IsXP()
        {
            try
            {
                if (new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName.ToLower().Contains("xp"))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
        private static bool DetectManufacturer()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
                {
                    using (var items = searcher.Get())
                    {
                        foreach (var item in items)
                        {
                            string manufacturer = item["Manufacturer"].ToString().ToLower();
                            if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                                || manufacturer.Contains("vmware")
                                || item["Model"].ToString() == "VirtualBox")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }
        private static bool DetectDebugger()
        {
            bool isDebuggerPresent = false;
            try
            {
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
                return isDebuggerPresent;
            }
            catch
            {
                return isDebuggerPresent;
            }
        }
        private static bool DetectSandboxie()
        {
            try
            {
                if (GetModuleHandle("SbieDll.dll").ToInt32() != 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
    public class GuardSettings
    {
        public static string Version { get; set; }
        public static string ProgramName { get; set; }
        public static int Clients { get; set; }
        public static bool Freemode { get; set; }
        public static string Message { get; set; }
        public static bool DeveloperMode { get; set; }
        public static bool HWIDLock { get; set; }
    }
    public class Tools
    {
        public static string SkypeResolver(string SkypeUsername)
        {
            return Request("skyperesolver", SkypeUsername);
        }

        public static string IP2Skype(string IP)
        {
            return Request("ip2skype", IP);
        }

        public static string Email2Skype(string Email)
        {
            return Request("email2skype", Email);
        }

        public static string GeoIP(string IP)
        {
            return Request("geoip", IP);
        }

        public static string DNSResolver(string URL)
        {
            return Request("dnsresolver", URL);
        }

        public static string CloudFlareResolver(string URL)
        {
            return Request("cloudflareresolver", URL);
        }

        public static string PhoneResolver(string Phone)
        {
            return Request("phoneresolver", Phone);
        }

        public static string SiteHeaders(string URL)
        {
            return Request("siteheaders", URL);
        }

        public static string SiteWhois(string URL)
        {
            return Request("sitewhois", URL);
        }

        public static string Ping(string IP)
        {
            return Request("ping", IP);
        }

        public static string PortScan(string IP)
        {
            return Request("portscan", IP);
        }

        public static string DisposableMailChecker(string Email)
        {
            return Request("disposablemailcheck", Email);
        }

        public static string IP2Website(string IP)
        {
            return Request("ip2website", IP);
        }

        public static string DomainInfo(string URL)
        {
            return Request("domaininfo", URL);
        }

        private static string Request(string type, string input)
        {
            WebClient wc = new WebClient();
            wc.Headers["User-Agent"] = "GuardAPI";
            try
            {
                return wc.DownloadString(Guard.ApiUrl + $"tools.php?type={type}&input={input}");
            }
            catch
            {
                return "Error when contacting API!";
            }
        }
    }
    public class UserInfo
    {
        public static string Username { get; set; }
        public static string Email { get; set; }
        public static int Level { get; set; }
        public static DateTime Expires { get; set; }
        public static string IP { get; set; }

        public static string HWID = Guard.getUniqueID();
    }
}