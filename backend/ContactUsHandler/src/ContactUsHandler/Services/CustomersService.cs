using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace ContactUsHandler.Services
{
    public class CustomerService
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "Customers";

        public async Task<bool> SaveCustomer(string email)
        {
            try
            {
                var customersTable = Table.LoadTable(client, tableName);
                
                var customer = new Document();
                customer["Email"] = email;
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
