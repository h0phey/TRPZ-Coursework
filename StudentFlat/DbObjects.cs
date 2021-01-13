using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using StudentFlat.Models;

namespace StudentFlat
{
    public class DbObjects
    {
        public static void Initial(AppDbContext context)
        {
            if (!context.Owner.Any())
            {
                Student student1 = new Student
                {
                    id = Guid.NewGuid(),
                    name = "Влад",
                    phone = "+380568875923",
                    email = "vlad_2001@gmail.com"
                };
                Student student2 = new Student
                {
                    id = Guid.NewGuid(),
                    name = "Олег",
                    phone = "+380525465923",
                    email = "oleg28@gmail.com"
                };
                Flat flat1 = new Flat()
                {
                    id = Guid.NewGuid(),
                    name = "Квартира на Відрадному",
                    shortDesc = "Гарно, файно",
                    longDesc = "Навіть не сподівайтеся на гарячу воду.",
                    isAvail = true,
                    price = 10000,
                    capacity = 3,
                    img = "~/img/otradnyi.jpg",
                    Students = new List<Student>
                    {
                        student1
                    }
                };
                Flat flat2 = new Flat()
                {
                    id = Guid.NewGuid(),
                    name = "Квартира на Троєщині",
                    shortDesc = "Для справжніх чоловіків",
                    longDesc = "Від центра трохи далековато...",
                    isAvail = true,
                    price = 7000,
                    capacity = 2,
                    img = "~/img/troyeschina.jpg"
                };
                Flat flat3 = new Flat()
                {
                    id = Guid.NewGuid(),
                    name = "Місце в гуртожитку",
                    shortDesc = "10 хвилин до метро",
                    longDesc = "Зате вам рідко буде самотньо.",
                    isAvail = false,
                    price = 700,
                    capacity = 4,
                    img = "~/img/gurtozhytok.jpg",
                    Students = new List<Student>
                    {
                        student2
                    }
                };
                Flat flat4 = new Flat()
                {
                    id = Guid.NewGuid(),
                    name = "Щось там у центрі",
                    shortDesc = "Все дуже круто",
                    longDesc = "Будинок бізнес-класу, складається з трьох 36 поверхових веж з унікальним фасадом і освітленням яке створює ілюзію руху. Вежі об'єднані на рівні п'ятого поверху в єдину територію: декілька спорт майданчиків, лаунж зони, парк та міні Дісней Ленд. З 1 по 5 поверх розміщені: спорт клуб з басейном, дитячий кафетерій, центр розвитку дитини, бутики, продуктові магазини. Після будівельників: стяжка, розводка опалення, радіатори, лічильники на всі комунікації, засклення балконів",
                    isAvail = true,
                    price = 23500,
                    capacity = 2,
                    img = "~/img/centr.png"
                };
                Owner owner1 = new Owner
                {
                    id = Guid.NewGuid(),
                    name = "Василь Іващенко",
                    phone = "+380952462684",
                    email = "vasya233@ukr.net",
                    Flats = new List<Flat>
                    {
                        flat1,
                        flat3
                    }
                };
                flat1.Owner = owner1;
                flat3.Owner = owner1;
                Owner owner2 = new Owner
                {
                    id = Guid.NewGuid(),
                    name = "Андрій Кучер",
                    phone = "+380278854665",
                    email = "andrijhome@outlook.com",
                    Flats = new List<Flat>
                    {
                        flat4
                    }
                };
                flat4.Owner = owner2;
                Owner owner3 = new Owner
                {
                    id = Guid.NewGuid(),
                    name = "Денис Сокіл",
                    phone = "+380975584635",
                    email = "densocol@ukr.net",
                    Flats = new List<Flat>
                    {
                        flat2
                    }
                };
                flat2.Owner = owner3;
                context.AddRange(owner1, owner2, owner3);
                context.Student.AddRange(student1, student2);
                context.Flat.AddRange(flat1, flat2, flat3, flat4);
            }
            context.SaveChanges();
        }
    }
}
