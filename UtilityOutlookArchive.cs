using System;

using Outlook;


namespace WordEngineering
{
 public class UtilityOutlook
 {
  public static bool OutlookNewSession = true;
  public static bool OutlookShowDialog = false;

  public static string OutlookPassword = "";
  public static string OutlookProfile = "";

  public static void Main( String[] argv )
  {
   ContactCreate(argv);
   ContactList();
  }

  public static void ContactCreate(String[] argv)
  {
   try
   {
    Outlook.ApplicationClass outlookApplicationClass = new Outlook.ApplicationClass();

    Outlook.NameSpace outlookNameSpace = outlookApplicationClass.GetNamespace("MAPI");
    outlookNameSpace.Logon(OutlookProfile, OutlookPassword, OutlookShowDialog, OutlookNewSession);


    foreach(string contactName in argv)
    {
     Outlook.ContactItem outlookContact = (Outlook.ContactItem) outlookApplicationClass.CreateItem(OlItemType.olContactItem);

     outlookContact.FirstName = contactName;
     outlookContact.LastName = contactName;
     outlookContact.MailingAddressStreet = "123 Some St.";
     outlookContact.MailingAddressCity = "Anytown";
     outlookContact.MailingAddressState = "CA";
     outlookContact.MailingAddressPostalCode = "12345";
     outlookContact.MailingAddressCountry = "USA";
     outlookContact.CompanyName = "Acme Inc.";
     outlookContact.Email1Address = "user@localhost.com";
     outlookContact.Email1AddressType = "SMTP";
     outlookContact.Save();
    } 
   }
   catch (System.Exception ex)
   {
    System.Console.WriteLine("Exception Message: {0}", ex.Message);
   }
  }

  public static void ContactList()
  {
   try
   {
    Outlook.ApplicationClass outlookApplicationClass = new Outlook.ApplicationClass();
    Outlook.NameSpace outlookNameSpace = outlookApplicationClass.GetNamespace("MAPI");
    outlookNameSpace.Logon(OutlookProfile, OutlookPassword, OutlookShowDialog, OutlookNewSession);
    Outlook.MAPIFolder outlookContacts = outlookNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);

    foreach (Outlook.ContactItem outlookContactItem in outlookContacts.Items)
    {
     System.Console.WriteLine
     (
      "First Name: {0} | Last Name: {1} | Email1 Address: {2}",
      outlookContactItem.FirstName,
      outlookContactItem.LastName,
      outlookContactItem.Email1Address
     );
    }
   }
   catch (System.Exception ex)
   {
    System.Console.WriteLine("Exception Message: {0}", ex.Message);
   }
  } 
 }
}