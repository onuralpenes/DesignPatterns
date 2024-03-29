using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;

namespace DesignPatterns
{
    
    /*
        * Should be open for extension and also should be closed for modification
        * Product Filter class can used but if we want another filter type or filter for double
        we should add another function and have to modify this function
        * With Better Filter class and their interfaces its open for extension
    */
    public enum Color
    {
        Red,Green,Blue
    }

    public enum Size
    {
        Small,Medium,Large,Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName:nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }
    
    public class ProductFilter
    {
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }
    }
    
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpesification<Product> spec)
        {
            foreach (var item in items)
            {
                if (spec.IsSatisfied(item))
                    yield return item;
            }
        }
    }
    
    public interface ISpesification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpesification<T> spec);
    }

    public class ColorSpesification : ISpesification<Product>
    {
        private Color color;

        public ColorSpesification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }
    public class SizeSpesification : ISpesification<Product>
    {
        private Size size;

        public SizeSpesification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class AndSpecification<T> : ISpesification<T>
    {
        private ISpesification<T> first, second;

        public AndSpecification(ISpesification<T> first, ISpesification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }


    public class Open_ClosedPrinciple
    {
        public void Open_ClosedPrincipleMain()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green Products (old):");
            foreach (var p in pf.FilterByColor(products,Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green" );
            }
            
            var bf = new BetterFilter();
            Console.WriteLine("Green Products (new):");
            foreach (var p in bf.Filter(products,new ColorSpesification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green" );
            }
            
            Console.WriteLine("Large blue items:");
            foreach (var p in bf.Filter(products,new AndSpecification<Product>(new ColorSpesification(Color.Blue),new SizeSpesification(Size.Large))))
            {
                Console.WriteLine($" - {p.Name} is large and blue" );
            }
            
        }
    }
}