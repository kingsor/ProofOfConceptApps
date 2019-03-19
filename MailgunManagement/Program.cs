using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;
using Scriban;
using System;
using System.Configuration;

namespace MailgunManagement
{
    class Program
    {
        static void Main(string[] args)
        {

            //EncryptConfig();

            //DecryptConfig();

            SendFluentEmail();

            Console.ReadKey();

        }

        static void TestEncryptAndSave()
        {
            var textToEncrypt = ConfigurationManager.AppSettings["DomainName"];
            var passPhrase = Environment.GetEnvironmentVariable("PASSPHRASE");

            var encrypted = StringCipher.Encrypt(textToEncrypt, passPhrase);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add("DomainNameEncrypted", encrypted);
            config.AppSettings.Settings.Add("DomainNamePippo", encrypted);
            config.Save();

            config.AppSettings.Settings["DomainNamePippo"].Value = "prova";
            config.Save();

            Console.WriteLine("Encrypted: {0}", encrypted);

            var decrypted = StringCipher.Decrypt(encrypted, passPhrase);

            Console.WriteLine("Decrypted: {0}", decrypted);
        }

        static void EncryptAppSettings(string key, string passPhrase)
        {
            var textToEncrypt = ConfigurationManager.AppSettings[key];
            var encrypted = StringCipher.Encrypt(textToEncrypt, passPhrase);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(key + "Orig", textToEncrypt);
            config.AppSettings.Settings[key].Value = encrypted;
            config.Save();
        }

        static void EncryptConfig()
        {
            var passPhrase = Environment.GetEnvironmentVariable("PASSPHRASE");

            EncryptAppSettings("DomainName", passPhrase);
            EncryptAppSettings("ApiKey", passPhrase);
            EncryptAppSettings("ToEmails", passPhrase);
            EncryptAppSettings("FromEmail", passPhrase);
            EncryptAppSettings("FromName", passPhrase);
        }

        static void DecryptConfig()
        {
            var passPhrase = Environment.GetEnvironmentVariable("PASSPHRASE");

            var domainName = DecryptAppSettings("DomainName", passPhrase);
            var apiKey = DecryptAppSettings("ApiKey", passPhrase);
            var toEmails = DecryptAppSettings("ToEmails", passPhrase);
            var fromEmail = DecryptAppSettings("FromEmail", passPhrase);
            var fromName = DecryptAppSettings("FromName", passPhrase);
        }

        static string DecryptAppSettings(string key, string passPhrase)
        {
            string res = string.Empty;

            res = StringCipher.Decrypt(ConfigurationManager.AppSettings[key], passPhrase);

            return res;
        }

        static void SendFluentEmail()
        {
            var passPhrase = Environment.GetEnvironmentVariable("PASSPHRASE");

            var domainName = DecryptAppSettings("DomainName", passPhrase);
            var apiKey = DecryptAppSettings("ApiKey", passPhrase);
            var toEmails = DecryptAppSettings("ToEmails", passPhrase);
            var fromEmail = DecryptAppSettings("FromEmail", passPhrase);
            var fromName = DecryptAppSettings("FromName", passPhrase);

            //var domainName = ConfigurationManager.AppSettings["DomainName"];
            //var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            //var toEmails = ConfigurationManager.AppSettings["ToEmails"];
            //var fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            //var fromName = ConfigurationManager.AppSettings["FromName"];


            var sender = new MailgunSender(
                domainName, // Mailgun Domain
                apiKey // Mailgun API Key
            );

            Email.DefaultSender = sender;

            // Parse a scriban template
            var template = Template.Parse("Dear {{name}}, You are totally {{compliment}}.");
            var bodyFromTemplate = template.Render(new { Name = "Luke", Compliment = "Awesome" });

            //.UsingTemplate(template, new { Name = "Luke", Compliment = "Awesome" })

            var objEmail = Email
                .From(fromEmail, fromName)
                .Subject(String.Format("FluentEmail.Mailgun Testing - {0}", DateTime.Now.ToString("yyyyMMddHHmmss")))
                .Body(bodyFromTemplate);

            objEmail.AttachFromFilename("output.txt");

            if (!String.IsNullOrEmpty(toEmails))
            {
                String[] emails = toEmails.Split(';');

                foreach (string email in emails)
                {
                    objEmail.To(email);
                }
            }

            SendResponse response = objEmail.Send();
        }
    }
}
