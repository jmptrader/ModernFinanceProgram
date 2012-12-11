using FWMonyker.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace FWMonyker.XML
{
    public class ObjextXMLSerializer
    {
        private static ObjextXMLSerializer instance;
        private ObjextXMLSerializer() { }

        public static ObjextXMLSerializer GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjextXMLSerializer();
                }
                return instance;
            }
        }
        public IEnumerable<Account> LoadAccounts()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MFP\\");
            string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MFP\\data.xml";
            List<Account> accountList = null;
            try
            {
                XDocument document = XDocument.Load(fileLocation);
                var accounts = from account in document.Descendants("Account")
                               select new Account
                               {
                                   Name = account.Attribute("Name").Value,
                                   Balance = decimal.Parse(account.Attribute("Balance").Value),
                                   Colour = new SolidColorBrush((Color)ColorConverter.ConvertFromString(account.Attribute("Colour").Value)),
                                   Transactions = from transaction in account.Descendants("Transaction").ToList()
                                                  select new Transaction
                                                  {
                                                      Description = transaction.Attribute("Description").Value,
                                                      Recipient = transaction.Attribute("Recipient").Value,
                                                      Amount = decimal.Parse(transaction.Attribute("Amount").Value),
                                                      TimeStamp = DateTime.FromBinary(long.Parse(transaction.Attribute("TimeStamp").Value))
                                                  }
                               };
                accountList = accounts.ToList();
            }
            catch (FileNotFoundException)
            {
            }
            return accountList ?? new List<Account>();
        }

        public void SaveAccounts(IEnumerable<Account> accountEnumerable)
        {
            Account[] accounts = accountEnumerable.ToArray();
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),

                new XElement("Accounts",

                from account in accounts
                select new XElement("Account",
                           new XAttribute("Name", account.Name),
                           new XAttribute("Balance", account.Balance),
                           new XAttribute("Colour", account.Colour),
                       new XElement("Transactions",

                           from transaction in account.Transactions.ToArray()
                           select new XElement("Transaction",
                               new XAttribute("Description", transaction.Description),
                               new XAttribute("Recipient", transaction.Recipient),
                               new XAttribute("Amount", transaction.Amount),
                               new XAttribute("TimeStamp", transaction.TimeStamp.ToBinary())
                           )
                       )
                )
                )
            );
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MFP\\data.xml";
            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MFP\\");
                document.Save(path);
            }
            catch (IOException)
            {
                MessageBox.Show("Could not save data!\nIOException occured", "Save error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Not allowed to save at "+path, "Save error", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
        }
    }
}
