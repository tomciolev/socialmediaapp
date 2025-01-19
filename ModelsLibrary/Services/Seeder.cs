using Microsoft.AspNetCore.Identity;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Services
{
    public class Seeder
    {
        private readonly SocialMediaDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public Seeder(SocialMediaDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (!_dbContext.Database.CanConnect())
                return;

            if (!_userManager.Users.Any())
            {
                var userss = GetUsers().ToList();
                foreach (var user in userss)
                {
                    await _userManager.CreateAsync(user, "Password123");
                }
            }
            if (!_dbContext.Posts.Any())
            {
                var users = _userManager.Users.ToList();
                var posts = new List<Post>()
                {
                    new Post()
                    {
                        Id=Guid.NewGuid().ToString(),
                        Title = "Świetny dzień nad rzeką z rodziną",
                        Content = "Ale popływaliśmy! Kajaki, ognisko i rodzinka - to jest to!",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://zabakcylowani.pl/wp-content/uploads/2021/05/drawienski-park-narodowy-nowiccy-13.jpg",
                        UserId = users[0].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = users[0].Id,
                                Emoji = "like"
                            },
                            new Reaction
                            {
                                Id=Guid.NewGuid().ToString(),
                                UserId = users[1].Id,
                                Emoji = "laugh"
                            }
                        }
                    },
                    new Post()
                    {
                        Id=Guid.NewGuid().ToString(),
                        Title = "Wspaniały wieczór przy ognisku",
                        Content = "Niezapomniany wieczór z przyjaciółmi przy ognisku nad jeziorem.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://tropster.pl/wp-content/uploads/2014/12/ognisko.jpg",
                        UserId = users[0].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[2].Id, Emoji = "like" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[3].Id, Emoji = "congrats" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[0].Id, Emoji = "congrats" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[1].Id, Emoji = "congrats" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Spacer po górskich szlakach",
                        Content = "Piękne widoki i świeże powietrze, to właśnie kocham w górach!",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://bartekwpodrozy.pl/wp-content/uploads/2021/04/toomas-tartes-Yizrl9N_eDA-unsplash_wyr.jpg",
                        UserId = users[0].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[1].Id, Emoji = "like" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[2].Id, Emoji = "laugh" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Weekend na plaży",
                        Content = "Spędzony czas na słonecznej plaży, relaks i odpoczynek.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://wakacjezdzieciakiem.pl/media/reviews/photos/original/3e/97/ab/666-gdynia-plaza-srodmiescie-26-1623515399.jpg",
                        UserId = users[0].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[2].Id, Emoji = "like" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Wieczór w kinie",
                        Content = "Oglądanie najnowszych hitów w kinie z najlepszymi przyjaciółmi.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://krakow.atrakcje.pl/zdjecia/atrakcje/2292/11361/1920x380/1/photo_190704205017_to_masz_wstawic.jpg",
                        UserId = users[3].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[0].Id, Emoji = "laugh" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[1].Id, Emoji = "sad" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Rodzinny obiad w ogrodzie",
                        Content = "Nic nie smakuje lepiej niż domowe jedzenie w towarzystwie najbliższych.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://www.weranda.pl/data/articles/letnie_przyjecie_w_ogrodzie_przepisy_(2).jpg",
                        UserId = users[0].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[3].Id, Emoji = "congrats" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Niezwykłe odkrycie w lesie",
                        Content = "Podczas spaceru natrafiliśmy na pięknie ukrytą polanę pełną dzikich kwiatów.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://smoglab.pl/wp-content/uploads/2024/04/Las_3_webLAS_047LAS_016.jpg",
                        UserId = users[2].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[1].Id, Emoji = "congrats" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Zachód słońca na plaży",
                        Content = "Najpiękniejszy zachód słońca, jaki kiedykolwiek widziałem.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://mynaszlaku.pl/wp-content/uploads/2018/10/MorzeBaltyckie08180009.jpg",
                        UserId = users[1].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[0].Id, Emoji = "congrats" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[2].Id, Emoji = "like" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Piknik w parku",
                        Content = "Cudowny dzień spędzony na pikniku z przyjaciółmi.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://1.bonami.pl/images/fit/43/e4/43e46edc47d49fa938081c9e42d44b5e6e783b9b-2000x2000.jpeg",
                        UserId = users[2].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[3].Id, Emoji = "laugh" },
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[2].Id, Emoji = "congrats" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Relaks w górach",
                        Content = "Poranek w górach z widokiem na wschód słońca.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://www.karolnienartowicz.com/wp-content/uploads/2019/12/21-1-1024x748.jpg",
                        UserId = users[3].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[1].Id, Emoji = "congrats" }
                        }
                    },
                    new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Weekendowy wypad na rowery",
                        Content = "Świetnie spędzony czas na rowerowej wycieczce po malowniczych trasach.",
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://d2lljesbicak00.cloudfront.net/merida-v2/media-bg-img//countries/pl-pl/news/264a0641dg-merida-vercors-2018-pano.jpg?p3",
                        UserId = users[3].Id,
                        Reactions = new List<Reaction>
                        {
                            new Reaction { Id = Guid.NewGuid().ToString(),UserId = users[2].Id, Emoji = "like" }
                        }
                    }
                };
                try
                {
                    await _dbContext.Posts.AddRangeAsync(posts);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

        private static IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    FirstName = "Tomek",
                    LastName = "Kowalski",
                    UserName = "tomekkowalski",
                    Email = "tomek@wp.pl",
                },
                new User()
                {
                    FirstName = "Julia",
                    LastName = "Malarska",
                    UserName = "julia_malarska",
                    Email = "julia@wp.pl",
                },
                new User()
                {
                    FirstName = "Max",
                    LastName = "Markowski",
                    UserName = "max_mark",
                    Email = "max@wp.pl",
                },
                new User()
                {
                    FirstName = "Hania",
                    LastName = "Miodek",
                    UserName = "hania_miodek",
                    Email = "hania@wp.pl",
                }
            };
            return users;
        }


    }
}
