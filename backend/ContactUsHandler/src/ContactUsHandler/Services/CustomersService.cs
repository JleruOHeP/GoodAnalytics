using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

using ContactUsHandler.Models;

namespace ContactUsHandler.Services
{
    public class CustomerService
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "Customers";

        public async Task<bool> SaveCustomer(UserMessageModel message)
        {
            var captchaService = new CaptchaService();
            var captchaResult = await captchaService.ValidateCaptcha(message.CaptchaToken);

            try
            {
                var customersTable = Table.LoadTable(client, tableName);
                
                var customer = new Document();
                customer["Name"] = message.Name;
                customer["Email"] = message.Email;
                customer["Message"] = message.Message;
                customer["CaptchaResponse"] = captchaResult ? "Good" : "Bad";
                customer["Date"] = DateTime.Now;

                await customersTable.PutItemAsync(customer);
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.Message); 
                return false;
            }
            return true;
        }
    }
}
