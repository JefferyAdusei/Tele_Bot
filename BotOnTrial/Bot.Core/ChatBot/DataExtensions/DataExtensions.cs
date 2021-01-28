using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Core.Data;
using Bot.Core.Di;

namespace Bot.Core.ChatBot.DataExtensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// Authenticates a user against the data base if they already exists
        /// </summary>
        /// <param name="username">The username of the telegram user</param>
        /// <returns>A boolean indicating whether the user exists</returns>
        public static async Task<bool> AuthenticateSenderAsync(this string username)
        {
            // Check if user already exist in the database
            try
            {
                BotDataModel result = await CoreDi.ReadDataService.RetrieveUserAsync(username);
                return result.Id > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="username">The unique identifier of the user</param>
        /// <param name="firstname">The firstname of the user</param>
        /// <param name="passCode">The companies pass code</param>
        /// <returns></returns>
        public static async Task<bool> AddNewUserAsync(this string username, string firstname, string passCode)
        {
            try
            {
                return await CoreDi.WriteDataService
                    .InsertRecordAsync(new BotDataModel
                    {
                        Firstname = firstname,
                        Username = username,
                        PassCode = passCode
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Get the list of all available colleague contacts
        /// </summary>
        /// <returns></returns>
        public static async Task<IAsyncEnumerable<BotDataModel>> GetOfficeColleagues()
        {
            try
            {
                return await CoreDi.ReadDataService.RetrieveAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<BotDataModel> GetColleagueAsync(this string username, string id)
        {
            try
            {
                BotDataModel passCode = await CoreDi.ReadDataService.RetrieveUserAsync(username);

                return await CoreDi.ReadDataService.RetrieveUserAsync(int.Parse(id), passCode.PassCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Sets users update information
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="update">The update to carry out</param>
        /// <returns></returns>
        public static async Task<bool> UpdateUserInfo(this string username, string update)
        {
            try
            {
                string updateProperty = update.Split('=')[0].ToLower().Substring(0,1);

                update = update.Split('=')[1];

                return await CoreDi.WriteDataService.UpdateDataAsync(updateProperty, update, username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        #region Helper Methods

        public static async Task<string> GetUserPassCode(this string username)
        {
            BotDataModel passCode = await CoreDi.ReadDataService.RetrieveUserAsync(username);

            return passCode.PassCode;
        }

        #endregion
    }
}