namespace WebStepBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Tags.Any())
            {
                CreateTag(context, "bmw");
                CreateTag(context, "toyota");
                CreateTag(context, "car");
                CreateTag(context, "audi");
                CreateTag(context, "a3");
                CreateTag(context, "m6");
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUser(context, "admin@gmail.com", "123", "System Administrator");
                CreateUser(context, "pesho@gmail.com", "123", "Peter Parker");
                CreateUser(context, "merry@gmail.com", "123", "Mariah Smith");
                CreateUser(context, "geshu@gmail.com", "123", "George Kostov");

                CreateRole(context, "Administrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");

                CreatePost(context,
                    title: "2002 BMW 3-Series 325CI",
                    body: @"FLORIDA CONDO CAR
                           2002 BMW 325CI with 108,799 miles FLY IN AND DRIVE HOME, OR STAY IN SUNNY FLORIDA FOR THE WINTER, WITH A PRISTINE BMW CONVERTIBLE TO ENJOY THE SUN AND SAND.
                            THIS IS A LOCAL NAPLES FLORIDA, GULF SHORE BOULEVARD CONDO CAR.
                            EXTERIOR IS SILVER (STAYS COOLER IN THE SUN) AND COMPLEMENTED BY A GRAY LEATHER INTERIOR,  BLACK POWER CONVERTIBLE TOP
                            RECENT SERVICE, LIKE NEW BLACK POWER TOP, TOOL KIT, BOOK, SUPER CLEAN,ITS ALWAYS NICE TO FIND A LOW MILAGE FLORIDA CAR.
                            US $8,499.00",
                    date: new DateTime(2016, 03, 27, 17, 53, 48),
                    authorUsername: "pesho@gmail.com",
                     tags: "car,bmw"
                );

                CreatePost(context,
                    title: "1987 BMW M6",
                    body: @"This BMW E24 M6 is not only a great investment vehicle but it is the perfect balance of classic design and elegant performance, No vintage BMW offers a combination like a e24 M6, there going to become un-affordable soon enough. With only 1632 of these built how many have not been wrecked, neglected, restored? How many are all original with original paint and in near perfect condition? There only original once. There are very few of these 6ers that have been maintained like this one and can still truly be driven daily and enjoyed while increasing in value at the same time. This 6er looks like a time capsule, shows like a 10k mile car. All the original nickel plating from top to bottom shines like the day it left the showroom... there is no comparable to this m6.
                            This classic M Series has been cared for extensively, Everything works! as if it were new… the previous owner has kept and documented this car in detail as you will see with the accompanied receipts and records that are all included with the purchase. All original VIN Tags are pictured, hood, door jams, trunk etc.
                            Also Includes the original tool kit, with all original tools. Everything on this M6 is in working order! The A/C blows Ice Cold, and all interior components are in working order! Engine and transmission run flawlessly. This is a no issue 6er, pulls nice through every gear. Starts and Runs Smooth Every time. Undercarriage is at Mint as it comes. Rust Free BMW. This M6 has 132K Miles but drives like new, it was well taken care of and shows.
                            US $39,500.00",
                    date: new DateTime(2016, 05, 11, 08, 22, 03),
                    authorUsername: "merry@gmail.com",
                     tags: "car,bmw,m6"
                );

                CreatePost(context,
                    title: "1979 BMW 3-Series 320i",
                    body: @"a 350 small block and chevy tranny. vehicle runs and drives. Vehicle comes with BC serious struts that are adjustable. Two 3inch flow master super 10 and a bm shifter. Solid body and very fun car. Asking 2800.00 
                            US $2,800.00",
                    date: new DateTime(2016, 03, 27, 17, 53, 48),
                    authorUsername: "geshu@gmail.com",
                    tags: "car,bmw"
                );

                CreatePost(context,
                    title: "2015 Audi A3",
                    body: @"UP FOR SALE IS A BEAUTIFUL 2015 AUDI A3 1.8T BLUE ON BLACK LEATHER INTERIOR WITH ONLY 12K ORIGINAL MILES!!!!!!
                            WE HAVE RECENTLY PURCHASED THIS A3 FROM AN INSURANCE COMPANY WITH NO DAMAGE!!!!! AS YOU SEE THE VEHICLE PICTURED BELOW... IS IN THE SAME STATE AS WE PURCHASED IT.
                            THIS AUDI A3 HAD MINOR PREVIOUS COLLISION DAMAGE TO THE FRONT WHICH THE INSURANCE COMPANY HAD PROFESSIONALLY REPAIRED PRIOR TO SALE. THERE ARE NO SIGNS OF ANY PREVIOUS WORK!!!! PAINT MATCHED 100%!!!! DRIVES EXCELLENT!!!! NO WIERD NOISES!!! NO VIBRATIONS!!! DRIVES STRAIGHT AND HANDLES GREAT!!! NO KNOWN MECHANICAL ISSUES!!! NO LIGHTS ON DASH!!!
                            THE CAR IS IN GREAT CONDITION INSIDE AND OUT AS YOU MAY SEE ON THE 70+ PICTURES LISTED BELOW!!!
                            EQUIPT WITH: 1.8T ENGINE, S-TRONIC TRANSMISSION, XENON LED HEADLIGHTS, NAVIGATION, KEYLESS GO & ENTRY, SATELLITE RADIO, BLUETOOTH, DUAL ZONE CLIMATE CONTROL, MULTI-FUNCTION STEERING WHEEL CONTROLS, SUNROOF, CRUISE, POWER EVERYTHING, LEATHER SEATS, AND MUCH MUCH MORE....
                            THE INSURANCE COMPANY VALUED THIS AUDI A3 AT $26,698",
                    date: new DateTime(2016, 02, 18, 22, 14, 38),
                    authorUsername: "pesho@gmail.com",
                     tags: "car,audi,a3"
                );

                CreatePost(context,
                    title: "2008 Toyota Corolla No Reserve,Warranty,No Reserve",
                    body: @"The 2008 Toyota Corolla comes in three different models: the base CE, sporty S, and better equipped LE. Each can be equipped with either a five-speed manual transmission or four-speed automatic. The 126-horsepower, 1.8L four-cylinder engine that powers all Corollas has variable valve timing with intelligence, which helps give it good response through the rev range without hampering fuel efficiency. With the manual transmission, the Corolla can accelerate from zero to 60 mph in just under nine seconds, according to Toyota, yet it achieves EPA fuel economy ratings of 28 mpg city, 35 mpg highway.The Corolla has a roomy interior and comfortable ride, yet a stiff structure and variable-assist rack-and-pinion steering give it a nimble feel. The back seat is split 60/40 and folds forward to expand the 13.6-cubic-foot trunk.Side-bolster front-seat air bags and full-length curtain air bags are available as an option across the line, as are anti-lock brakes, while electronic stability control is available only on LE and S models with the optional automatic transmission. Models equipped with the stability control system and anti-lock brakes also get brake assist, which helps apply full brake force in emergency braking.Standard equipment on the base CE model includes air conditioning, tilt steering, a height-adjustable driver's seat, a trunk lamp, power mirrors, and a CD sound system with four speakers. The S model adds a sportier look, with smoked headlamp lenses, integrated front fog lamps, under-body spoilers, rocker panel extensions, and rear mudguards, and it also upgrades to power locks, map lights, and a six-speaker sound system. The LE doesn't get all the sporty appearance cues of the S but instead has remote keyless entry, power windows, and woodgrain trim. Other major options include a power moonroof, alloy wheels, and an eight-speaker JBL sound system with six-disc changer
                            us $3,100.00",
                    date: new DateTime(2016, 04, 11, 19, 02, 05),
                    authorUsername: "geshu@gmail.com",
                    tags: "car,toyota"
                );

                CreatePost(context,
                    title: "2010 Toyota Tundra SR5 Double Cab",
                    body: @"We are pleased to offer this 2010 Toyota Tundra SR5 Double Cab that is damaged. We can offer Domestic and International shipping arrangements, please take a look at the pictures for more details and don't pass up the opportunity to own this for a fraction of the original sales price as the listing can be removed any second due to the number of potential buyers!!!! This Vehicle has a SALVAGE TITLE and currently is not registered. The buyer will have to register it in his or her state of residence, which may or may not involve some extra steps compared to registering a clean title car. All California Buyer ONLY must pay 9% sales tax and will receive a Acquisition Bill Of Sale. We make no representations about repairability, availability of parts or costs of repairs.
                            US $6,950.00",
                    date: new DateTime(2016, 06, 30, 17, 36, 52),
                    authorUsername: "merry@gmail.com",
                    tags: "car,toyota"
                );

                context.SaveChanges();
            }
        }

        private void CreateUser(ApplicationDbContext context,
            string email, string password, string fullName)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreateTag(ApplicationDbContext context, string title)
        {
            var tag = new Tag();
            tag.Title = title;
            context.Tags.Add(tag);
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string body, DateTime date, string authorUsername,string tags)
        {
            var post = new Post();
            post.Title = title;
            post.Body = body;
            post.Date = date;
            string[] allTags = tags.Split(',').ToArray();
            foreach (var tag in allTags)
            {
                Tag newTag=new Tag() { Title=tag};
                post.Tag = tags;
                if (context.Tags.Any(t => t.Title == newTag.Title))
                {
                    var existingTag = context.Tags.SingleOrDefault(t => t.Title == newTag.Title);
                    post.Tags.Add(existingTag);
                }
                else
                {
                    post.Tags.Add(newTag);
                }
            }
            post.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            context.Posts.Add(post);
        }
    }
}

