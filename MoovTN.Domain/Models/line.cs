using System;
using System.Collections.Generic;
namespace MoovTn.Domain.Models
{
    public partial class Line
    {
        public Line()
        {
            this.notifications = new List<Notification>();
            this.trips = new List<Trip>();
            this.linestations = new List<Linestation>();
            this.trips1 = new List<Trip>();
            this.subscriptioncards = new List<SubscriptionCard>();
            this.users = new List<User>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string type { get; set; }
        public virtual ICollection<Notification> notifications { get; set; }
        public virtual ICollection<Trip> trips { get; set; }
        public virtual ICollection<Linestation> linestations { get; set; }
        public virtual ICollection<Trip> trips1 { get; set; }
        public virtual ICollection<SubscriptionCard> subscriptioncards { get; set; }
        public virtual ICollection<User> users { get; set; }
    }
}
