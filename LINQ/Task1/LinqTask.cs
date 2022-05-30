using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Select((c, s) => new ValueTuple<Customer, IEnumerable<Supplier>>
            {
                Item1 = c, 
                Item2 = suppliers.Where(su => su.City == c.City && su.Country == c.Country)
            });
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            var suppliersByAddresses = suppliers.GroupBy(s => (s.City, s.Country));

            return customers.Select((c, s) => new ValueTuple<Customer, IEnumerable<Supplier>>
            {
                Item1 = c,
                Item2 = suppliersByAddresses
                    .Where(su => su.Key == (c.City, c.Country))
                    .SelectMany(su => su)
                    
            });
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(c => c.Orders.Any(o => o.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(c => c.Orders.Length > 0).Select((c, s) => new ValueTuple<Customer, DateTime>
            {
                Item1 = c,
                Item2 = c.Orders.Min(o => o.OrderDate)
            });
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(c => c.Orders.Length > 0).Select((c, s) => new ValueTuple<Customer, DateTime>
            {
                Item1 = c,
                Item2 = c.Orders.Min(o => o.OrderDate)
            }).OrderBy(r => r.Item2.Year)
                .ThenBy(r => r.Item2.Month)
                .ThenByDescending(r => r.Item1.Orders.Sum(o => o.Total))
                .ThenBy(r => r.Item1.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(c => !c.PostalCode.All(char.IsDigit) ||
                                        c.Region == null ||
                                        !c.Phone.Contains('('));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var groupedProducts =
                from product in products
                group product by product.Category into categories
                from unitsInStock in (
                    from product in categories
                    group product by product.UnitsInStock
                )
                group unitsInStock by categories.Key;

            var res = new List<Linq7CategoryGroup>();

            foreach (var categories in groupedProducts)
            {
                var list = categories.Select(unitsInStock => new Linq7UnitsInStockGroup
                {
                    UnitsInStock = unitsInStock.Key, 
                    Prices = unitsInStock.Select(p => p.UnitPrice)
                }).ToList();

                res.Add(new Linq7CategoryGroup
                {
                    Category = categories.Key, 
                    UnitsInStockGroup = list
                });
            }

            return res;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return products.OrderBy(p => p.UnitPrice)
                .GroupBy(pr => pr.UnitPrice <= cheap ? cheap : pr.UnitPrice <= middle ? middle : expensive)
                .Select(m => (m.Key, m.Select(prod => prod)));
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            var res = customers.GroupBy(c => c.City)
                .Select(g => new ValueTuple<string, int, int>
                {
                    Item1 = g.Key,
                    Item2 = (int)Math.Round(g.Average(cu => cu.Orders.Length > 0 ? cu.Orders.Sum(o => o.Total) : 0)),
                    Item3 = g.Sum(cu => cu.Orders.Count()) / g.Count()
                });

            return res;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers is null)
            {
                throw new ArgumentNullException(nameof(suppliers));
            }

            return suppliers.Select(s => s.Country)
                .Distinct()
                .OrderBy(s => s.Length)
                .ThenBy(s => s)
                .Aggregate((a, b) => a + b);
        }
    }
}