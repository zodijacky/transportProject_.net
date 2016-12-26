using Microsoft.AspNet.Identity;
using MoovTn.Domain.Models;
using MoovTn.Service.Classes;
using MoovTn.Service.Interfaces;
using MoovTn.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;

namespace MoovTn.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class RentController : Controller
    {
        ILocationService locationService = null;

       
        IRentService rentService = null;
        public RentController()
        {
            locationService = new LocationService();
           
            rentService = new RentService(); 
        }
        // GET: Rent
        public ActionResult Index(string id)
        {
       
             
            return View(rentService.GetMany(i => i.user_id == id));
        }

        // GET: Rent/Details/5
        public ActionResult Details(int id)
        {
            RentModel model=new RentModel();
            Rent r1 = rentService.GetById(id);
            long t1 = Convert.ToInt64(r1.idDepart);
            long t2 = Convert.ToInt64(r1.idDestination);

            Location Departure = locationService.GetById(t1);
            Location Arrival = locationService.GetById(t2);
            model.latitudeArrival = Arrival.latitude;
            model.longitudeArrival = Arrival.longitude;
            model.longitudeDeparture = Departure.longitude;
            model.latitudeDeparture = Departure.latitude;
            model.members = r1.members;
            model.dateArrival = (DateTime)r1.rentEnd;
            model.dateDeparture = (DateTime)r1.rentStart;
            model.Status = r1.status;
            model.idRent = r1.id;
            TimeSpan days = (DateTime)r1.rentEnd - (DateTime)r1.rentStart;
            Double numberDays = days.TotalDays;
            model.Days = numberDays;
            return View(model);
        }

        // GET: Rent/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/Create
        [HttpPost]
        
        public ActionResult Create(RentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    String latitude1 = model.latitudeDeparture.ToString();
                    String longitude1 = model.longitudeDeparture.ToString();
                    String latitude2 = model.latitudeArrival.ToString();
                    String longitude2 = model.longitudeArrival.ToString();
                  
                    string Adresse1 = ReverseGeoLoc(longitude1, latitude1);

                    string Adresse2 = ReverseGeoLoc(longitude2, latitude2);
                 
                    Location location1 = new Location { adress = Adresse1, latitude = model.latitudeDeparture, longitude = model.longitudeDeparture };
                    Location location2 = new Location { adress = Adresse2, latitude = model.latitudeArrival, longitude = model.longitudeArrival };
                    locationService.Add(location1);
                    locationService.Add(location2);
                    locationService.Commit();
                    Location FindLocation1 = locationService.Get(t => t.adress.Equals(Adresse1));
                    Location FindLocation2 = locationService.Get(t => t.adress.Equals(Adresse2));


                    Rent rent = new Rent { user_id = User.Identity.GetUserId(), idDepart= FindLocation1.id,idDestination = FindLocation2.id,status= "In Porgress", members=model.members,rentStart=model.dateDeparture,rentEnd=model.dateArrival};

                    rentService.Add(rent);
                    rentService.Commit();
                    Rent r1 = rentService.Get(t => t.idDepart == FindLocation1.id  );
                    return RedirectToAction("Details", new { id = r1.id });
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Rent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rent/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Rent rent = rentService.GetById(id);
            if (rent == null) return HttpNotFound();
            rentService.Delete(rent);
            rentService.Commit();


            return RedirectToAction("Create");
        }

        // POST: Rent/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public static string ReverseGeoLoc(string longitude, string latitude
             )
        {

           String  Address_ShortName = "";
            String Address_country = "";
           

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false");
                XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                if (element.InnerText == "ZERO_RESULTS")
                {
                    return ("No data available for the specified location");
                }
                else
                {

                    element = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");

                    
                    string shortname = "";
                    string typename = "";
                    bool fHit = false;


                    XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
                    foreach (XmlNode xn in xnList)
                    {
                        try
                        {
                            
                            shortname = xn["short_name"].InnerText;
                            typename = xn["type"].InnerText;


                            fHit = true;
                            switch (typename)
                            {
                                //Add whatever you are looking for below
                                case "country":
                                    {
                                        
                                        Address_ShortName = shortname;
                                        break;
                                    }

                                

                                 

                                
 

                                default:
                                    fHit = false;
                                    break;
                            }


                            if (fHit)
                            {
                                Console.Write(typename);
                                Console.ForegroundColor = ConsoleColor.Green;
                             
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }

                        catch (Exception e)
                        {
                            //Node missing either, longname, shortname or typename
                            fHit = false;
                            Console.Write(" Invalid data: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\tX: " + xn.InnerXml + "\r\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }


                    }

                    //Console.ReadKey();
                    return (element.InnerText);
                }

            }
            catch (Exception ex)
            {
                return ("(Address lookup failed: ) " + ex.Message);
            }
        }
    }
}

