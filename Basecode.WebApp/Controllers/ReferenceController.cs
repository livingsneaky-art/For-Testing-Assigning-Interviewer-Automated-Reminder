﻿using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ReferenceController : Controller
    {
        /// <summary>
        /// Stores data from the inputs of the user for their personal information in the public application form.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="lastName"></param>
        /// <param name="date"></param>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <param name="nationality"></param>
        /// <param name="tempStreet"></param>
        /// <param name="tempCity"></param>
        /// <param name="tempProvince"></param>
        /// <param name="tempZip"></param>
        /// <param name="tempPhone"></param>
        /// <param name="tempMail"></param>
        /// <returns>View of the Character Reference Form Page.</returns>
        [HttpPost]
        public IActionResult Index(string firstName,
                            string middleName,
                            string lastName,
                            string date,
                            string age,
                            string gender,
                            string nationality,
                            string tempStreet,
                            string tempCity,
                            string tempProvince,
                            string tempZip,
                            string tempPhone,
                            string tempMail)
        {
            TempData["Name"] = firstName + " " + middleName + " " + lastName;
            TempData["Birthdate"] = date;
            TempData["Age"] = age;
            TempData["Gender"] = gender;
            TempData["Nationality"] = nationality;
            TempData["Address"] = tempStreet + ", " + tempCity + ", " + tempProvince + " " + tempZip;
            TempData["Phone"] = tempPhone;
            TempData["Email"] = tempMail;
            return View();
        }
    }
}