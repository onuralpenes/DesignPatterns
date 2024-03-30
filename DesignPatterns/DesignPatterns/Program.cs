using System;
using System.Diagnostics;

namespace DesignPatterns
{
    class Program
    {
        
        // This is the main of the Design Pattern project, you can uncomment the desired part of the project
        // to see the result
        
        
        static void Main(string[] args)
        {
            
            // Open_ClosedPrinciple openClosedPrinciple = new Open_ClosedPrinciple();
            // openClosedPrinciple.Open_ClosedPrincipleMain();

            LiskovSubstitutionPrinciple liskovSubstitutionPrinciple = new LiskovSubstitutionPrinciple();
            liskovSubstitutionPrinciple.LiskovSubPrinciple();

        }
    }
}