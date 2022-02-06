using System;
using System.Net.Http;
using Domain;
using Hangfire;


namespace Application.HangFire
{

    public interface IReaminder
    {
        void DelayReminder(string meeting, DateTime date, string name, string surName, string phone);
    }
    public class Reminder: IReaminder
    {
        static readonly HttpClient client = new HttpClient();

        
        public void DelayReminder(string meeting, DateTime date, string name, string surName, string phone)
        {

            var str =$"<b>Паціент:</b> <i>{name + " " + surName}</i> \n" + 
            $"<b>Заплановано на: </b> <i>{date.ToString("F",new System.Globalization.CultureInfo("ru-Ru"))} </i> \n" +
            $"<b>Назначення:</b> <i>{meeting}</i> \n" +
            $"<b>Тел.</b> {phone}" ;

            client.GetAsync($". .  .  ");

        }
    }
}